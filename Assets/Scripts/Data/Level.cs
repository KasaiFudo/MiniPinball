using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int LevelIndex;
    public int Score;
    public bool IsFinished;
    public bool IsUnlocked;
    public Level(int levelIndex)
    {
        IsFinished = false;
        IsUnlocked = false;
        Score = 0;
        LevelIndex = levelIndex;
    }
}