using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Player
//{
//    // �\�����f���̎��
//    public enum eKind
//    {
//        police,
//        zombie,
//    }
//}

public class PlayerController : MonoBehaviour
{
    // �ڒn�̏�ԁitrue�Ȃ�W�����v�ł���j
    private bool _isGround = false;
    // �v���C���[���f���̎��
    Player.eKind _kind = Player.eKind.police;

    public bool IsGround { get { return _isGround; } }
    public Player.eKind Kind { get { return _kind; } set { _kind = value; } }

    void Start() { }

    void Update() { }

    // �ʒu�ƌ��������Z�b�g����
    public void Restart()
    {
        // �����_���Ɍ��肷��
        float x = Random.value * 10.0f - 5.0f;
        float y = 4;
        float z = Random.value * 10.0f + 10.0f;

        // �\�����f���̎�ނɂ����z���W�̕��������߂�i���������l�j
        if (_kind == 0)
        {
            z = -z;
        }

        transform.position = new Vector3(x, y, z);
        transform.rotation = Quaternion.Euler(0.0f, _kind == 0 ? 0.0f : 180.0f, 0.0f);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // �Փ˒��ɌĂяo�����R�[���o�b�N
    void OnCollisionStay(Collision collision) { _isGround = true; }

    // �ՓˏI�����ɌĂяo�����R�[���o�b�N
    void OnCollisionExit(Collision collision) { _isGround = false; }
}