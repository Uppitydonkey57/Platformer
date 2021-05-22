using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    public Action[] actions;

    public void Action1()
    {
        actions[0].PerformAction();
    }

    public void Action2()
    {
        actions[1].PerformAction();
    }

    public void Action3()
    {
        actions[2].PerformAction();
    }

    public void Action4()
    {
        actions[3].PerformAction();
    }

    public void Action5()
    {
        actions[4].PerformAction();
    }
}
