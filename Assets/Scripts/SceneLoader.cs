using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    public static UnityEvent OnSceneLoaded = new UnityEvent();

    private int _sceneIndexToLoad;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int scene)
    {
        _sceneIndexToLoad = scene;
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(_sceneIndexToLoad);

        while (!load.isDone)
        {
            yield return null;
        }
        if(load.isDone)
            OnSceneLoaded.Invoke();
    }
}
