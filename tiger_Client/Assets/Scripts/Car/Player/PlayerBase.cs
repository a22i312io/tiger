using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Player
{
    public abstract class PlayerBase
    {
        //プレイヤーID
        protected byte _id = byte.MaxValue;
        //プレイヤーのゲームオブジェクト
        protected GameObject _obj = null;
        //プレイヤー操作クラス
        protected PlayerController _playerController = null;
        //前フレームの位置
        protected Vector3 _lastPosition = Vector3.zero;
        //前フレームの回転
        protected Quaternion _lastDirection = Quaternion.identity;
        // 移動速度
        protected Vector3 _force = Vector3.zero;
        //アナログレバーの入力値
        protected Vector3 _inputSteering = Vector3.zero;
        // ボタン入力をマスクにしたもの
        protected PacketData.eInputMask _inputMask = 0;
        public byte Id { get { return _id; } set { _id = value; } }
        public GameObject Obj { get { return _obj; } set { _obj = value; } }
        public Vector3 Force { get { return _force; } }
        public Vector3 InputValue { get { return _inputSteering; } }
        public PacketData.eInputMask InputMask { get { return _inputMask; } }
        public bool IsAccelerator { get { return (_inputMask & PacketData.eInputMask.Accelerator) != 0; } }

        public PlayerBase(GameObject prefab, Transform parent)
        {
            _obj = GameObject.Instantiate(prefab);

            _obj.transform.parent = parent.transform;

            _playerController = _obj.GetComponent<PlayerController>();
        }

        public void SetActive(bool flag) { if (_obj) { _obj.SetActive(flag); } }

        public virtual int ReadByte(byte[] getByte, int offset) { return 0; }

        public virtual void Update(Input input) 
        {
            _force = input.SteeringValue;
            //_inputSteering = input.Steering;

            _inputMask = 0;
            if (input.IsAccerelation) { _inputMask |= PacketData.eInputMask.Accelerator; }

        }

    }


    public class Player : PlayerBase
    {
        
        public Player(GameObject prefab, Transform parent) : base(prefab, parent)
        {
        }

        public override void Update(Input input)
        {
            base.Update(input);
            
        }
    }

    public class NetPlayer : PlayerBase
    {
        public NetPlayer(GameObject prefab, Transform parent) : base(prefab, parent)
        {
            _playerController.Sleep();
        }

        // 受信したbyte配列からデータを復元する
        public override int ReadByte(byte[] getByte, int offset)
        {
            // 位置
            float px = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float py = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float pz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.position = new Vector3(px, py, pz);

            // 移動速度
            _playerController.Speed = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);

            // 姿勢
            float rx = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float ry = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rw = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.rotation = new Quaternion(rx, ry, rz, rw);

            // ID
            _id = getByte[offset]; offset += sizeof(byte);

            // 入力マスクをクリア
            //_inputMask = 0;


            // 位置と姿勢を保存
            _lastPosition = _obj.transform.position;
            _lastDirection = _obj.transform.rotation;

            return offset;
        }
    }


}
