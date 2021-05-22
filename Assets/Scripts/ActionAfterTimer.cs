using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAfterTimer : MonoBehaviour
{
    public Action[] actions;

    public float waitTime;

    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        foreach (Action action in actions) action.PerformAction();
    }
}
