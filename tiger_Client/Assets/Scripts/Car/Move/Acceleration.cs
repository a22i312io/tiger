using UnityEngine;
using UnityEngine.Windows;

namespace Car.Move
{
    public class Accelerator : MonoBehaviour
    {
        private Core _core;

        private bool _isAccelerator = false;

        Vector3 _frontDirection;

        public bool IsAccelerator {  get { return _isAccelerator; } set { _isAccelerator = value; } }
        void Start()
        {
            _core = GetComponent<Core>();
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_core == null) return;
            
            if (_isAccelerator)
            {
                
                _frontDirection = _core.CurrentRotation * Vector3.forward;
                
                ApplyAccelerator(_frontDirection);
            }
            
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
        }
    }
}