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
        // タイムアウトまでのフレーム数
        private const int c_timeout = 600;
        private int _timeout = c_timeout;
        // 受信済みフレーム
        private byte _receiveTimer = 0;
        // 処理済みフレーム
        private byte _execTimer = 0;
        private readonly object _lockObject = new object();
        private List<PacketData> _packets = new List<PacketData>();
        private IPEndPoint _endPoint;
        // プレイヤーGameObject
        private GameObject _obj = null;
        // プレイヤー操作クラス
        private PlayerController _playerController = null;
        // 前フレームの位置
        protected Vector3 _lastPos = Vector3.zero;
        // 前フレームの姿勢
        protected Quaternion _lastDir = Quaternion.identity;
        // ボタン入力をマスクにしたもの
        private PacketData.eInputMask _inputMask = 0;
        // 状態を表すマスク
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
