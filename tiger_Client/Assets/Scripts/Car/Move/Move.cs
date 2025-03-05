using UnityEngine;
namespace Car.Move
{
    public class Move : MonoBehaviour
    {
        private Input _input;
        private Hover _hover;
        private Accelerator _accelerator;
        private Steering _steering;
        void Start()
        {
            _input = GetComponent<Input>();
            _hover = GetComponent<Hover>();
            _accelerator = GetComponent<Accelerator>();
            _steering = GetComponent<Steering>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_input != null)
            {
                _accelerator.IsAccelerator = _input.IsAccerelation;

                _steering.Steeringvalue = _input.SteeringValue;

            }


        }

        
    }
}