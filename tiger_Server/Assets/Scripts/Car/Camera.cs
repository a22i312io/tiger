using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed;
        private Vector3 _cameraPosition;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _cameraPosition = _target.position + _target.TransformDirection(_offset);

            transform.position = _cameraPosition;
            transform.LookAt(_target.position + _target.forward * 2f + new Vector3(0, 0, 1.7f));
            float targetZRotation = _target.eulerAngles.z;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, _target.eulerAngles.y, targetZRotation);
        }
    }
}
