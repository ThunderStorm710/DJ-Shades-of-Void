using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMasterMixer;
    public AudioMixer audioMusicMixer;
    public AudioMixer audioSFXMixer;

    [SerializeField] private Slider musicSlider;

    public TMP_Dropdown resolutionDropdown; 
    Resolution[] resolutions;


    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        Debug.Log("Passou a linha 25");
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Debug.Log("Fez o Start");

    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex]; 
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void SetOverallVolume(float volume)
    {
        audioMasterMixer.SetFloat("volume", volume);
    }

    public void SetVolumeMusic(float music)
    {
        float volume = musicSlider.value;
        
        audioMusicMixer.SetFloat("music", Mathf.Log10(music)*20);
    }

    public void SetVolumeSFX(float sfx)
    {
        audioSFXMixer.SetFloat("sfx", sfx);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
