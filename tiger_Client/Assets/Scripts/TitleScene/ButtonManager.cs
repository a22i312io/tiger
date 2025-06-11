using UnityEngine;
using UnityEngine.SceneManagement;

namespace Title {
    public class ButtonManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartButton()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
