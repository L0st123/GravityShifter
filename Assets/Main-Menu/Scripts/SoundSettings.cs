using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer masterMixer;

    private const string MusicPref = "SavedMusicVolume";
    private const string SfxPref = "SavedSfxVolume";

    private void Start()
    {
        // Load saved volumes or default to 0.5 (50%)
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicPref, 0.5f);
        float savedSfxVolume = PlayerPrefs.GetFloat(SfxPref, 0.5f);

        SetMusicVolume(savedMusicVolume);
        SetSfxVolume(savedSfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        musicSlider.value = volume;
        PlayerPrefs.SetFloat(MusicPref, volume);

        float dB = Mathf.Log10(volume) * 20f;
        masterMixer.SetFloat("MusicVolume", dB);
    }

    public void SetSfxVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        sfxSlider.value = volume;
        PlayerPrefs.SetFloat(SfxPref, volume);

        float dB = Mathf.Log10(volume) * 20f;
        masterMixer.SetFloat("SFXVolume", dB);
    }

    
    public void OnMusicSliderChanged()
    {
        SetMusicVolume(musicSlider.value);
    }

    public void OnSfxSliderChanged()
    {
        SetSfxVolume(sfxSlider.value);
    }
}
