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
        //重量
        protected float _weight = 0;
        //速度
        protected float _speed = 0;
        //最高速度
        protected float _maxspeed = 0;
        //加速力
        protected float _acceleration = 0;
        //ハンドリング
        protected float _handling = 0;
        //スタミナ
        protected float _stamina = 0;
        
        public byte Id {  get { return _id; } set { _id = value; } }
        public GameObject Obj { get { return _obj; } set { _obj = value; } }
        public float Weight { get { return _weight; } set { _weight = value; } }
        public float Speed { get { return _speed; } set { _speed = value; } }
        public float Acceleration { get { return _acceleration; } set { _acceleration = value; } }
        public float Handling { get { return _handling; } set {_handling = value; } }
        public float Stamina { get { return _stamina; } set { _stamina = value; } }

        public PlayerBase(GameObject prefab, Transform parent)
        {
            _obj = GameObject.Instantiate(prefab);

            _obj.transform.parent = parent.transform;

            //_playerController = _obj.GetComponent<Controller>();
        }

        public void SetActive(bool flag) { if (_obj) { _obj.SetActive(flag); } }

        public virtual int ReadByte(byte[] getByte, int offset) { return 0; }

       

    }

}
