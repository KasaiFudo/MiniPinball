using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIControl : MonoBehaviour
{
    [SerializeField] private GamePanel gamePanel;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private PausePanel pausePanel;
    private void Start()
    {
        GameManager.OnGameWin.AddListener(ShowWin);
        GameManager.OnGameLose.AddListener(ShowLose);
        GameManager.OnGameStarted.AddListener(ShowGame);
        GameManager.OnGameContinue.AddListener(ShowGame);
        GameManager.OnGamePaused.AddListener(ShowPause);
    }
    private void ShowGame()
    {
        HideAll();
        gamePanel.Show();
    }
    private void ShowPause()
    {
        HideAll();
        pausePanel.Show();
    }
    private void ShowLose()
    {
        HideAll();
        gameOverPanel.Show();
    }
    private void ShowWin()
    {
        HideAll();
        winPanel.Show();
    }

    private void HideAll()
    {
        gamePanel.Hide();
        winPanel.Hide();
        pausePanel.Hide();
        gameOverPanel.Hide();
    }



}
