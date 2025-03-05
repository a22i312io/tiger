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
        void LateUpdate()
        {
            //_cameraPosition = _target.position + _target.TransformDirection(_offset);

            //transform.position = _cameraPosition;
            //transform.LookAt(_target.position + _target.forward * 2f);

            Vector3 desiredPosition = _target.TransformPoint(_offset);
            desiredPosition = desiredPosition + new Vector3(3.5f, 1.6f, 2.7f);

            // �X���[�Y�ɒǏ]
            _cameraPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = _cameraPosition;

            // �^�[�Q�b�g�̉�]���l�����Č����𒲐�
            transform.LookAt(_target.position + _target.forward * 2f);
        }
    }
}
