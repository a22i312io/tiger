using UnityEngine;

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
        _cameraPosition = _target.position + _target.TransformDirection(_offset);

        
       //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime));

        transform.position = _cameraPosition;
        transform.LookAt(_target.position + _target.forward * 2f);
    }
}
