using UnityEngine;
using UnityEngine.Windows;

namespace Car.Move
{
    public class Accelerator : MonoBehaviour
    {
        private Core _core;

        private bool _isAccelerator = false;

        Vector3 _frontDirection;

        private float _speed;

        public float _accelPerSecond;

        public float _turnPerSecond;

        public bool IsAccelerator {  get { return _isAccelerator; } set { _isAccelerator = value; } }
        public float Speed { get { return _speed; } set { _speed = value; } }
        void Start()
        {
           _speed = 0;
            _core = GetComponent<Core>();
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_core == null) return;
            Debug.Log(_speed);
            if (_isAccelerator)
            {

                //_frontDirection = _core.CurrentRotation * Vector3.forward;

                _speed += _accelPerSecond * Time.deltaTime;
                if (_speed > 150) _speed = 150;
                //Debug.Log("gg");

                //_frontDirection = _core.CurrentRotation * Vector3.forward;
                //ApplyAccelerator(_frontDirection);

            }
            else
            {
                _speed -= _accelPerSecond * Time.deltaTime / 2;
                if (_speed < 0) _speed = 0;
            }


            _core.Rb.linearVelocity = transform.forward * _speed;
        }

        private void ApplyAccelerator(Vector3 position)
        {
            if (_core.Speed > _core.MaxSpeed)
            {
                _core.Rb.linearDamping = 0.6f;
            }
            else
            {
                _core.Rb.linearDamping = 0.1f; // í èÌéûÇÃ drag Ç…ñﬂÇ∑
            }

            _core.Rb.AddForce(position * _core.Acceleration);

            //_core.Rb.linearVelocity = position * Time.deltaTime * 1000.0f;
        }
    }
}