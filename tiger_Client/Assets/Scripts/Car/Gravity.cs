using UnityEngine;
using UnityEngine.UIElements;

namespace Car
{
    public class Gravity : MonoBehaviour
    {

        private Core _core;

        private Vector3 _position;

        private float _gravityForce = -9.81f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _core = GetComponent<Core>();
            _position = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            
            Ray ray = new Ray(_position, -gameObject.transform.up);

            if (Physics.Raycast(ray, out RaycastHit hit, 10, _core.GroundLayer))
            {
                Vector3 groundNormal = hit.normal;
                Vector3 gravityDirection = -groundNormal;
                _core.Rb.AddForce(gravityDirection * _gravityForce, ForceMode.Acceleration);
            }
            else
            {
                //_core.Rb.AddForce(Vector3.down * _gravityForce * 10, ForceMode.Acceleration);
            }
        }

        
    }
}
