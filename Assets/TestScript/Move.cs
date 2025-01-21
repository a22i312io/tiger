using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(0, 0, 5);
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(-Physics.gravity * _rb.mass, ForceMode.Force);
    }
}
