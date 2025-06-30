using UnityEngine;

namespace Car.Move
{
    public class Move : MonoBehaviour
    {
        private Input _input;
        private Hover _hover;
        private Accelerator _accelerator;
        private Steering _steering;
        private Brake _brake;
        private Drift _drift;
        private Player.Player _player;
        private bool _isAccelerator = false;
        private Vector3 _steeringforce = new Vector3();
        private bool _isBrake = false;
        private bool _isDrift = false;

        public bool IsAccelerator { set { _isAccelerator = value; } }
        public Vector3 SteeringForce { set { _steeringforce = value; } }
        public bool IsBrake { set { _isBrake = value; } }

        void Start()
        {
            _hover = GetComponent<Hover>();
            _accelerator = GetComponent<Accelerator>();
            _steering = GetComponent<Steering>();
            _brake = new Brake();
            _drift = new Drift();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
                _accelerator.IsAccelerator = _isAccelerator;

                _steering.Steeringvalue = _steeringforce.x;

            if (_isBrake)
            {
                _brake.OnBrake(_accelerator);
            }

            if (_isDrift)
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