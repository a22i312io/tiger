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
        // �D�悷��C���^�[�t�F�[�X�𕶎���Ŏw��i�Ȃ���΋󔒁j
        [SerializeField] private string _priorityIntafaceName;
        // �h���b�v�_�E��
        [SerializeField] private TMP_Dropdown _selectNetworkInterfaceDropdown;
        // ���p�\�ȋ@��̃��X�g
        private List<NetworkInterfaceData> _networkInterfaces;

        public GameObject _playerPrefab;

        private List<PlayerBase> _players = new List<PlayerBase>();

        private Input _playerInput;
        private Player _offlinePlayer;
        //private CinemachineVirtualCamera _cinemachineVirtualCamera1;
        private GameObject _cameraTarget;

        // UDP�ʐM�̂��߂̃N���X
        private UdpClient _udpClient;
        // Server��IpAddress���w�肷��ilocalhost�͎������g�j
        [SerializeField] private string host = "localhost";
        // �g�p����Port(Server)
        private int _portServer = 53724;
        // �g�p����Port(����:Client)
        private int _portClient = 53725;
        // ���M�o�b�t�@
        //UdpBuffer _udpTransmitter = new UdpBuffer();
        // ��M�o�b�t�@
        UdpBuffer _udpReceiver = new UdpBuffer();

        // ��M�������[�U�[ID
        private byte _userIdWork = Byte.MaxValue;
        // ���[�U�[ID
        private byte _userId = Byte.MaxValue;
        // �^�C�}�[�i�T�[�o�[���Ńp�P�b�g���X�̔���ȂǂɎg�p����j
        private byte _globalTimer = 0;
        // ���M�p�P�b�g
        private Packet _paket = new Packet();
        void Start()
        {
            // ���p�\�ȋ@��̃��X�g���擾
            _networkInterfaces = NetworkInterfaceData.GetIpAddress();
            {
                // �h���b�v�_�E�����X�g�ɋ@�햼�𔽉f
                foreach (var ni in _networkInterfaces)
                    _selectNetworkInterfaceDropdown.options.Add(new TMP_Dropdown.OptionData(ni.InterfaceName));

                // �D�悷��C���^�[�t�F�[�X��������
                int index = _selectNetworkInterfaceDropdown.options.FindIndex((options) => { return options.text == _priorityIntafaceName; });
                // ���������ꍇ�͂����I��
                if (index > 0)
                {
                    _selectNetworkInterfaceDropdown.value = index;
                    _ipAddress = _networkInterfaces[index].IpAddress.ToString();
                    _macAddress = _networkInterfaces[index].MacAddress.ToString();
                }
                // ������Ȃ������ꍇ�͂O�Ԃ�I��
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

            // UDP����M�̂��߂̃N���X���쐬����
            _udpClient = new UdpClient(_portClient);
            {
                // 1byte�̃_�~�[�f�[�^�𑗐M���ăT�[�o�[��ID��U���Ă��炤
                byte[] message = { 0 };
                _udpClient.Send(message, message.Length, host, _portServer);
            }

            // �҂��󂯊J�n�i��M���������Ƃ�OnReceived���Ă΂��j
            _udpClient.BeginReceive(OnReceived, _udpClient);
        }

        // Update is called once per frame
        void Update()
        {
            // ��M�������[�U�[ID���̗p����
            if (_userId != _userIdWork)
            {
                _userId = _userIdWork;
            }

            lock (_udpReceiver.LockObject)
            {
                // ��M�o�b�t�@�Ƀf�[�^������
                if (_udpReceiver.Buffer != null)
                {
                    // �v���C���[��
                    int playerNum = _udpReceiver.Buffer[0];

                    int offset = 1;

                    // �v���C���[������Ȃ��ꍇ�͕�[����
                    for (int i = _players.Count; i < playerNum; i++)
                        _players.Add(new NetPlayer(_playerPrefab, this.transform));

                    // �v���C���[����ǂݍ���
                    for (int i = 0; i < playerNum; i++)
                        offset = _players[i].ReadByte(_udpReceiver.Buffer, offset);

                    // �v���C���[���X�g�������Ƃ��͍폜
                    if (playerNum < _players.Count)
                    {
                        for (int i = playerNum; i < _players.Count; i++)
                        {
                            GameObject.Destroy(_players[i].Obj);
                            _players[i].Obj = null;
                        }
                        _players.RemoveRange(playerNum, _players.Count - playerNum);
                    }
                    // ��M�o�b�t�@�����
                    _udpReceiver.Buffer = null;
                }
            }

            bool isNetwork = _userId != byte.MaxValue;

            // �l�b�g���[�N�ڑ����̓��X�g���玩����ID��T���A������v���C���[�Ƃ��Ďg��
            PlayerBase player = !isNetwork ? null : _players.Find(x => x.Id == _userId);
            // ID��������Ȃ������ꍇ��I�t���C���̂Ƃ���_offlinePlayer���v���C���[�Ƃ��Ďg��
            if (player == null)
            {
                player = _offlinePlayer;
                isNetwork = false;
            }
            // �l�b�g���[�N�ڑ��̗L����_offlinePlayer�𖳌��i�L���j�ɂ���
            _offlinePlayer.SetActive(!isNetwork);

            // Player�̍X�V
            player.Update(_playerInput);


            // �^�C�}�[�A���́A�t�H�[�X���p�P�b�g�ɋl��
            _paket.Push(_globalTimer, player.InputMask, player.Force);

            // ID���U���Ă�����T�[�o�[�Ƀf�[�^�𑗐M
            if (_userId != Byte.MaxValue)
            {
                List<byte> buffer = new List<byte>();

                // ID
                buffer.Add(_userId);
                // �p�P�b�g
                buffer.AddRange(_paket.GetBytes());

                // ���M
                _udpClient.Send(buffer.ToArray(), buffer.Count, host, _portServer);
            }

            {
                //// �J�����̒����_�Ƀv���C���[�ʒu���R�s�[
                //_cameraTarget.transform.position = player.Obj.transform.position;

                //// �v���C���[�؂�ւ��⃊�Z�b�g���������Ă�����
                //if (/*player.IsChangeCharacter | */player.IsReset)
                //{
                //    Quaternion q = player.Obj.transform.rotation;
                //    //Vector3 startPos = player.Obj.transform.position + q * new Vector3(0, 3, -5);
                //    // �J�����𑦎��v���C���[����ɐ؂�ւ�
                //   // ��Ԃ𖳌��ɂ���
                //    // _cinemachineVirtualCamera1.ForceCameraPosition(startPos, q);
                //    //_cinemachineVirtualCamera1.PreviousStateIsValid = false;
                //}
                //else
                //{
                //    // ��Ԃ�L���ɂ���
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

            // ���b�N���Ă����M����Byte�z���ǂݎ��
            lock (_udpReceiver.LockObject)
            {
                try
                {
                    _udpReceiver.Buffer = getUdp.EndReceive(result, ref ipEnd);

                    // 1byte�̂Ƃ���ID�����o���Ĕj��
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