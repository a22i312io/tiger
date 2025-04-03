using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private Input _input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
