using UnityEngine;

public class StartLine : MonoBehaviour
{
    private GameManager gameManager;
    private int _laps;
    [SerializeField]private GameObject _manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = _manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.Laps += 1;
            Debug.Log(gameManager.Laps);
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
