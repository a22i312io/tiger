using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour
{
    //地面との最大距離
    [SerializeField] private float _maxDistance;
    //減衰力
    [SerializeField] private float _dampingForce;
    //加速力
    [SerializeField] private float _accelerationForce;
    //最高速度
    [SerializeField] private float _maxSpeed;
    //地面を判定するためのレイヤー
    [SerializeField] private LayerMask _groundLayer;
    //スピードメーター
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

            // 車の現在の位置と回転を取得
            Vector3 currentPosition = gameObject.transform.position;
            Quaternion currentRotation = gameObject.transform.rotation;

            // ローカル座標系のオフセットをワールド座標系に変換
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

            // 地面との距離
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
            _rb.linearDamping = 0.1f; // 通常時の drag に戻す
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

        

        //// 目標方向（前方向）に沿った速さを計算
        //float speedInTargetDirection = Vector3.Dot(velocity, targetDirection.normalized);

        //// 必要な速度との差を計算
        //float speedDifference = targetSpeed - speedInTargetDirection;

        //// 差が大きければ、力を加えて調整
        //if (Mathf.Abs(speedDifference) > 0.1f) // 誤差を小さくする
        //{
        //    Vector3 force = targetDirection.normalized * speedDifference;
        //    _rb.AddForce(force, ForceMode.Force);
        //}





        _lastPosition = transform.position;

    }
    void ApplyDamping()
    {
        Vector3 velocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
        // ダンピング力は速度に比例して逆方向にかかる
        Vector3 DampingForce = -_dampingForce * velocity;

        // 物体全体にダンピングを適用
        _rb.AddForce(DampingForce, ForceMode.Force);

        _lastPosition = transform.position;

    }
}
