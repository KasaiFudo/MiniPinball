using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using GoToApps.Serialization;

public class LevelSectionHandler : MonoBehaviour
{
    public static int unlockedLevels = 1;
    private static int totalLevels = 35;
    private static int maxfinishedLevel = 0;
    private static string pathFile;
    private static List<Level> levels;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;
    [SerializeField]private LevelButton[] levelButtons;

    private int totalPage = 0;
    private int page = 0;
    private const int pageItem = 9;

    private void Awake()
    {
        pathFile = Path.Combine(Application.persistentDataPath, "Levels.dat");   
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 35);
        maxfinishedLevel = PlayerPrefs.GetInt("MaxfinishedLevel", 0);
        FillLevelData();
        Refresh();
    }
    private void OnApplicationPause(bool pause)
    {
        SaveLevels();
    }
    private void OnApplicationQuit()
    {
        SaveLevels();
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
                levelButtons[i].Setup(
                    levels[level].LevelIndex, 
                    levels[level].IsUnlocked, 
                    levels[level].IsFinished);
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
            SceneLoader.Instance.LoadLevel(level);
            //SceneManager.LoadScene(level);
            //GameManager.OnGameStarted.Invoke();
        }
    }
    public static void UnlockNewLevels(int finishedLevel)
    {
        if (CheckLevel(finishedLevel))
        {
            if(unlockedLevels < totalLevels)
            {
                unlockedLevels += 2;
                UpdateUnlockedLevels();
                SaveLevels();
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
            level.IsUnlocked = level.LevelIndex <= unlockedLevels;
        }
    }
    public static void UpdateLevelScore(int indexLevel, int score)
    {
        if (levels[indexLevel - 1].IsFinished)
        {
            if(score < levels[indexLevel - 1].Score)
            {
                Debug.Log($"Новый рекорд! Было: {levels[indexLevel - 1].Score}. Стало: {score}");
                levels[indexLevel - 1].Score = score;
            }
            else 
            {
                Debug.Log($"Рекорд не побит.");
            }
        }
        else
        {
            levels[indexLevel - 1].IsFinished = true;
            levels[indexLevel - 1].Score = score;
        }
        SaveLevels();
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
                if(levels.Count < totalLevels)
                {
                    for (int levelIndex = levels.Count; levelIndex < totalLevels; levelIndex++)
                    {
                        Level level = new Level(levelIndex + 1)
                        {
                            IsUnlocked = levelIndex + 1 <= unlockedLevels,
                        };
                        levels.Add(level);
                    }
                }
            }
            else
            {
                for (int levelIndex = 0; levelIndex < totalLevels; levelIndex++)
                {
                    Level level = new Level(levelIndex + 1)
                    {
                        IsUnlocked = levelIndex + 1 <= unlockedLevels,
                    };
                    levels.Add(level);
                }
            }
        }
    }
    private static void SaveLevels()
    {
        BinarySerializer.Serialize(pathFile, levels);
        PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        PlayerPrefs.SetInt("MaxfinishedLevel", maxfinishedLevel);
    }
    private void CheckButton()
    {
        backButton.SetActive(page>0);
        nextButton.SetActive(page<totalPage);
    }
}
