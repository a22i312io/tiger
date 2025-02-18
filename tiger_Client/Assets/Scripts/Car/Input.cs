using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class Input : MonoBehaviour
{
    // インプットアクション
    private PlayerInputAction _input;
    // ステアリング入力値
    private Vector2 _steeringInputValue;
    // 加速判定
    private bool _isAccerelation = false;
    // ステアリングのX値
    private float _steering;

    public bool IsAccerelation { get { return _isAccerelation; } }
    public float Steering { get { return _steering; } }

    void Start()
    {
        _input = new PlayerInputAction();
        
        _input.Player.Accelerator.performed += OnAcceleration;
        _input.Player.Accelerator.canceled += OnAcceleration;

        _input.Player.Steering.started += OnSteering;
        _input.Player.Steering.performed += OnSteering;
        _input.Player.Steering.canceled += OnSteering;

        _input.Enable();
    }

    private void OnAcceleration(InputAction.CallbackContext context)
    {
        _isAccerelation = context.ReadValue<float>() > 0;
        
    }

    private void OnSteering(InputAction.CallbackContext context)
    {
        _steeringInputValue = context.ReadValue<Vector2>();

        _steering = _steeringInputValue.x;
    }

    private void OnDestroy()
    {
        _input?.Dispose();
    }
}
