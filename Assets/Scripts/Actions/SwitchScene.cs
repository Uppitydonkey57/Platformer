using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : Action
{
    public string sceneName;

    public bool useBuildOrder;

    public override void PerformAction()
    {
        if (!useBuildOrder)
        {
            SceneManager.LoadScene(sceneName);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
