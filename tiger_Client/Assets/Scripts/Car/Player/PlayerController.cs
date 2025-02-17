using Car;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    // �ڒn�̏�ԁitrue�Ȃ�W�����v�ł���j
    private bool _isGround = false;
    // �v���C���[���f���̎��
    //private Player.eKind _kind = Player.eKind.police;
    // �ړ��i�A�j���[�V�����́j���x
    private float _speed = 0.0f;
    // �v���C���[�𓮂������߂�Rigidbody�ɉ������
    private Vector3 _force = Vector3.zero;
    // Model�I�u�W�F�N�g
    private GameObject model = null;
    // Model�I�u�W�F�N�g�̎q�iTT_demo_police��TT_demo_zombie�m�[�h�j
    private List<GameObject> children = new List<GameObject>();

    public bool IsGround { get { return _isGround; } }

    //public Player.eKind Kind { get { return _kind; } set { _kind = value; } }
    public float Speed { set { _speed = value; } }
    public Vector3 Force { get { return _force; } set { _force = value; } }

    void Start()
    {
        // Model�m�[�h��T��
        model = transform.Find("Model").gameObject;
        if (model)
        {
            // Model�m�[�h�̎q��children�ɋl��
            Transform transforms = model.GetComponentInChildren<Transform>();
            foreach (Transform obj in transforms)
            {
                children.Add(obj.gameObject);
            }
        }
    }

    void Update()
    {
        foreach (GameObject obj in children)
        {
            // ���݂�Kind�ipolice�܂���zombie�j�𖼑O�Ɋ܂ރm�[�h������L���ɂ���i���������j
            //obj.SetActive(obj.name.Contains(_kind.ToString()));

            // �L�������ꂽ�m�[�h�ɃA�j���[�V�������x�𔽉f����
            if (obj.activeSelf)
            {
                Animator anim = obj.GetComponent<Animator>();
                if (anim)
                {
                    // 0.25�悵�Ă���̂͒P�Ȃ钲���̂���
                    float animSpeed = Mathf.Pow(_speed, 0.25f);
                    anim.SetFloat("Speed", animSpeed);
                }
            }
        }
    }

    // Sleep�i�T�[�o�[�ŏՓ˔����V�~�����[�V�������s����ԁj
    public void Sleep()
    {
        // Collider�𖳌���
        GetComponent<SphereCollider>().enabled = false;
        // Rigidbody�𖳌����i�ʒu���p���w��݂̂œ������j
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // WakeUp�i���[�J���ŏՓ˔����V�~�����[�V�������s����ԁj
    public void WakeUp()
    {
        // Collider��L����
        GetComponent<SphereCollider>().enabled = true;
        // Rigidbody��L����
        GetComponent<Rigidbody>().isKinematic = false;
    }

    // �ʒu�ƌ��������Z�b�g����
    //public void Restart()
    //{
    //    // �����_���Ɍ��肷��
    //    float x = Random.value * 10.0f - 5.0f;
    //    float y = 4;
    //    float z = Random.value * 10.0f + 10.0f;

    //    // �\�����f���̎�ނɂ����z���W�̕��������߂�i���������l�j
    //    if (_kind == 0)
    //    {
    //        z = -z;
    //    }

    //    transform.position = new Vector3(x, y, z);
    //    transform.rotation = Quaternion.Euler(0.0f, _kind == 0 ? 0.0f : 180.0f, 0.0f);

    //    // �A�j���[�V�������x��������
    //    _speed = 0.0f;

    //    // �t�H�[�X��������
    //    _force = Vector3.zero;

    //    // Rigidbody�̈ړ����x�Ɖ�]���x��������
    //    GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //}

    // �Փ˒��ɌĂяo�����R�[���o�b�N
    void OnCollisionStay(Collision collision) { _isGround = true; }

    // �ՓˏI�����ɌĂяo�����R�[���o�b�N
    void OnCollisionExit(Collision collision) { _isGround = false; }


    // �v���C���[�𓮂������߂�Rigidbody�ɉ�����͂��v�Z����
    //public void UpdateForce(PlayerBase player)
    //{
    //    float baseForce = 600.0f;
    //    float jumpForce = 24.0f * 8.0f;

    //    // �J�����̑O���ƉE�����̃x�N�g��
    //    Vector3 vForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    //    Vector3 vRight = Camera.main.transform.right;

    //    Vector3 inputValue = player.InputValue;
    //    _force = baseForce * Time.deltaTime * (vForward * inputValue.y + vRight * inputValue.x);

    //    // �W�����v
    //    if (player.IsJump && _isGround)
    //    {
    //        _force.y += jumpForce;
    //    }
    //}
}