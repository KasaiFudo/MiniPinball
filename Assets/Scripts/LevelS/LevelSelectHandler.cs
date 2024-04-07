using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using GoToApps.Serialization;

public class LevelSectionHandler : MonoBehaviour
{
    public static int unlockedLevels = 1;
    private static int totalLevels = 21;
    private static int maxfinishedLevel = 0;
    private static string pathFile;
    private static List<Level> levels;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;

    private LevelButton[] levelButtons;
    private int totalPage = 0;
    private int page = 0;
    private const int pageItem = 9;

    private void Awake()
    {
        pathFile = Path.Combine(Application.persistentDataPath, "save.dat");   
        levelButtons = GetComponentsInChildren<LevelButton>();
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        maxfinishedLevel = PlayerPrefs.GetInt("MaxfinishedLevel", 0);
        FillLevelData();
        Refresh();
    }
    private void OnApplicationPause(bool pause)
    {
        BinarySerializer.Serialize(pathFile, levels);
    }
    private void OnApplicationQuit()
    {
        BinarySerializer.Serialize(pathFile, levels);
    }
    public void ClickNext()
    {
        page += 1;
        Refresh();
    }
    public void ClickBack()
    {
        page -= 1;
        Refresh();
    }
    public void Refresh()
    {
        totalPage = totalLevels / pageItem;
        int index = page * pageItem;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = index + i;
            if (level < totalLevels)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(levels[level]._levelIndex, levels[level]._isUnlocked, levels[level]._isFinished);
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
        CheckButton();
    }
    public void StartLevel(int level)
    {
        if(level <= unlockedLevels)
        {
            SceneManager.LoadScene(level);
        }
    }
    public static void SetNewDataOfLevel(int finishedLevel, int score)
    {
        UpdateLevelScore(finishedLevel, score);
        if (CheckLevel(finishedLevel))
        {
            if(unlockedLevels < totalLevels)
            {
                unlockedLevels += 2;
                UpdateUnlockedLevels();
                BinarySerializer.Serialize(pathFile, levels);
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
                PlayerPrefs.SetInt("MaxfinishedLevel", maxfinishedLevel);
                Debug.Log("Добавлено 2 открытых уровня, обновлен максималный завершенный уровень");
            }
        }
        else
            Debug.Log("Уровень меньше максимально пройденного уровня или слишком далеко от пследнего разблокированного");
    }
    private static void UpdateUnlockedLevels()
    {
        foreach(var level in levels)
        {
            level._isUnlocked = level._levelIndex <= unlockedLevels;
        }
    }
    private static void UpdateLevelScore(int indexLevel, int score)
    {
        if (levels[indexLevel - 1]._isFinished)
        {
            if(score < levels[indexLevel - 1]._score)
            {
                Debug.Log($"Новый рекорд! Было: {levels[indexLevel - 1]._score}. Стало: {score}");
                levels[indexLevel - 1]._score = score;
            }
            else 
            {
                Debug.Log($"Рекорд не побит.");
            }
        }
        else
        {
            levels[indexLevel - 1]._isFinished = true;
            levels[indexLevel - 1]._score = score;
        }
        BinarySerializer.Serialize(pathFile, levels);
    }
    private static bool CheckLevel(int finishedLevel)
    {
        if (finishedLevel > maxfinishedLevel && finishedLevel > unlockedLevels - 2)
        {
            maxfinishedLevel = finishedLevel;
            return true;
        }
        else
            return false;
    }
    private void FillLevelData()
    {
        if(levels == null)
        {
            levels = new List<Level>();
            if (File.Exists(pathFile))
            {
                levels = BinarySerializer.Deserialize<List<Level>>(pathFile);
            }
            else
            {
                for (int levelIndex = 0; levelIndex < totalLevels; levelIndex++)
                {
                    Level level = new Level(levelIndex + 1)
                    {
                        _isUnlocked = levelIndex + 1 <= unlockedLevels,
                    };
                    levels.Add(level);
                }
            }
        }
    }
    private void CheckButton()
    {
        backButton.SetActive(page>0);
        nextButton.SetActive(page<totalPage);
    }
}
