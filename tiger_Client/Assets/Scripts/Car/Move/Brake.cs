using UnityEngine;
using UnityEngine.UIElements;

namespace Car.Move
{
    public class Brake
    {
        // �Ԃ���
        private Vector3 _force;
        // ���[�J���x���V�e�B
        private Vector3 _localVelocity;
        // ���݂̃X�s�[�h
        private float _speed;
        // �u���[�L��
        private float _brakepower = 150;
        public Vector3 Brakeforce(Core core)
        {
            _localVelocity = core.transform.InverseTransformDirection(core.Rb.linearVelocity);
            _speed = _localVelocity.magnitude;
            Debug.Log(_localVelocity);
            if(_speed > 0)
            {
                
                _force = -_localVelocity.normalized * _brakepower;
                //core.Rb.linearDamping = 1000;
                //Debug.Log("aa");
            }
            else
            {
                _force = Vector3.zero;
            }
            

            return _force;
        }
    }
}
