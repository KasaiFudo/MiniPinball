using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSetter : MonoBehaviour
{
    private bool _isActive;
    private int _locale = 0;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerLocale"))
        {
            _locale = PlayerPrefs.GetInt("PlayerLocale");
            StartCoroutine(SetLocale(_locale));
        }

    }
    public void ChangeLocale(int locale)
    {
        if (_isActive)
            return;

        _locale = locale;
        StartCoroutine(SetLocale(_locale));
    }

    private IEnumerator SetLocale(int localeIndex)
    {
        _isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        PlayerPrefs.SetInt("PlayerLocale", _locale);
        PlayerPrefs.Save();
        _isActive = false;
    }
}
