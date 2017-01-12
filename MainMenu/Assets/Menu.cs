using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;

    public Slider[] volumeSilders;
    public Toggle[] resToggles;
    public Toggle fullscreenToggle;
    public int[] screenWidths;
    int activeScreenIndex;

    void Start()
    {
        activeScreenIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1)? true : false;

        //Audio
        volumeSilders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSilders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSilders[2].value = AudioManager.instance.sfxVolumePercent;
        //Audio

        for (int i=0;i<resToggles.Length;i++)
        {
            resToggles[i].isOn = i == activeScreenIndex;
        }

        fullscreenToggle.isOn = isFullScreen;
    }


    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionMenu()
    {
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
    }

    public void SetscreenSolution(int i)
    {
        if (resToggles[i].isOn)
        {
            activeScreenIndex = i;
            float Ratio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / Ratio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool isFullScreen)
    {
        for (int i = 0; i < resToggles.Length; i++)
        {
            resToggles[i].interactable = !isFullScreen;
        }

        if (isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetscreenSolution(activeScreenIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullScreen)?1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
