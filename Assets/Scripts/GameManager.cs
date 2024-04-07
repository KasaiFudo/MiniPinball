using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text targetsCounter;
    [SerializeField] private TMP_Text scoreCounter;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TMP_Text results;
    [SerializeField] private TMP_Text textOfResults;
    private int countOfAllTargets;
    private int countOfCatchedTargets;
    private int score;
    private AudioSource soundWin;
    private AudioSource soundFail;

    void Start()
    {
        countOfAllTargets = FindObjectsOfType<TargetCatcher>().Length;
        soundWin = GetComponent<AudioSource>();
        soundFail = GetComponentInChildren<AudioSource>();
        targetsCounter.text = $"Catch the targets: {countOfCatchedTargets}/{countOfAllTargets}";
    }

    public void CatchTheTarget()
    {
        countOfCatchedTargets++;
        if(countOfCatchedTargets == countOfAllTargets)
        {
            continueButton.SetActive(true);
            targetsCounter.text = $"Catch the targets: {countOfCatchedTargets}/{countOfAllTargets}";
            foreach(GameOverTrigger obj in FindObjectsOfType<GameOverTrigger>())
            {
                Destroy(obj.gameObject);
            }
            soundWin.Play();
        }
        else
        {
            targetsCounter.text = $"Catch the targets: {countOfCatchedTargets}/{countOfAllTargets}";
        }
    }
    public void AddScore(int value)
    {
        score += value;
        scoreCounter.text = "Score\n" + score;
    }
    public void CallGameOver()
    {
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
        restartButton.SetActive(true);
        textOfResults.text = "You have no balls! \n:c";
        results.text = $"Targets catched: {countOfCatchedTargets} \n Score: {score}";
        soundFail.Play();

    }
    public void CallWin()
    {
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
        nextButton.SetActive(true);
        textOfResults.text = "Your ball is win! \nc:";
        results.text = $"Targets catched: {countOfCatchedTargets} \n Score: {score}";
        LevelSectionHandler.UnlockNewLevels(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
        restartButton.SetActive(true);
        textOfResults.text = "Pause";
        results.text = $"Targets catched: {countOfCatchedTargets} \n Score: {score}";
    }
    public void RemovePause()
    {
        Time.timeScale = 1f;
        gamePanel.SetActive(true);
        restartButton.SetActive(false);
        menuPanel.SetActive(false);
    }
}
