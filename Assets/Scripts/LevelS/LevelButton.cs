using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]private TMP_Text levelIndex;
    [SerializeField] private CustomButton button;
    [SerializeField] private GameObject closedSprite;
    [SerializeField] private GameObject completeSprite;
    private int level = 0;
    private LevelSectionHandler levelSectionHandler;

    private void OnEnable()
    {
        button = gameObject.GetComponent<CustomButton>();
        levelSectionHandler = FindObjectOfType<LevelSectionHandler>();
    }

    public void Setup(int level, bool isUnlock, bool isComplete)
    {
        this.level = level;
        levelIndex.text = level.ToString();

        completeSprite.SetActive(isComplete);
        closedSprite.SetActive(!isUnlock);
        levelIndex.gameObject.SetActive(isUnlock);
        button.enabled = isUnlock;
    }
    public void OnClick()
    {
        levelSectionHandler.StartLevel(level);
    }

}
