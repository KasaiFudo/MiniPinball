using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetup : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
#endif
    }
}
