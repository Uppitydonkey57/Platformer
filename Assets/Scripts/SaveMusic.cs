using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMusic : MonoBehaviour
{
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.time = PlayerPrefs.GetFloat(musicSource.clip.name);
    }

    public void MusicSave()
    {
        PlayerPrefs.SetFloat(musicSource.clip.name, musicSource.time);
        Debug.Log(PlayerPrefs.GetFloat(musicSource.clip.name));
    }
}
