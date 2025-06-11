using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private Input _input;
    private bool _isGameStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _isGameStart = false;
    }
    void Start()
    {
        _input = FindAnyObjectByType<Input>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input != null)
        {
            if (_input.IsReset)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    private void StartManager()
    {
        if (_isGameStart) return;

    }


}
