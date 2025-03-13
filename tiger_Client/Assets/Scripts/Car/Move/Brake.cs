using Unity.Cinemachine;
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

        
       
        public void OnBrake(Accelerator accele)
        {
            
            _speed = accele.Speed;
            Debug.Log(_localVelocity);
            if(_speed > 0)
            {

                accele.Speed = _speed - 2;

            }
            else
            {
                accele.Speed = 0;
            }
            
        }
    }
}
