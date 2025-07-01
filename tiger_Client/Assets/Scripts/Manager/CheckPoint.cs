using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private int _number = -1;
    private bool _isPassed;
    BoxCollider _collider;
    MeshRenderer _mesh;
    public int Number { get { return _number; }  set { _number = value; } }
    public bool IsPassed { get { return _isPassed; } set { _isPassed = value; } }
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _mesh = GetComponent<MeshRenderer>();
        _isPassed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPassed = true;
            Debug.Log("pass");
            _collider.enabled = false;
            _mesh.enabled = false;
        }
    }

    public void OnCollider()
    {
        _collider.enabled = true;
        _mesh.enabled = true;
    }
}
