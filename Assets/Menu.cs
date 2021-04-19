using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    GameMaster gm;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(gm.LoadLevel(sceneName));
    }
}
