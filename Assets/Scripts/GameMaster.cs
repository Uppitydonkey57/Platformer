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

    public bool usingController;

    Vector2 oldMousePos;

    Vector2 oldLeftStick;
    Vector2 oldRightStick;

    public Texture2D mouseTexture;

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

    public void PlayerDeath(float time)
    {
        StartCoroutine(LoadLevelTime(SceneManager.GetActiveScene().name, time));
    }

    public IEnumerator LoadLevel(string sceneName)
    {
        sceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(sceneTransitionTime);

        if (FindObjectOfType<SaveMusic>() != null)
            FindObjectOfType<SaveMusic>().MusicSave();

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadLevel()
    {
        sceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(sceneTransitionTime);

        if (FindObjectOfType<SaveMusic>() != null) FindObjectOfType<SaveMusic>().MusicSave();

        DOTween.Clear(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadLevelTime(string sceneName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(sceneTransitionTime);

        Debug.Log("Loading Scene");

        if (FindObjectOfType<SaveMusic>() != null) FindObjectOfType<SaveMusic>().MusicSave();

        DOTween.Clear(true);

        SceneManager.LoadScene(sceneName);
    }

    public void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool leftMoved = false;
        bool rightMoved = false;

        bool mouseMoved = oldMousePos - Mouse.current.position.ReadValue() != new Vector2(0, 0);

        if (Gamepad.current != null)
        {
            leftMoved = oldLeftStick - Gamepad.current.leftStick.ReadValue() != Vector2.zero;
            rightMoved = oldRightStick - Gamepad.current.rightStick.ReadValue() != Vector2.zero;
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.aButton.wasPressedThisFrame || rightMoved || leftMoved || Gamepad.current.dpad.ReadValue() != Vector2.zero || Gamepad.current.leftTrigger.ReadValue() > 0 || Gamepad.current.rightTrigger.ReadValue() > 0 || Gamepad.current.leftShoulder.ReadValue() > 0 || Gamepad.current.rightShoulder.ReadValue() > 0)
            {
                usingController = true;
            }
            else if (Keyboard.current.aKey.wasPressedThisFrame || Mouse.current.leftButton.isPressed || mouseMoved)
            {
                usingController = false;
            }
        } else
        {
            usingController = false;
        }

        if (Application.isEditor && Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }

        oldMousePos = Mouse.current.position.ReadValue();

        if (Gamepad.current != null)
        {
            oldLeftStick = Gamepad.current.leftStick.ReadValue();
            oldRightStick = Gamepad.current.rightStick.ReadValue();
        }
    }

    private void OnGUI()
    {
        if (usingController)
        {
            Cursor.SetCursor(mouseTexture, Camera.main.WorldToScreenPoint(player.transform.position), CursorMode.Auto);
        } else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        Cursor.visible = !usingController;
    }

    private void Restart(InputAction.CallbackContext callback)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }
}
