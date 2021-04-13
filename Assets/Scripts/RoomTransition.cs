using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition : MonoBehaviour
{
    public string sceneName;

    GameMaster gm;

    Animator animator;

    public bool useBuildOrder;

    public AudioClip transitionSound;
    private AudioSource source;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();

        animator = GetComponent<Animator>();

        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sceneName != null && sceneName != "" && !useBuildOrder)
            {
                StartCoroutine(gm.LoadLevel(sceneName));

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
        }
    }
}
