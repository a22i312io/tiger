using UnityEngine;

public class CameraTarget : MonoBehaviour
{

    private Transform _parent;
    [SerializeField] private Vector3 _localOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            //transform.position = transform.parent.position + transform.parent.TransformDirection(_localOffset);

           
            transform.rotation = transform.parent.rotation;
        }
    }
}
