using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using Car.Player;


public class GameManager : MonoBehaviour
{

    private Input _input;
    private bool _isStart;
    private enum GameState { waiting, countdown, playing, finished }
    private GameState _state;
    private int _laps = 0;
    [SerializeField] private int _targetlaps;
    private List<GameObject> _checkpoints = new List<GameObject>();
    [SerializeField] private GameObject _player;
    private float _time;
    private bool _isCount = false;
    private bool _isTime = false;
    private bool _isFinished = false;
    private bool _cangoal = false;
    [SerializeField] private TextMeshProUGUI _counttext;
    [SerializeField] private TextMeshProUGUI _timetext;
    

    public int Laps { get { return _laps; } set{ _laps = value; } }
    public bool Cangoal { get { return _cangoal; } set { _cangoal = value; } }
    public GameObject Player { get { return _player; }set { _player = value; } }
    public bool State => _state == GameState.playing;
    void Awake()
    {
        InitGame();
        _input = FindAnyObjectByType<Input>();
        //StartCoroutine(StartCount());
        _state = GameState.countdown;
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

        switch (_state)
        {
            case GameState.waiting:

                break;

            case GameState.countdown:
                if (!_isCount)
                {
                    StartCoroutine(StartCount());

                    _isCount = true;
                }
                
                break;

            case GameState.playing:
                if (!_isTime)
                {
                    StartCoroutine(Timer());
                    StartManager();
                    _isTime = true;
                }

                if(_laps == _targetlaps)
                {
                    _state = GameState.finished;
                }
                
                break;

            case GameState.finished:
                if (!_isFinished)
                {
                    FinishedProcess();
                }
                break;


        }
    }

    private void InitGame()
    {
        _state = GameState.waiting;
        _time = 0;

    }

    private void StartManager()
    {
        if (_player)
        {
            PlayerController controller = _player.GetComponent<PlayerController>();
            controller.WakeUp();
        }
    }

    private IEnumerator StartCount()
    {
        int counttimer = 3;
        while(counttimer > 0)
        {
            _counttext.text =  counttimer.ToString();

            yield return new WaitForSeconds(1f);
            counttimer--;
        }

        _counttext.text = "GO";

        _state = GameState.playing;

        yield return new WaitForSeconds(1f);

        _counttext.text = "";
    }

    private IEnumerator Timer()
    {
        while(_state == GameState.playing)
        {
            _time += Time.deltaTime;

            _timetext.text = _time.ToString("F2");

            yield return null; 
        }

       

    }

    private void FinishedProcess()
    {
        _counttext.text = "GOAL!!";
    }
   


}
