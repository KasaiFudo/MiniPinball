using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : Panel
{
    [SerializeField] private TargetsCounter targetsCounter;
    [SerializeField] private ScoreCounter scoreCounter;

    private void Start()
    {
        GameManager.OnScoreCountChanged.AddListener(ChangeScore);
        GameManager.OnTargetsCountChanged.AddListener(ChangeTargets);
    }
    protected override void OnShowBegin()
    {
        targetsCounter.CounterUpdate(GameManager.Instance.GetCountOfCatchedTargets(), GameManager.Instance.GetCountOfAllTargets());
        scoreCounter.CounterUpdate(GameManager.Instance.GetScore());
    }

    private void ChangeScore(int value)
    {
        scoreCounter.CounterUpdate(value);
    }

    private void ChangeTargets(int value, int countOfAllTargets)
    {
        targetsCounter.CounterUpdate(value, countOfAllTargets);
    }

}
