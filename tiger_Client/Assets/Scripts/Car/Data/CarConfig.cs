using UnityEngine;
namespace Car.Data
{
    [SerializeField]
    [CreateAssetMenu(fileName = "CarConfig", menuName = "Scriptable Objects/CarConfig")]
    public class CarConfig : ScriptableObject
    {
        // ���O
        [SerializeField] private string _name;
        // �d��
        [SerializeField] private float _weight;
        // ���x
        [SerializeField] private float _speed;
        // �ō����x
        [SerializeField] private float _maxspeed;
        // ������
        [SerializeField] private float _acceleration;
        // �n���h�����O
        [SerializeField] private float _steering;
        // �X�^�~�i
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