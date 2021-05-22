using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    bool isPaused;

    Controls controls;

    private void OnEnable()
    {
        controls = new Controls();

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Player.Pause.performed += ControlTriggerPause;
    }

    public void ControlTriggerPause(InputAction.CallbackContext context)
    {
        TriggerPause();
    }

    public void TriggerPause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseMenu.SetActive(false);
        }
    }


}
