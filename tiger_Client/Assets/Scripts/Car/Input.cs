using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class Input : MonoBehaviour
{
    // インプットアクション
    private PlayerInputAction _input;
    // ステアリング入力値
    private Vector2 _steeringValue;
    // 加速判定
    private bool _isAcceleration = false;
    // ステアリングのX値
    private float _steering;
    // ブレーキ判定
    private bool _isBrake = false;
    // ドリフト判定
    private bool _isDrift = false;

    private bool _isReset = false;

    public Vector3 SteeringValue { get { return _steeringValue; } }
    public bool IsAcceleration { get { return _isAcceleration; } }
    public float Steering { get { return _steering; } }
    public bool IsBrake { get { return _isBrake; } }
    public bool IsDrift {  get { return _isDrift; } }
    public bool IsReset { get { return _isReset; } }

    void Start()
    {
        _input = new PlayerInputAction();
        
        _input.Player.Accelerator.performed += OnAcceleration;
        _input.Player.Accelerator.canceled += OnAcceleration;

        _input.Player.Steering.started += OnSteering;
        _input.Player.Steering.performed += OnSteering;
        _input.Player.Steering.canceled += OnSteering;

        _input.Player.Brake.performed += OnBrake;
        _input.Player.Brake.canceled += OnBrake;

        _input.Player.Drift.performed += OnDrift;
        _input.Player.Drift.canceled += OnDrift;

        _input.Player.Reset.performed += OnReset;
        _input.Player.Reset.canceled += OnReset;

        _input.Enable();
    }

    private void OnAcceleration(InputAction.CallbackContext context)
    {
        _isAcceleration = context.ReadValue<float>() > 0;
       
    }

    private void OnSteering(InputAction.CallbackContext context)
    {
        _steeringValue = context.ReadValue<Vector2>();
      
    }

    private void OnBrake(InputAction.CallbackContext context)
    {
        _isBrake = context.ReadValue<float>() > 0;
    }

    private void OnDrift(InputAction.CallbackContext context)
    {
        _isDrift = context.ReadValue<float>() > 0;
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        _isReset = context.ReadValue<float>() >0;   
    }
    private void OnDestroy()
    {
        _input?.Dispose();
    }
}
