using Car.Data;
using UnityEngine;
using UnityEngine.UIElements;
namespace Car
{
    public class Core : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private CarDataBase _carDataBase;
        private CarConfig _carConfig;
        // �n�ʂ𔻒肷�邽�߂̃��C���[
        [SerializeField] private LayerMask _groundLayer;
        // �n�ʂƂ̍ő勗��
        [SerializeField] private float _maxDistance;
        // ������
        [SerializeField] private float _dampingForce;
        // ���݈ʒu
        Vector3 _currentPosition;
        // ���ݎp��
        Quaternion _currentRotation;
        // ���O
        private string _name;
        // �d��
        private float _weight;
        // ���ݑ��x
        private float _speed;
        // �ō����x
        private float _maxspeed;
        // ������
        private float _acceleration;
        // �n���h�����O
        private float _steering;
        // �X�^�~�i
        private float _stamina;

        public Rigidbody Rb { get { return _rb; } }
        public LayerMask GroundLayer { get { return _groundLayer; } }
        public float MaxDistance { get { return _maxDistance; } }
        public float DampingForce {  get { return _dampingForce; } }
        public Vector3 CurrentPosition { get { return _currentPosition; } }
        public Quaternion CurrentRotation { get { return _currentRotation; } }
        public string Name { get { return _name; } }
        public float Weight { get { return _weight; } }
        public float Speed { get { return _speed; } }
        public float MaxSpeed { get { return _maxspeed; } }
        public float Acceleration { get { return _acceleration; } }
        public float Steering { get { return _steering; } }
        public float Stamina { get { return _stamina; } }

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _carConfig = _carDataBase.GetCarConfigs("Normal");
            ApplyCarConfig();
        }

        private void Update()
        {
            
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            _currentPosition = gameObject.transform.position;
            _currentRotation = gameObject.transform.rotation;

            _speed = _rb.linearVelocity.magnitude;


        }

        private void ApplyCarConfig()
        {
            if(_carConfig != null)
            {
                _name = _carConfig.Name;
                _weight = _carConfig.Weight;   
                _speed = _carConfig.Speed;
                _maxspeed = _carConfig.MaxSpeed;
                _acceleration = _carConfig.Acceleration;
                _steering = _carConfig.Steering;
                _stamina = _carConfig.Stamina;
            }
        }

        private void GravityForce()
        {
            Ray ray = new Ray(new Vector3(0, 0, 0), -gameObject.transform.up);

            if(Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayer))
            {
                
            }
        }
    }
}