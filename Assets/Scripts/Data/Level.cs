using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int _levelIndex;
    public int _score;
    public bool _isFinished;
    public bool _isUnlocked;

    public Level(int levelIndex)
    {
        _isFinished = false;
        _isUnlocked = false;
        _score = 0;
        _levelIndex = levelIndex;
    }
}