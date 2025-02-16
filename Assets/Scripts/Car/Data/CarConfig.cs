using UnityEngine;
namespace Car.Data
{
    [SerializeField]
    [CreateAssetMenu(fileName = "CarConfig", menuName = "Scriptable Objects/CarConfig")]
    public class CarConfig : ScriptableObject
    {
        // 名前
        [SerializeField] private string _name;
        // 重量
        [SerializeField] private float _weight;
        // 速度
        [SerializeField] private float _speed;
        // 最高速度
        [SerializeField] private float _maxspeed;
        // 加速力
        [SerializeField] private float _acceleration;
        // ハンドリング
        [SerializeField] private float _steering;
        // スタミナ
        [SerializeField] private float _stamina;

        public string Name { get { return _name; } }
        public float Weight { get { return _weight; } }
        public float Speed { get { return _speed; } }
        public float MaxSpeed { get { return _maxspeed; } }
        public float Acceleration { get { return _acceleration; } }
        public float Steering { get { return _steering; } }
        public float Stamina { get { return _stamina; } }

    }
}