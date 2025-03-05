using Car;
using UnityEngine;

public class Steering : MonoBehaviour
{
    private Core _core;

    private float _currentTurn;

    private Vector3 _steeringvalue;

    [SerializeField] private float _smoothFactor = 5f;
   
    private float _turnSpeed = 50f;

    public Vector3 Steeringvalue { get { return _steeringvalue; } set { _steeringvalue = value; } } 
    void Start()
    {
        _core = GetComponent<Core>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_core == null) return;
        //Debug.Log(_frontDirection);
        
        ApplySteeringForce();
    }

    private void ApplySteeringForce()
    {
        float targetTurn = _steeringvalue.x * _turnSpeed;

        // ���X�Ƀ^�[�����x��ڕW�l�ɋ߂Â���
        _currentTurn = Mathf.Lerp(_currentTurn, targetTurn, Time.deltaTime * _smoothFactor);

        // Y����]
        transform.Rotate(Vector3.up * _currentTurn * Time.deltaTime);
    }
}
