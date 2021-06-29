using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    GameMaster gm;

    public AudioMixer SFXMixer;
    public AudioMixer musicMixer;
    public TMP_Dropdown controlsDropdown;

    Resolution[] resolutions;
    public TMP_Dropdown dropdown;

    public Toggle fullscreenToggle;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();

        resolutions = Screen.resolutions;

        dropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i  = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdown.AddOptions(options);
        dropdown.value = currentResolutionIndex;
        dropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(gm.LoadLevel(sceneName));
    }

    public void SetVolumeMusic(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        SFXMixer.SetFloat("Volume", volume);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
