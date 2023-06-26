using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    
    private GameObject volumeSlider;
    private Slider vSlider;
    private GameObject resolutionField;
    private Dropdown resolutionDropdown;
    private GameObject fullScreen;
    private Toggle toggleFullScreen;
    public AudioMixer audioMixer;
    public GameObject buttons;

    [SerializeField]
    private SettingsMenuSO settingsMenuSO;

    Resolution[] resolutions;

    void Awake()
    {
        volumeSlider = GameObject.Find("SettingMenu/VolumeSlider");
        fullScreen = GameObject.Find("SettingMenu/FullScreen");
        resolutionField = GameObject.Find("SettingMenu/ResolutionDropdown");
        vSlider = volumeSlider.GetComponent<Slider>();
        toggleFullScreen = fullScreen.GetComponent<Toggle>();
        resolutionDropdown = resolutionField.GetComponent<Dropdown>();

       resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        //int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            /*if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }*/
        }
        resolutionDropdown.AddOptions(options);
        
        

        SetFullScreen(settingsMenuSO.IsFullScreen);
        SetVolume(settingsMenuSO.Volume);
        SetResolution(settingsMenuSO.ResolutionIndex);
        toggleFullScreen.isOn = settingsMenuSO.IsFullScreen;
        vSlider.value = settingsMenuSO.Volume;
        resolutionDropdown.value= settingsMenuSO.ResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    public void SetVolume(float volume)
    {
        settingsMenuSO.Volume = volume;
        audioMixer.SetFloat("mainVolume", volume);

        //Debug.Log("SetVolume"+ volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        settingsMenuSO.ResolutionIndex = resolutionIndex;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
       // Debug.Log("SetResolution"+resolution);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        settingsMenuSO.IsFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        //Debug.Log ("SetFullScreen"+Screen.fullScreen);
    }


    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SetButtons(bool isActive)
    {
        buttons.SetActive(isActive);
    }

}

