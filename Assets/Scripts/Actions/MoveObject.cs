using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : Action
{
    public GameObject moveObject;

    public float moveTime;

    public Vector2 movePosition;

    public override void PerformAction()
    {
        moveObject.transform.DOMove((Vector2)transform.position + movePosition, moveTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(movePosition + (Vector2)transform.position, 0.3f);
    }
}
