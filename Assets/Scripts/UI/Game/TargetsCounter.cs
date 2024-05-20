using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;

    public void CounterUpdate(int targetsNum, int allTargetsCount)
    {
        counter.text = $"{targetsNum}/{allTargetsCount}";
    }
}
