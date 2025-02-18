using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEditor.U2D.ScriptablePacker;

namespace Car
{
    public abstract class PlayerBase
    {
        // �^�C���A�E�g�܂ł̃t���[����
        private const int c_timeout = 600;
        private int _timeout = c_timeout;
        // ��M�ς݃t���[��
        private byte _receiveTimer = 0;
        // �����ς݃t���[��
        private byte _execTimer = 0;
        private readonly object _lockObject = new object();
        private List<PacketData> _packets = new List<PacketData>();
        private IPEndPoint _endPoint;
        // �v���C���[GameObject
        private GameObject _obj = null;
        // �v���C���[����N���X
        private PlayerController _playerController = null;
        // �O�t���[���̈ʒu
        protected Vector3 _lastPos = Vector3.zero;
        // �O�t���[���̎p��
        protected Quaternion _lastDir = Quaternion.identity;
        // �{�^�����͂��}�X�N�ɂ�������
        private PacketData.eInputMask _inputMask = 0;
        // ��Ԃ�\���}�X�N
        protected PacketData.eStateMask _stateMask = 0;
        public byte Id {  get { return _id; } set { _id = value; } }
        public GameObject Obj { get { return _obj; } set { _obj = value; } }
        

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
