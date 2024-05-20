using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    private Slider _sliderSound;
    private Slider _sliderMusic;


    private void Start()
    {
        _mixer.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 1));
        _mixer.audioMixer.SetFloat("SoundsVolume", PlayerPrefs.GetFloat("SoundsVolume", 1));      
    }

    public void ChangeMusic()
    {
        _mixer.audioMixer.SetFloat("MusicVolume", _sliderMusic.value);

        PlayerPrefs.SetFloat("MusicVolume", _sliderMusic.value);
    }
    public void ChangeSounds()
    {
        _mixer.audioMixer.SetFloat("SoundsVolume", _sliderSound.value);

        PlayerPrefs.SetFloat("SoundsVolume", _sliderSound.value);
    }
    public void SearchSliders()
    {
        float soundValue, musicValue;
        _mixer.audioMixer.GetFloat("SoundsVolume", out soundValue);
        _mixer.audioMixer.GetFloat("MusicVolume", out musicValue);
        _sliderMusic = GameObject.Find("MusicSlider").GetComponent<Slider>();
        _sliderSound = GameObject.Find("SoundsSlider").GetComponent<Slider>();
        _sliderSound.value = soundValue;
        _sliderMusic.value = musicValue;
    }

}
