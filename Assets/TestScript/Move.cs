using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour
{
    //�n�ʂƂ̍ő勗��
    [SerializeField] private float _maxDistance;
    //������
    [SerializeField] private float _dampingForce;
    //������
    [SerializeField] private float _accelerationForce;
    //�ō����x
    [SerializeField] private float _maxSpeed;
    //�n�ʂ𔻒肷�邽�߂̃��C���[
    [SerializeField] private LayerMask _groundLayer;
    //�X�s�[�h���[�^�[
    [SerializeField] private TMP_Text _speedText;
    private Rigidbody _rb;

    private Vector3 _lastPosition;
    private Vector3 _velocity;

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

        _rb.angularVelocity *= 0.9f;
        _velocity = _rb.linearVelocity;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            
            Vector3 offsetUpperRight = new Vector3(1f, -0.4f, 1.85f);
            Vector3 offsetUpperLeft = new Vector3(-1f, -0.4f, 1.85f);
            Vector3 offsetLowerRight = new Vector3(1f, -0.4f, -1.85f);
            Vector3 offsetLowerLeft = new Vector3(-1f, -0.4f, -1.85f);

            // �Ԃ̌��݂̈ʒu�Ɖ�]���擾
            Vector3 currentPosition = gameObject.transform.position;
            Quaternion currentRotation = gameObject.transform.rotation;

            // ���[�J�����W�n�̃I�t�Z�b�g�����[���h���W�n�ɕϊ�
            position_UpperRight = currentPosition + currentRotation * offsetUpperRight;
            position_UpperLeft = currentPosition + currentRotation * offsetUpperLeft;
            position_LowerRight = currentPosition + currentRotation * offsetLowerRight;
            position_LowerLeft = currentPosition + currentRotation * offsetLowerLeft;

            ApplyLiftForce(position_UpperRight);
            ApplyLiftForce(position_UpperLeft);
            ApplyLiftForce(position_LowerRight);
            ApplyLiftForce(position_LowerLeft);

            //ApplyDamping();
            Vector3 frontDirection = currentRotation * Vector3.forward;
            float speedInTargetDirection = Vector3.Dot(_rb.linearVelocity, frontDirection);
            _speedText.text = $"Speed: {speedInTargetDirection:F2} m/s";

            if (Input.GetKey(KeyCode.A))
            {
               
                
                //_rb.AddForce(0, 0, 5);
                ApplyAccelerationForce(frontDirection);

            }
        }
    }
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

    private void ApplyAccelerationForce(Vector3 position)
    {
        float speed = _rb.linearVelocity.magnitude;

        //_rb.linearDamping = (speed > _maxSpeed) ? 2f:0.3f;

        if (speed > _maxSpeed)
        {
            _rb.linearDamping = 0.6f;
        }
        else
        {
            _rb.linearDamping = 0.1f; // �ʏ펞�� drag �ɖ߂�
        }

        _rb.AddForce(position * _accelerationForce);

        //if (_velocity.magnitude < 100)
        //{
        //    _rb.AddForce(position * _accelerationForce);
        //}
        //else
        //{
        //    _rb.AddForce(position * 100);
        //}

        

        //// �ڕW�����i�O�����j�ɉ������������v�Z
        //float speedInTargetDirection = Vector3.Dot(velocity, targetDirection.normalized);

        //// �K�v�ȑ��x�Ƃ̍����v�Z
        //float speedDifference = targetSpeed - speedInTargetDirection;

        //// �����傫����΁A�͂������Ē���
        //if (Mathf.Abs(speedDifference) > 0.1f) // �덷������������
        //{
        //    Vector3 force = targetDirection.normalized * speedDifference;
        //    _rb.AddForce(force, ForceMode.Force);
        //}





        _lastPosition = transform.position;

    }
    void ApplyDamping()
    {
        Vector3 velocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
        // �_���s���O�͂͑��x�ɔ�Ⴕ�ċt�����ɂ�����
        Vector3 DampingForce = -_dampingForce * velocity;

        // ���̑S�̂Ƀ_���s���O��K�p
        _rb.AddForce(DampingForce, ForceMode.Force);

        _lastPosition = transform.position;

    }
}
