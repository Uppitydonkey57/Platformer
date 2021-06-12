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

        FindObjectOfType<SaveMusic>().MusicSave();

        DOTween.Clear(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadLevelTime(string sceneName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(sceneTransitionTime);

        Debug.Log("Loading Scene");

        FindObjectOfType<SaveMusic>().MusicSave();

        DOTween.Clear(true);

        SceneManager.LoadScene(sceneName);
    }

    public void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor && Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }

    private void Restart(InputAction.CallbackContext callback)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }
}
