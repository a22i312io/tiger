using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour
{
    //�n�ʂƂ̍ő勗��
    [SerializeField] private float _maxDistance;
    //������
    [SerializeField] private float _dampingForce;
    //�n�ʂ𔻒肷�邽�߂̃��C���[
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;

    private Vector3 _lastPosition;

    private float _gravityForce;

    private Vector3 position_UpperRight;
    private Vector3 position_UpperLeft;
    private Vector3 position_LowerRight;
    private Vector3 position_LowerLeft;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Debug.Log(Physics.gravity.magnitude);
        _lastPosition = transform.position;

        _gravityForce = Physics.gravity.magnitude * _rb.mass;

         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(0, 0, 5);
        }
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            position_UpperRight = new Vector3(
                transform.position.x + 0.35f,
                transform.position.y - 0.4f,
                transform.position.z + 0.85f
                );

            position_UpperLeft = new Vector3(
                transform.position.x - 0.35f,
                transform.position.y - 0.4f,
                transform.position.z + 0.85f
                );
            position_LowerRight = new Vector3(
                transform.position.x + 0.35f,
                transform.position.y - 0.4f,
                transform.position.z - 0.85f
                );

            position_LowerLeft = new Vector3(
                transform.position.x - 0.35f,
                transform.position.y - 0.4f,
                transform.position.z - 0.85f
                );

            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_UpperRight);
            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_UpperLeft);
            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_LowerRight);
            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_LowerLeft);
            ApplyLiftForce(position_UpperRight);
            ApplyLiftForce(position_UpperLeft);
            ApplyLiftForce(position_LowerRight);
            ApplyLiftForce(position_LowerLeft);

            ApplyDamping();
        }
    }

    private void ApplyLiftForce(Vector3 position)
    {
        
            
            
            Ray ray = new Ray(position, Vector3.down);
        Debug.DrawRay(position, Vector3.down, Color.red, 2.0f);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayer))
            {
                float forceMagnitude;
            Debug.Log(hit.distance);

            if (hit.distance < _maxDistance)
                    {
                        forceMagnitude = _gravityForce / 4 + (_maxDistance - hit.distance);

                    }
            else if (hit.distance > _maxDistance)
            {
                // �ނ荇���_�ȏ�ł͗͂�����
                forceMagnitude = _gravityForce / 4 - (hit.distance - _maxDistance) * _dampingForce;
            }
            else
            {
                // ���S�ɒނ荇���_�ɂ���ꍇ�A�͂����̂܂�
                forceMagnitude = _gravityForce / 4;
            }
            //else if (hit.distance == _maxDistance)
            //{
            //Debug.Log("aaa");
            //    // �ނ荇���_�ȏ�ł͗͂�����
            //    _rb.linearVelocity = Vector3.zero;
            //_rb.isKinematic = true;
            //_rb.isKinematic = false;
            //forceMagnitude = _gravityForce / 4;
            //}
            //else
            //    {
            //        forceMagnitude = _gravityForce / 4;
            //    }

            forceMagnitude = Mathf.Clamp(forceMagnitude, 0, _gravityForce);
                _rb.AddForceAtPosition(Vector3.up * forceMagnitude, position);
            }
        
    }
    void ApplyDamping()
    {
        Vector3 velocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
        // �_���s���O�͂͑��x�ɔ�Ⴕ�ċt�����ɂ�����
        Vector3 DampingForce = -_dampingForce * velocity;

        // ���̑S�̂Ƀ_���s���O��K�p
        _rb.AddForce(DampingForce, ForceMode.Acceleration);

        _lastPosition = transform.position;

    }
}
