using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{
    public string sceneName;

    GameMaster gm;

    Animator animator;

    public bool useBuildOrder;

    public AudioClip transitionSound;
    private AudioSource source;

    public float waitTime;

    bool hasTransitioned = false;

    bool shouldStopCamera;

    CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();

        animator = GetComponent<Animator>();

        source = GetComponent<AudioSource>();

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transition();
        }
    }

    void Transition()
    {
        if (hasTransitioned)
        {
            if (shouldStopCamera)
            {
                virtualCamera.Follow = null;
            }
            
            if (sceneName != null && sceneName != "" && !useBuildOrder)
            {
                StartCoroutine(gm.LoadLevelTime(sceneName, waitTime));

                animator.SetTrigger("Open");

                source.PlayOneShot(transitionSound);
            }
            else if (useBuildOrder)
            {
                StartCoroutine(gm.LoadLevel());

                animator.SetTrigger("Open");

                source.PlayOneShot(transitionSound);
            }
            else
            {
                Debug.LogWarning("You have not entered a scene name! ENTER A SCENE NAME!!!!!!!!!!!!!!!!");
            }

            hasTransitioned = true;
        }
    }
}
