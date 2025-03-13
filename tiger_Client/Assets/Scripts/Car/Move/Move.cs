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

        private Vector3 _force;
        void Start()
        {
            _core = GetComponent<Core>();
            _input = GetComponent<Input>();
            _hover = GetComponent<Hover>();
            _accelerator = GetComponent<Accelerator>();
            _steering = GetComponent<Steering>();
            _brake = new Brake();
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
                    _force = _brake.Brakeforce(_core);
                    _core.Rb.AddForce(_force);
                }

            }


        }

        
    }
}