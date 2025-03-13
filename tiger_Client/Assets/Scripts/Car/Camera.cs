using UnityEngine;

namespace Car
{
    public class Camera : MonoBehaviour
    {
        //Transform _target;
        
        [SerializeField] private Transform _target;

        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed;
        [SerializeField] private float _rotationSmoothSpeed = 5f;

        private Vector3 _cameraPosition;

        private void Start()
        {
            
        }
        void LateUpdate()
        {
            //_target = _targetchildren.parent;

            Debug.Log(_target.gameObject);

            _cameraPosition = _target.position + _target.TransformDirection(_offset);


            //_cameraPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = _cameraPosition;

            transform.LookAt(_target.position + _target.forward * 2f + new Vector3(0, 0, 1.7f));
            float targetZRotation = _target.eulerAngles.z;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetZRotation);
        }
    }
}
