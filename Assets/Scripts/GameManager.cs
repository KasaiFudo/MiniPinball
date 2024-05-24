using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private AudioSource soundWin;
    [SerializeField] private AudioSource soundFail;

    public static UnityEvent OnGameStarted = new UnityEvent();

    public static UnityEvent OnGameWin = new UnityEvent();

    public static UnityEvent OnGameLose = new UnityEvent();

    public static UnityEvent OnGamePaused = new UnityEvent();

    public static UnityEvent OnGameContinue = new UnityEvent();

    public static UnityEvent<int> OnScoreCountChanged = new UnityEvent<int>();

    public static UnityEvent<int, int> OnTargetsCountChanged = new UnityEvent<int, int>();

    private static int _countOfAllTargets;
    private static int _countOfCatchedTargets;
    private static int _score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SceneLoader.OnSceneLoaded.AddListener(StartGame);
    }
    public int GetScore()
    {
        return _score;
    }

    public int GetCountOfAllTargets()
    {
        return _countOfAllTargets;
    }
    public int GetCountOfCatchedTargets()
    {
        return _countOfCatchedTargets;
    }
    public void CatchTheTarget()
    {
        _countOfCatchedTargets++;
        OnTargetsCountChanged.Invoke(_countOfCatchedTargets, _countOfAllTargets);
        if (_countOfCatchedTargets == _countOfAllTargets)
        {
            CallWin();
            foreach(GameOverTrigger obj in FindObjectsOfType<GameOverTrigger>())
            {
                Destroy(obj.gameObject);
            }
            soundWin.Play();
        }
    }
    public void AddScore(int value)
    {
        _score += value;
        OnScoreCountChanged.Invoke(_score);
    }
    public void CallGameOver()
    {
        soundFail.Play();
        OnGameLose.Invoke();
    }
    public void CallWin()
    {
        OnGameWin.Invoke();
        LevelSectionHandler.UnlockNewLevels(SceneManager.GetActiveScene().buildIndex);
        LevelSectionHandler.UpdateLevelScore(SceneManager.GetActiveScene().buildIndex, _score);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        OnGamePaused.Invoke();
    }
    public void RemovePause()
    {
        Time.timeScale = 1f;
        OnGameContinue.Invoke();
    }
    public void ReturnToMenu()
    {
        SceneLoader.Instance.LoadLevel(0);
        Time.timeScale = 1f;
    }
    public void StartGameLevel()
    {
        SceneLoader.Instance.LoadLevel(LevelSectionHandler.unlockedLevels);
    }
    public void RestartScene()
    {
        SceneLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void NextLevel()
    {
        SceneLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void StartGame()
    {
        _score = 0;
        _countOfCatchedTargets = 0;
        _countOfAllTargets = FindObjectsOfType<TargetCatcher>().Length;
        OnGameStarted.Invoke();
    }
}
