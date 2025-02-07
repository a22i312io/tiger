using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] private float _turnSpeed = 2f;

    [SerializeField] private float _smoothFactor = 5f;
    private float _currentTurn;
    private Rigidbody _rb;

    private Vector3 _lastPosition;
    private Vector3 _velocity;

    private float _gravityForce;
    private float _springStrength = 5f;

    private Vector3 position_UpperRight;
    private Vector3 position_UpperLeft;
    private Vector3 position_LowerRight;
    private Vector3 position_LowerLeft;

    private bool _isAccerelation = false;

    private float _steering;

    private PlayerInputAction _gameInputs;
    private Vector2 _moveInputValue;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _lastPosition = transform.position;

        _gravityForce = Physics.gravity.magnitude * _rb.mass;

        _rb.angularVelocity *= 0.9f;
        _velocity = _rb.linearVelocity;

        _gameInputs = new PlayerInputAction();

        _gameInputs.Player.Steering.started += OnSteering;
        _gameInputs.Player.Steering.performed += OnSteering;
        _gameInputs.Player.Steering.canceled += OnSteering;

        _gameInputs.Player.Accelerator.performed += OnAcceleration;
        _gameInputs.Player.Accelerator.canceled += OnAcceleration;
        _gameInputs.Enable();
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

            if (_isAccerelation)
            {


                //_rb.AddForce(0, 0, 5);
                ApplyAccelerationForce(frontDirection);

            }
            if (Input.GetKey(KeyCode.A))
            {


                //_rb.AddForce(0, 0, 5);
                ApplyAccelerationForce(frontDirection);

            }

            ApplySteeringForce();
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

    private void OnAcceleration(InputAction.CallbackContext context)
    {
        _isAccerelation = context.ReadValue<float>() > 0;
    }
    private void ApplySteeringForce()
    {


        //if (_rb)
        //{
            
        //    transform.Rotate(Vector3.up * _steering * _turnSpeed );
        //}


        //if (_rb)
        //{
        //    Debug.Log("a");
        //    _rb.AddForce(10, 0, 0);
        //}

        float targetTurn = _steering * _turnSpeed;

        // 徐々にターン速度を目標値に近づける
        _currentTurn = Mathf.Lerp(_currentTurn, targetTurn, Time.deltaTime * _smoothFactor);

        // Y軸回転
        transform.Rotate(Vector3.up * _currentTurn * Time.deltaTime);






    }
    private void OnSteering(InputAction.CallbackContext context)
    {
        Vector2 steeringInput = context.ReadValue<Vector2>();

        _steering = steeringInput.x;
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
