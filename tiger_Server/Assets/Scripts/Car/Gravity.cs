using UnityEngine;
using UnityEngine.UIElements;

namespace Car
{
    public class Gravity : MonoBehaviour
    {
        private Core _core;
        private float _gravityForce;


        void Start()
        {
            _core = GetComponent<Core>();
            _gravityForce = _core.Rb.mass * 9.81f;
        }

        void FixedUpdate()
        {
            
            Vector3 rayStart = transform.position + Vector3.up * 0.5f;

            Ray ray = new Ray(rayStart, -transform.up);
            Debug.DrawRay(rayStart, -transform.up * 100, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _core.GroundLayer))
            {
                Vector3 groundNormal = hit.normal;
                //Vector3 gravityDirection = Vector3.Lerp(-groundNormal, Vector3.down, 1f).normalized;
                _core.Rb.AddForce(-groundNormal * _gravityForce, ForceMode.Acceleration);
                Debug.Log("Hit Ground");
                _core.IsGround = true;
            }
            else
            {
                _core.Rb.AddForce(Vector3.down * _gravityForce, ForceMode.Acceleration);
                _core.IsGround = false;
                Debug.Log("Missed Ground");
            }
        }
    }
}
