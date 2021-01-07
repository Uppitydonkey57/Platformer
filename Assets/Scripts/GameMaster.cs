using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameMaster : MonoBehaviour
{
    public Sprite MATS;

    public bool isMatsMode;

    Controls controls;

    private void Start()
    {
        controls = new Controls();

        controls.Enable();

        if (isMatsMode)
        {
            foreach (SpriteRenderer rend in FindObjectsOfType<SpriteRenderer>())
            {
               rend.sprite = MATS;
            }
        }

        controls.Player.Reset.performed += Restart;
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }

    private void Restart(InputAction.CallbackContext callback)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
