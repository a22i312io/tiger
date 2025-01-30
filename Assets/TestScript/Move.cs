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
    private float _springStrength = 5f;

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
            //position_UpperRight = new Vector3(
            //    gameObject.transform.position.x + 0.35f,
            //    gameObject.transform.position.y - 0.4f,
            //    gameObject.transform.position.z + 0.85f
            //    );

            //position_UpperLeft = new Vector3(
            //    gameObject.transform.position.x - 0.35f,
            //    gameObject.transform.position.y - 0.4f,
            //    gameObject.transform.position.z + 0.85f
            //    );
            //position_LowerRight = new Vector3(
            //    gameObject.transform.position.x + 0.35f,
            //    gameObject.transform.position.y - 0.4f,
            //    gameObject.transform.position.z - 0.85f
            //    );

            //position_LowerLeft = new Vector3(
            //    gameObject.transform.position.x - 0.35f,
            //    gameObject.transform.position.y - 0.4f,
            //    gameObject.transform.position.z - 0.85f
            //    );
            Vector3 offsetUpperRight = new Vector3(0.35f, -0.4f, 0.85f);
            Vector3 offsetUpperLeft = new Vector3(-0.35f, -0.4f, 0.85f);
            Vector3 offsetLowerRight = new Vector3(0.35f, -0.4f, -0.85f);
            Vector3 offsetLowerLeft = new Vector3(-0.35f, -0.4f, -0.85f);

            // �Ԃ̌��݂̈ʒu�Ɖ�]���擾
            Vector3 currentPosition = gameObject.transform.position;
            Quaternion currentRotation = gameObject.transform.rotation;

            // ���[�J�����W�n�̃I�t�Z�b�g�����[���h���W�n�ɕϊ�
            position_UpperRight = currentPosition + currentRotation * offsetUpperRight;
            position_UpperLeft = currentPosition + currentRotation * offsetUpperLeft;
            position_LowerRight = currentPosition + currentRotation * offsetLowerRight;
            position_LowerLeft = currentPosition + currentRotation * offsetLowerLeft;

            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_UpperRight);
            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_UpperLeft);
            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_LowerRight);
            //_rb.AddForceAtPosition(Vector3.up * _gravityForce / 4, position_LowerLeft);
            ApplyLiftForce(position_UpperRight);
            ApplyLiftForce(position_UpperLeft);
            ApplyLiftForce(position_LowerRight);
            ApplyLiftForce(position_LowerLeft);

            //ApplyDamping();
        }
    }


    //private void ApplyLiftForce(Vector3 position)
    //{



    //    Ray ray = new Ray(position, -gameObject.transform.up);
    //    Debug.DrawRay(position, Vector3.down, Color.red, 2.0f);
    //    if (Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayer))
    //    {
    //        float forceMagnitude;
    //        //Debug.Log(hit.distance);

    //        if (hit.distance < _maxDistance)
    //        {
    //            forceMagnitude = _gravityForce / 4 + (_maxDistance - hit.distance);

    //        }
    //        else if (hit.distance > _maxDistance)
    //        {
    //            // �ނ荇���_�ȏ�ł͗͂�����
    //            forceMagnitude = _gravityForce / 4 - (hit.distance - _maxDistance) * _dampingForce;
    //        }
    //        else
    //        {
    //            // ���S�ɒނ荇���_�ɂ���ꍇ�A�͂����̂܂�
    //            forceMagnitude = _gravityForce / 4;
    //        }
    //        // else if (hit.distance == _maxDistance)
    //        // {
    //        //Debug.Log("aaa");
    //        // �ނ荇���_�ȏ�ł͗͂�����
    //        //_rb.linearVelocity = Vector3.zero;
    //        //_rb.isKinematic = true;
    //        //_rb.isKinematic = false;
    //        //forceMagnitude = _gravityForce / 4;
    //        //}
    //        //else
    //        //    {
    //        //       forceMagnitude = _gravityForce / 4;
    //        //  }

    //        forceMagnitude = Mathf.Clamp(forceMagnitude, 0, _gravityForce);
    //        _rb.AddForceAtPosition(Vector3.up * forceMagnitude, position);
    //    }

    //}


    private void ApplyLiftForce(Vector3 position)
    {

        Ray ray = new Ray(position, -gameObject.transform.up);
        Debug.DrawRay(position, -gameObject.transform.up, Color.red, 2.0f);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayer))
        {
            
            //Vector3 maxposition = new Vector3(position.x, position.y - _maxDistance, position.z);
            //Vector3 displacement = maxposition - hit.point;
            //Vector3 springForce = -springStrength * displacement;
            //Vector3 dampingForce = -_dampingForce * _rb.linearVelocity;
            //Vector3 force = (springForce + dampingForce);
            //_rb.AddForceAtPosition(force, position);

            // �n�ʂƂ̋���
            float distance = _maxDistance - hit.distance;

            Vector3 groundNormal = hit.normal;
            float verticalVelocity = Vector3.Dot(_rb.GetPointVelocity(position), groundNormal);

            float springForce = _springStrength * distance;
            float dampingForce = _dampingForce * verticalVelocity;

            float force = springForce - dampingForce;
            _rb.AddForceAtPosition(groundNormal * force, position);
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
