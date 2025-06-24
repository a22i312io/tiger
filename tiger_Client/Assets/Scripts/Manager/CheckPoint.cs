using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private int _number = -1;
    private bool _isPassed;
    BoxCollider collider;
    public int Number { get { return _number; }  set { _number = value; } }
    public bool IsPassed { get { return _isPassed; } set { _isPassed = value; } }
    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
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
            collider.enabled = false;
        }
    }

    public void OnCollider()
    {
        collider.enabled = true;
    }
}
