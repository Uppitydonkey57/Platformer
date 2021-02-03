using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition : MonoBehaviour
{
    public string sceneName;

    GameMaster gm;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sceneName != null && sceneName != "")
            {
                StartCoroutine(gm.LoadLevel(sceneName));
            }
            else
            {
                Debug.LogWarning("You have not entered a scene name! ENTER A SCENE NAME!!!!!!!!!!!!!!!!");
            }
        }
    }
}
