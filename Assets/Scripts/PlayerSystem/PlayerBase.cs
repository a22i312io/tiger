using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public abstract class PlayerBase
    {
        //�v���C���[ID
        protected byte _id = byte.MaxValue;
        //�v���C���[�̃Q�[���I�u�W�F�N�g
        protected GameObject _obj = null;
        //�d��
        protected float _weight = 0;
        //���x
        protected float _speed = 0;
        //�ō����x
        protected float _maxspeed = 0;
        //������
        protected float _acceleration = 0;
        //�n���h�����O
        protected float _handling = 0;
        //�X�^�~�i
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

            
        }
    }

}
