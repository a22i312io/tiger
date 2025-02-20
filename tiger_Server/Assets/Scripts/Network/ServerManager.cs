using Car;
using Car.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using UnityEditor.PackageManager;

public class Server : MonoBehaviour
{
    public GameObject _playerPrefab;
    public GameObject _bulletPrefab;

    // UDP�ʐM�̂��߂̃N���X
    private UdpClient _udpClient = null;
    // �g�p����Port(Server)
    private int _portServer = 53724;
    // ���b�N�I�u�W�F�N�g�i�v���C���[���X�g�j
    private readonly object _lockObject = new object();
    // �󂢂Ă���ID�̃��X�g
    private List<byte> _freeIds = new List<byte>(Byte.MaxValue - 1);
    // ID��Key�ɂ���Player��Dictionary
    private Dictionary<byte, Player> _players = new Dictionary<byte, Player>();
   

    void OnDestroy()
    {
        _players.Clear();

        if (_udpClient != null)
        {
            _udpClient.Close();
            _udpClient.Dispose();
            _udpClient = null;
        }
    }

    void Start()
    {
        // �󂢂Ă���ID�̃��X�g��0�`255�����ɋl�߂Ă���
        for (byte i = 0; i < _freeIds.Capacity; i++)
        {
            _freeIds.Add(i);
        }

        // �҂��󂯂��邽�߂�UdpClient���쐬
        _udpClient = new UdpClient(AddressFamily.InterNetworkV6);
        _udpClient.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, 0);
        IPEndPoint localEP = new IPEndPoint(IPAddress.IPv6Any, _portServer);
        _udpClient.Client.Bind(localEP);

        // �҂��󂯊J�n�i��M���������Ƃ�OnReceived���Ă΂��j
        _udpClient.BeginReceive(OnReceived, _udpClient);
    }

    // ���̊Ԋu�Ń��[�v�����邽�߂�Update�ł͂Ȃ�FixedUpdate���g��
    void FixedUpdate()
    {
        lock (_lockObject)
        {
            update();
        }
    }

    void update()
    {
        //Debug.Log($"[Client] Sending Data: {_paket.GetBytes().Length} bytes");

        foreach (KeyValuePair<byte, Player> player in _players)
        {
            player.Value.Update(_playerPrefab, transform);

            //if (player.Value.IsFire && _bullets.Count < 255)
            //{
            //    // �e�ۂ��쐬
            //    GameObject bullet = GameObject.Instantiate(_bulletPrefab);
            //    {
            //        // �Փ˔���p���C���[���v���C���[���v���C���[�Ɠ����ɂ���
            //        bullet.layer = player.Value.Obj.layer;
            //        // �ʒu�Ǝp���̓v���C���[����R�s�[
            //        bullet.transform.position = player.Value.Obj.transform.position;
            //        bullet.transform.rotation = player.Value.Obj.transform.rotation;
            //        // �����x��^����
            //        Vector3 force = bullet.transform.rotation * new Vector3(0, 0, 1000);
            //        bullet.GetComponent<Rigidbody>().AddForce(force);
            //    }
            //    // �e�ۊǗ����X�g�ɓo�^
            //    _bullets.Add(bullet);
            //}
        }
        {
            //// �폜���X�g
            //List<GameObject> listDelete = new List<GameObject>();

            //// �S�Ă̒e��
            //foreach (var bullet in _bullets)
            //{
            //    BulletController bc = bullet.GetComponent<BulletController>();
            //    // �e�ۂ̎������s���Ă�����폜���X�g�ɓo�^����
            //    if (bc.LifeTime < 0.0f)
            //    {
            //        listDelete.Add(bullet);
            //    }
            //}

            //// �폜���X�g�ɋl�܂ꂽ�e�ۂ��폜����
            //foreach (var item in listDelete)
            //{
            //    Destroy(item.gameObject);
            //    _bullets.Remove(item);
            //}
        }
        {
            List<byte> listRemove = new List<byte>();

            foreach (KeyValuePair<byte, Player> player in _players)
            {
                // �ʐM���^�C���A�E�g�����v���C���[���폜���X�g�ɋl��
                if (player.Value.DecTimeout())
                {
                    player.Value.OnDestroy();
                    listRemove.Add(player.Key);
                }
            }

            // �폜���X�g�̃v���C���[���폜
            foreach (byte key in listRemove)
            {
                Debug.Log($"disconnect {key}");
                _players.Remove(key);
                _freeIds.Add(key);
            }
        }
        {
            // ���M�p���X�g
            List<byte> list = new List<byte>();

            // �v���C���[��
            list.Add((byte)_players.Count);
            // �e�ې�
            //list.Add((byte)_bullets.Count);

            // �v���C���[�������X�g�ɋl��
            foreach (KeyValuePair<byte, Player> player in _players)
                list.AddRange(player.Value.GetBytes(player.Key));

            //// �e�ۏ������X�g�ɋl��
            //foreach (GameObject bullet in _bullets)
            //    list.AddRange(bullet.GetComponent<BulletController>().GetBytes());

            // �S�v���C���[�ɑ��M
            for (byte i = 0; i < _players.Count; i++)
                _udpClient.Send(list.ToArray(), list.Count, _players[i].EndPoint);
        }

    }

    private void OnReceived(IAsyncResult result)
    {
        UdpClient getUdp = (UdpClient)result.AsyncState;
        IPEndPoint ipEnd = null;

        if (getUdp.Client == null)
        {
            return;
        }

        if (_freeIds.Count <= 0)
        {
            // ��ID������
            goto labelEnd;
        }

        byte[] getByte;
        try
        {
            // getByte�Ɏ󂯎�����f�[�^��ǂݏo��
            getByte = getUdp.EndReceive(result, ref ipEnd);
        }
        catch
        {
            // �ǂݎ��G���[
            goto labelEnd;
        }

        // �ŏ��̐ڑ��i1byte�ȉ��̃f�[�^�j�Ȃ�
        if (getByte.Length <= 1)
        {
            byte id;

            // Player�쐬
            Player player = new Player(ipEnd);

            // id�̔����������ɎQ�Ƃ��Ȃ��悤�Ƀ��b�N����
            lock (_lockObject)
            {
                // ��id���X�g���犄��U��
                id = _freeIds[0];
                // ����U����id�����X�g���甲��
                _freeIds.RemoveAt(0);
                // Player��id����擾�ł���悤��Dictionary�ɓo�^
                _players.Add(id, player);
            }

            Debug.Log($"ip {ipEnd.Address.ToString()}, port {ipEnd.Port.ToString()}, id {id}");

            // ���̃v���C���[�Ɋ��蓖�Ă�ꂽid��ԐM����
            byte[] data = new byte[1] { id };
            _udpClient.Send(data, data.Length, ipEnd);
        }
        else
        {
            int offset = 1;
            byte userId = getByte[0];

            lock (_lockObject)
            {
                if (_players.ContainsKey(userId))
                {
                    PacketData packet0 = new PacketData();
                    PacketData packet1 = new PacketData();
                    PacketData packet2 = new PacketData();

                    offset = packet0.ReadBytes(getByte, offset);
                    offset = packet1.ReadBytes(getByte, offset);
                    offset = packet2.ReadBytes(getByte, offset);

                    _players[userId].ResetTimeout();
                    _players[userId].Push(packet0);
                    _players[userId].Push(packet1);
                    _players[userId].Push(packet2);
                }
            }
        }

    labelEnd:
        getUdp.BeginReceive(OnReceived, getUdp);
    }
}