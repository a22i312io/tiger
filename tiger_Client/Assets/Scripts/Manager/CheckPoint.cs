using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private int _number = -1;
    private bool _isPassed;

    public int Number { get { return _number; }  set { _number = value; } }
    public bool IsPassed { get { return _isPassed; } }
    private void Awake()
    {
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
        }
    }

    public void OnCollider()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.enabled = true;
    }
}
