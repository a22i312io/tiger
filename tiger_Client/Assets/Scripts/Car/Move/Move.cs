using UnityEngine;
namespace Car.Move
{
    public class Move : MonoBehaviour
    {
        private Core _core;
        private Input _input;
        private Hover _hover;
        private Accelerator _accelerator;
        private Steering _steering;
        private Brake _brake;
        private float _speed;
        private Drift _drift;

        public float Speed { get { return _speed; }set { _speed = _accelerator.Speed; } }

        private Vector3 _force;
        void Start()
        {
            _core = GetComponent<Core>();
            _input = GetComponent<Input>();
            _hover = GetComponent<Hover>();
            _accelerator = GetComponent<Accelerator>();
            _steering = GetComponent<Steering>();
            _brake = new Brake();
            _drift = new Drift();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_input != null)
            {
                _accelerator.IsAccelerator = _input.IsAccerelation;

                _steering.Steeringvalue = _input.SteeringValue;

                if (_input.IsBrake)
                {
                    _brake.OnBrake(_accelerator);
                }
                
                if (_input.IsDrift)
                {
                    _drift.OnDrift(_steering);
                }
                else
                {
                    _drift.OffDrift(_steering);
                }

            }


        }

        
    }
}