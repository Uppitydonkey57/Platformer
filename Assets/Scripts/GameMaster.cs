using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using DG.Tweening;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    public Sprite MATS;

    public bool isMatsMode;

    public Transform reloadCircle;

    PlayerController player;

    Controls controls;

    CinemachineBrain brain;

    public float sceneTransitionTime;

    public Animator sceneTransitionAnimator;

    private void Start()
    {
        controls = new Controls();

        controls.Enable();

        brain = Camera.main.GetComponent<CinemachineBrain>();

        player = FindObjectOfType<PlayerController>();

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

    public IEnumerator LoadLevel(string sceneName)
    {
        sceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(sceneTransitionTime);

        Debug.Log("Loading Scene");

        SceneManager.LoadScene(sceneName);
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
