using UnityEngine;
using UnityEngine.UIElements;

namespace Car
{
    public class Gravity : MonoBehaviour
    {
        private Core _core;
        private float _gravityForce = 9.81f;

        void Start()
        {
            _core = GetComponent<Core>();
        }

        void Update()
        {
            // レイの開始位置を車の少し上に調整
            Vector3 rayStart = transform.position + Vector3.up * 0.5f;

            Ray ray = new Ray(rayStart, -transform.up);
            Debug.DrawRay(rayStart, -transform.up * 100, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _core.GroundLayer))
            {
                Vector3 groundNormal = hit.normal;
                //Vector3 gravityDirection = Vector3.Lerp(-groundNormal, Vector3.down, 1f).normalized;
                _core.Rb.AddForce(-groundNormal * _gravityForce, ForceMode.Acceleration);
                //Debug.Log("Hit Ground");
            }
            else
            {
                _core.Rb.AddForce(Vector3.down * _gravityForce, ForceMode.Acceleration);
                Debug.Log("Missed Ground");
            }
        }
    }
}
