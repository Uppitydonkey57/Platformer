using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    Vector2 startPos;
    public Vector2[] endPositions;
    private Vector2[] actualEndPositions;

    LineRenderer lineRenderer;

    public bool startOnCollision;

    public float moveSpeed = 2;

    bool hasStarted;


    // Start is called before the first frame update
    void Start()
    {
        actualEndPositions = GetActualPositions();

        startPos = transform.position;

        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = actualEndPositions.Length + 1;
        lineRenderer.SetPosition(0, startPos);

        for (int i = 0; i < actualEndPositions.Length; i++)
            lineRenderer.SetPosition(i + 1, actualEndPositions[i]);

        if (!startOnCollision)
        {
            StartMoving();
            hasStarted = true;
        }
    }

    void StartMoving()
    {
        Sequence sequence = DOTween.Sequence();

        foreach (Vector2 position in actualEndPositions)
        {
            Debug.Log(position);
            sequence.Append(transform.DOMove(position, moveSpeed));
        }

        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    Vector2[] GetActualPositions()
    {
        if (endPositions.Length > 0)
        {
            Vector2[] actualPositions = new Vector2[endPositions.Length];

            for (int i = 0; i < actualPositions.Length; i++) actualPositions[i] = endPositions[i];

            actualPositions[0] += (Vector2)transform.position;

            for (int i = 1; i < actualPositions.Length; i++)
            {
                actualPositions[i] += actualPositions[i - 1];
            }

            return actualPositions;
        } else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector2[] positions = GetActualPositions();

        if (positions != null)
        {
            Gizmos.DrawLine(transform.position, positions[0]);

            if (positions.Length > 1)
                for (int i = 1; i < positions.Length; i++)
                    Gizmos.DrawLine(positions[i - 1], positions[i]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);

            collision.gameObject.GetComponent<PlayerController>();

            if (!hasStarted)
            {
                hasStarted = true;

                StartMoving();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
