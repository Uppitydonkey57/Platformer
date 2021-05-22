using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIfDestroyed : MonoBehaviour
{
    public Action[] actions;

    public bool doOnce;

    bool hasPerformed;

    public GameObject triggerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hasPerformed && !doOnce) || !hasPerformed)
            if (triggerObject == null)
            {
                foreach (Action action in actions) action.PerformAction();
                hasPerformed = true;
            }
    }
}
