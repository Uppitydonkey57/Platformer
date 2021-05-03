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

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();

        PlayerPrefs.SetString("ControlScheme", "Keyboard");

        /*if (PlayerPrefs.GetString("ControlScheme") == "Controller")
        {
            controlsDropdown.value = 1;
        }*/
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(gm.LoadLevel(sceneName));
    }

    public void UsingController(int choice)
    {
        if (choice == 0)
        {
            PlayerPrefs.SetString("ControlScheme", "Keyboard");
        }

        else if (choice == 1)
        {
            PlayerPrefs.SetString("ControlScheme", "Controller");
        }
    }

    public void SetVolumeMusic(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        SFXMixer.SetFloat("Volume", volume);
    }
}
