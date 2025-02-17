using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public abstract class PlayerBase
    {
        //�v���C���[ID
        protected byte _id = byte.MaxValue;
        //�v���C���[�̃Q�[���I�u�W�F�N�g
        protected GameObject _obj = null;
        //�v���C���[����N���X
        //protected Controller _playerController = null;
        //�O�t���[���̈ʒu
        protected Vector3 _lastPosition = Vector3.zero;
        //�O�t���[���̉�]
        protected Quaternion _lastDirection = Quaternion.identity;
        //�A�i���O���o�[�̓��͒l
        protected Vector3 _inputValue = Vector3.zero;
        // �{�^�����͂��}�X�N�ɂ�������
        protected PacketData.eInputMask _inputMask = 0;
        // ��Ԃ�\���}�X�N
        protected PacketData.eStateMask _stateMask = 0;
        // eStateMask���Q�Ƃ��ꂽ��true�ɂȂ�}�X�N
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

        // ��M����byte�z�񂩂�f�[�^�𕜌�����
        public override int ReadByte(byte[] getByte, int offset)
        {
            // �ʒu
            float px = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float py = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float pz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.position = new Vector3(px, py, pz);

            // �ړ����x
            _Controller.Speed = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);

            // �p��
            float rx = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float ry = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rz = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            float rw = System.BitConverter.ToSingle(getByte, offset); offset += sizeof(float);
            _obj.transform.rotation = new Quaternion(rx, ry, rz, rw);

            // ID
            _id = getByte[offset]; offset += sizeof(byte);

            //  �\�����f���̎��
            _playerController.Kind = (eKind)getByte[offset]; offset += sizeof(byte);

            // ���̓}�X�N���N���A
            //_inputMask = 0;

            // ��ԃ}�X�N�͎Q�ƍρi���łɎg���Ă�����j�㏑���A���Q�Ɓi�܂������Ă��Ȃ���΁jOR����邱�ƂőO�̏�Ԃ��c��
            if (_isStateUsed) { _stateMask = (PacketData.eStateMask)getByte[offset]; offset += sizeof(byte); }
            else { _stateMask |= (PacketData.eStateMask)getByte[offset]; offset += sizeof(byte); }

            // �ʒu�Ǝp����ۑ�
            _lastPos = _obj.transform.position;
            _lastDir = _obj.transform.rotation;

            return offset;
        }
    }


}
