using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingSettings : MonoBehaviour
{
    private int _isActive = 1;

    private void Start()
    {
        _isActive = PlayerPrefs.GetInt("PostProcessingSettings", 1);
        if (_isActive == 1)
            Camera.main.GetComponent<PostProcessVolume>().enabled = true;
        else
            Camera.main.GetComponent<PostProcessVolume>().enabled = false;
    }

    public void SetPostProcessing()
    {
        if (_isActive == 1)
        {
            _isActive = 0;
            Camera.main.GetComponent<PostProcessVolume>().enabled = false;
            PlayerPrefs.SetInt("PostProcessingSettings", _isActive);
        }
        else
        {
            _isActive = 1;
            Camera.main.GetComponent<PostProcessVolume>().enabled = true;
            PlayerPrefs.SetInt("PostProcessingSettings", _isActive);
        }
    }
}
