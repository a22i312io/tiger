using UnityEngine;

namespace Car.Move
{
    public class Move : MonoBehaviour
    {
        private Input _input;
        private Hover _hover;
        private Accelerator _accelerator;
        private Steering _steering;
        private Player.Player _player;
        private bool _isAccelerator = false;
        private Vector3 _steeringforce = new Vector3();

        public bool IsAccelerator { set { _isAccelerator = value; } }
        public Vector3 SteeringForce { set { _steeringforce = value; } }

        void Start()
        {
            _hover = GetComponent<Hover>();
            _accelerator = GetComponent<Accelerator>();
            _steering = GetComponent<Steering>();
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
                _accelerator.IsAccelerator = _isAccelerator;

                _steering.Steeringvalue = _steeringforce.x;
            


        }

        
    }
}