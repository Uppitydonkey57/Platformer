using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition : MonoBehaviour
{
    public string sceneName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sceneName != null && sceneName != "")
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogWarning("You have not entered a scene name! ENTER A SCENE NAME!!!!!!!!!!!!!!!!");
            }
        }
    }
}
