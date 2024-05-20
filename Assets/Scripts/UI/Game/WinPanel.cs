using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : Panel
{
    [SerializeField] private TMP_Text targets;
    [SerializeField] private TMP_Text score;
    protected override void OnShowBegin()
    {
        base.OnShowBegin();
        targets.text = GameManager.Instance.GetCountOfCatchedTargets().ToString();
        score.text = GameManager.Instance.GetScore().ToString();
    }
}