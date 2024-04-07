using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSectionHandler : MonoBehaviour
{
    public static int unlockedLevels = 1;
    private static int totalLevels = 20;
    private static int maxfinishedLevel = 0;

    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;

    private LevelButton[] levelButtons;
    private int totalPage = 0;
    private int page = 0;

    private const int pageItem = 9;

    private void Start()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        maxfinishedLevel = PlayerPrefs.GetInt("MaxfinishedLevel", 0);
        Refresh();
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
        for(int i = 0; i < levelButtons.Length; i++) 
        {
            int level = index + i + 1;
            if( level <= totalLevels )
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level, level <= unlockedLevels);

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
    public static void UnlockNewLevels(int finishedLevel)
    {
        if (CheckLevel(finishedLevel))
        {
            if(unlockedLevels < totalLevels)
            {
                unlockedLevels += 2;
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
                PlayerPrefs.SetInt("MaxfinishedLevel", maxfinishedLevel);
                Debug.Log("Добавлено 2 открытых уровня, обновлен максималный завершенный уровень");
            }
        }
        else
            Debug.Log("Уровень меньше максимально пройденного уровня или слишком далеко от пследнего разблокированного");
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
    private void CheckButton()
    {
        backButton.SetActive(page>0);
        nextButton.SetActive(page<totalPage);
    }

}

struct LevelData
{
    public int levelIndex;
    public bool isFinished;
    public bool isUnlocked;
}