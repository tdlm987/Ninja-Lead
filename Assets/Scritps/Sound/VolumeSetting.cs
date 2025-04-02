using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public void SetMusicVolume()
    {
        float volumValue = musicSlider.value;
        audioMixer.SetFloat("BackGroundMusic",Mathf.Log10(volumValue)*20);
    }
    public void SetSFXVolume()
    {
        float volumeValue = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volumeValue) * 20);
    }
}
