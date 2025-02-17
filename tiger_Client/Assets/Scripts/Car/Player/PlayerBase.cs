using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public abstract class PlayerBase
    {
        //プレイヤーID
        protected byte _id = byte.MaxValue;
        //プレイヤーのゲームオブジェクト
        protected GameObject _obj = null;
        //プレイヤー操作クラス
        //protected Controller _playerController = null;
        //前フレームの位置
        protected Vector3 _lastPosition = Vector3.zero;
        //前フレームの回転
        protected Quaternion _lastDirection = Quaternion.identity;
        //アナログレバーの入力値
        protected Vector3 _inputValue = Vector3.zero;
        // ボタン入力をマスクにしたもの
        protected PacketData.eInputMask _inputMask = 0;
        // 状態を表すマスク
        protected PacketData.eStateMask _stateMask = 0;
        // eStateMaskが参照されたらtrueになるマスク
        protected bool _isStateUsed = true;
        public byte Id {  get { return _id; } set { _id = value; } }
        public GameObject Obj { get { return _obj; } set { _obj = value; } }
        public Vector3 InputValue { get { return _inputValue; } }
        public PacketData.eInputMask InputMask { get { return _inputMask; } }
        public bool IsGround { get { _isStateUsed = true; return (_stateMask & PacketData.eStateMask.Ground) != 0; } }
        public bool IsReset { get { _isStateUsed = true; return (_stateMask & PacketData.eStateMask.Reset) != 0; } }

        public PlayerBase(GameObject prefab, Transform parent)
        {
            _obj = GameObject.Instantiate(prefab);

            //_obj.transform.parent = parent.transform;

            //_playerController = _obj.GetComponent<Controller>();
        }

        public void SetActive(bool flag) { if (_obj) { _obj.SetActive(flag); } }

        public virtual int ReadByte(byte[] getByte, int offset) { return 0; }

       public virtual void Update(PlayerInputAction input) { }

    }


    public class Player : PlayerBase
    {
        public Player(GameObject prefab, Transform parent) : base(prefab, parent) 
        {
        }

        public override void Update(PlayerInputAction input)
        {
            base.Update(input);
        }
    }

    public class NetPlayer : PlayerBase
    {
        public NetPlayer(GameObject prefab, Transform parent) : base(prefab, parent)
        {
            //_PlayerController.Sleep();
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
            _Controller.Speed = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);

            // 姿勢
            float rx = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float ry = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rw = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.rotation = new Quaternion(rx, ry, rz, rw);

            // ID
            _id = getByte[offset]; offset += sizeof(byte);

            //  表示モデルの種類
            _playerController.Kind = (eKind)getByte[offset]; offset += sizeof(byte);

            // 入力マスクをクリア
            //_inputMask = 0;

            // 状態マスクは参照済（すでに使われていたら）上書き、未参照（まだつかわれていなければ）ORを取ることで前の状態も残す
            if (_isStateUsed) { _stateMask = (PacketData.eStateMask)getByte[offset]; offset += sizeof(byte); }
            else { _stateMask |= (PacketData.eStateMask)getByte[offset]; offset += sizeof(byte); }

            // 位置と姿勢を保存
            _lastPos = _obj.transform.position;
            _lastDir = _obj.transform.rotation;

            return offset;
        }
    }


}
