using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;

    public void CounterUpdate(int count)
    {
        counter.text = $"{count}";
    }
}
