using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionOnAnyKey : MonoBehaviour
{
    public Action[] actions;

    public bool doOnce;

    bool hasPerformed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hasPerformed && !doOnce) || !hasPerformed)
        {
            if (Mouse.current.leftButton.ReadValue() == 1)
            {
                foreach (Action action in actions) action.PerformAction();
                hasPerformed = true;
            }
            if (Gamepad.current != null)
            {
                if (Gamepad.current.aButton.ReadValue() == 1 || Gamepad.current.rightTrigger.ReadValue() == 1)
                {
                    foreach (Action action in actions) action.PerformAction();
                    hasPerformed = true;
                }
            }
        }
    }
}
