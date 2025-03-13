using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

namespace Car.Move
{
    public class Brake
    {
        // 返す力
        private Vector3 _force;
        // ローカルベロシティ
        private Vector3 _localVelocity;
        // 現在のスピード
        private float _speed;
        // ブレーキ力
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
