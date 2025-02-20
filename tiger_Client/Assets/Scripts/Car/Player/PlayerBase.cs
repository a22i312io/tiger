using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Player
{
    public abstract class PlayerBase
    {
        //�v���C���[ID
        protected byte _id = byte.MaxValue;
        //�v���C���[�̃Q�[���I�u�W�F�N�g
        protected GameObject _obj = null;
        //�v���C���[����N���X
        protected PlayerController _playerController = null;
        //�O�t���[���̈ʒu
        protected Vector3 _lastPosition = Vector3.zero;
        //�O�t���[���̉�]
        protected Quaternion _lastDirection = Quaternion.identity;
        // �ړ����x
        protected Vector3 _force = Vector3.zero;
        //�A�i���O���o�[�̓��͒l
        protected Vector3 _inputSteering = Vector3.zero;
        // �{�^�����͂��}�X�N�ɂ�������
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

        // ��M����byte�z�񂩂�f�[�^�𕜌�����
        public override int ReadByte(byte[] getByte, int offset)
        {
            // �ʒu
            float px = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float py = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float pz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.position = new Vector3(px, py, pz);

            // �ړ����x
            _playerController.Speed = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);

            // �p��
            float rx = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float ry = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rw = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.rotation = new Quaternion(rx, ry, rz, rw);

            // ID
            _id = getByte[offset]; offset += sizeof(byte);

            // ���̓}�X�N���N���A
            //_inputMask = 0;


            // �ʒu�Ǝp����ۑ�
            _lastPosition = _obj.transform.position;
            _lastDirection = _obj.transform.rotation;

            return offset;
        }
    }


}
