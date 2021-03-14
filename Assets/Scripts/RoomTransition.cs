using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition : MonoBehaviour
{
    public string sceneName;

    GameMaster gm;

    Animator animator;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sceneName != null && sceneName != "")
            {
                StartCoroutine(gm.LoadLevel(sceneName));
                animator.SetTrigger("Open");
            }
            else
            {
                Debug.LogWarning("You have not entered a scene name! ENTER A SCENE NAME!!!!!!!!!!!!!!!!");
            }
        }
    }
}
