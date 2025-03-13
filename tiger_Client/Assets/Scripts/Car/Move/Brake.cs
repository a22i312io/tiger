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
