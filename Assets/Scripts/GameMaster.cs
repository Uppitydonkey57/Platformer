using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    public Sprite MATS;

    public bool isMatsMode;

    private void Start()
    {
        if (isMatsMode)
        {
            foreach (SpriteRenderer rend in FindObjectsOfType<SpriteRenderer>())
            {
               rend.sprite = MATS;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Swap out the input later
        if (Keyboard.current.rKey.isPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }
}
