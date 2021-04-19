using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnCollision : MonoBehaviour
{
    public Action[] actions;

    public Vector2 size;

    public LayerMask layer;

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(transform.position, size, 0, layer))
            foreach (Action action in actions) action.PerformAction(); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
}
