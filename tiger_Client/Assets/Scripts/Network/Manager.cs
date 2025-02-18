using Car;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Network
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] private string _ipAddress;
        [SerializeField] private string _macAddress;
        // 優先するインターフェースを文字列で指定（なければ空白）
        [SerializeField] private string _priorityIntafaceName;
        // ドロップダウン
        [SerializeField] private TMP_Dropdown _selectNetworkInterfaceDropdown;
        // 利用可能な機器のリスト
        private List<NetworkInterfaceData> _networkInterfaces;

        public GameObject _playerPrefab;

        private List<PlayerBase> _players = new List<PlayerBase>();

        private Input _playerInput;
        private Player _offlinePlayer;
        //private CinemachineVirtualCamera _cinemachineVirtualCamera1;
        private GameObject _cameraTarget;

        // UDP通信のためのクラス
        private UdpClient _udpClient;
        // ServerのIpAddressを指定する（localhostは自分自身）
        [SerializeField] private string host = "localhost";
        // 使用するPort(Server)
        private int _portServer = 53724;
        // 使用するPort(自分:Client)
        private int _portClient = 53725;
        // 送信バッファ
        //UdpBuffer _udpTransmitter = new UdpBuffer();
        // 受信バッファ
        UdpBuffer _udpReceiver = new UdpBuffer();

        // 受信したユーザーID
        private byte _userIdWork = Byte.MaxValue;
        // ユーザーID
        private byte _userId = Byte.MaxValue;
        // タイマー（サーバー側でパケットロスの判定などに使用する）
        private byte _globalTimer = 0;
        // 送信パケット
        private Packet _paket = new Packet();
        void Start()
        {
            // 利用可能な機器のリストを取得
            _networkInterfaces = NetworkInterfaceData.GetIpAddress();
            {
                // ドロップダウンリストに機器名を反映
                foreach (var ni in _networkInterfaces)
                    _selectNetworkInterfaceDropdown.options.Add(new TMP_Dropdown.OptionData(ni.InterfaceName));

                // 優先するインターフェース名を検索
                int index = _selectNetworkInterfaceDropdown.options.FindIndex((options) => { return options.text == _priorityIntafaceName; });
                // 見つかった場合はそれを選択
                if (index > 0)
                {
                    _selectNetworkInterfaceDropdown.value = index;
                    _ipAddress = _networkInterfaces[index].IpAddress.ToString();
                    _macAddress = _networkInterfaces[index].MacAddress.ToString();
                }
                // 見つからなかった場合は０番を選択
                else if (_selectNetworkInterfaceDropdown.options.Count > 0)
                {
                    _selectNetworkInterfaceDropdown.value = 0;
                    _ipAddress = _networkInterfaces[0].IpAddress.ToString();
                    _macAddress = _networkInterfaces[0].MacAddress.ToString();
                }
                _selectNetworkInterfaceDropdown.RefreshShownValue();
            }

            _playerInput = GetComponent<Input>();
            _offlinePlayer = new Player(_playerPrefab, this.transform);
            //_cinemachineVirtualCamera1 = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            _cameraTarget = GameObject.Find("CameraTarget");

            // UDP送受信のためのクラスを作成する
            _udpClient = new UdpClient(_portClient);
            {
                // 1byteのダミーデータを送信してサーバーにIDを振ってもらう
                byte[] message = { 0 };
                _udpClient.Send(message, message.Length, host, _portServer);
            }

            // 待ち受け開始（受信があったときOnReceivedが呼ばれる）
            _udpClient.BeginReceive(OnReceived, _udpClient);
        }

        // Update is called once per frame
        void Update()
        {
            // 受信したユーザーIDを採用する
            if (_userId != _userIdWork)
            {
                _userId = _userIdWork;
            }

            lock (_udpReceiver.LockObject)
            {
                // 受信バッファにデータがある
                if (_udpReceiver.Buffer != null)
                {
                    // プレイヤー数
                    int playerNum = _udpReceiver.Buffer[0];

                    int offset = 1;

                    // プレイヤーが足りない場合は補充する
                    for (int i = _players.Count; i < playerNum; i++)
                        _players.Add(new NetPlayer(_playerPrefab, this.transform));

                    // プレイヤー情報を読み込む
                    for (int i = 0; i < playerNum; i++)
                        offset = _players[i].ReadByte(_udpReceiver.Buffer, offset);

                    // プレイヤーリストが多いときは削除
                    if (playerNum < _players.Count)
                    {
                        for (int i = playerNum; i < _players.Count; i++)
                        {
                            GameObject.Destroy(_players[i].Obj);
                            _players[i].Obj = null;
                        }
                        _players.RemoveRange(playerNum, _players.Count - playerNum);
                    }
                    // 受信バッファを解放
                    _udpReceiver.Buffer = null;
                }
            }

            bool isNetwork = _userId != byte.MaxValue;

            // ネットワーク接続中はリストから自分のIDを探し、それをプレイヤーとして使う
            PlayerBase player = !isNetwork ? null : _players.Find(x => x.Id == _userId);
            // IDが見つからなかった場合やオフラインのときは_offlinePlayerをプレイヤーとして使う
            if (player == null)
            {
                player = _offlinePlayer;
                isNetwork = false;
            }
            // ネットワーク接続の有無で_offlinePlayerを無効（有効）にする
            _offlinePlayer.SetActive(!isNetwork);

            // Playerの更新
            player.Update(_playerInput);


            // タイマー、入力、フォースをパケットに詰む
            _paket.Push(_globalTimer, player.InputMask, player.Force);

            // IDが振られていたらサーバーにデータを送信
            if (_userId != Byte.MaxValue)
            {
                List<byte> buffer = new List<byte>();

                // ID
                buffer.Add(_userId);
                // パケット
                buffer.AddRange(_paket.GetBytes());

                // 送信
                _udpClient.Send(buffer.ToArray(), buffer.Count, host, _portServer);
            }

            {
                //// カメラの注視点にプレイヤー位置をコピー
                //_cameraTarget.transform.position = player.Obj.transform.position;

                //// プレイヤー切り替えやリセットが発生していたら
                //if (/*player.IsChangeCharacter | */player.IsReset)
                //{
                //    Quaternion q = player.Obj.transform.rotation;
                //    //Vector3 startPos = player.Obj.transform.position + q * new Vector3(0, 3, -5);
                //    // カメラを即時プレイヤー後方に切り替え
                //   // 補間を無効にする
                //    // _cinemachineVirtualCamera1.ForceCameraPosition(startPos, q);
                //    //_cinemachineVirtualCamera1.PreviousStateIsValid = false;
                //}
                //else
                //{
                //    // 補間を有効にする
                //    //_cinemachineVirtualCamera1.PreviousStateIsValid = true;
                //}
            }

            _globalTimer++;
        }

        private void OnReceived(System.IAsyncResult result)
        {
            UdpClient getUdp = (UdpClient)result.AsyncState;
            IPEndPoint ipEnd = null;

            if (getUdp.Client == null)
            {
                return;
            }

            // ロックしてから受信したByte配列を読み取る
            lock (_udpReceiver.LockObject)
            {
                try
                {
                    _udpReceiver.Buffer = getUdp.EndReceive(result, ref ipEnd);

                    // 1byteのときはIDを取り出して破棄
                    if (_udpReceiver.Buffer.Length <= 1)
                    {
                        _userIdWork = _udpReceiver.Buffer[0];
                        _udpReceiver.Buffer = null;
                    }
                }
                catch
                {
                    _udpReceiver.Buffer = null;
                }
            }

            getUdp.BeginReceive(OnReceived, getUdp);
        }
    }
}