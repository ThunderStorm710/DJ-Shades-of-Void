using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer MyMixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider soundSlider;



    void Start()
    {

        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume") || PlayerPrefs.HasKey("soundVolume")){
            LoadVolume();
        } else {
            SetSoundVolume();
            SetVolumeMusic();
            SetVolumeSFX();
        }
    }

    public void SetSoundVolume()
    {
        float volume = soundSlider.value;
        
        MyMixer.SetFloat("sound", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void SetVolumeMusic()
    {
        float volume = musicSlider.value;
        
        MyMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume(){
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");

        SetVolumeMusic();
        SetVolumeSFX();
        SetSoundVolume();
    }
    /*public void SetOverallVolume(float volume)
    {
        audioMasterMixer.SetFloat("volume", volume);
    }*/

    public void SetVolumeSFX()
    {
        float volume = sfxSlider.value;
        
        MyMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }


}
