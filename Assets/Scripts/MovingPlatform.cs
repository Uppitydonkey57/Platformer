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

    Rigidbody2D rb;

    bool touchingPlayer;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        actualEndPositions = GetActualPositions();

        startPos = transform.position;

        rb = GetComponent<Rigidbody2D>();

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

        for (int i = 0; i < actualEndPositions.Length; i++)
        {
            float moveTime = GetTime(i == 0 ? (Vector2)transform.position : actualEndPositions[i - 1], actualEndPositions[i]);

            sequence.Append(rb.DOMove(actualEndPositions[i], moveTime, false));
        }

        sequence.SetLoops(-1, LoopType.Yoyo).SetUpdate(UpdateType.Fixed);
    }

    float GetTime(Vector2 position1, Vector2 position2)
    {
        return ((Mathf.Abs(position2.x) - Mathf.Abs(position1.x)) + (Mathf.Abs(position2.y) - Mathf.Abs(position1.y))) / moveSpeed;
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
            touchingPlayer = true;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;

            collision.gameObject.transform.SetParent(null);
        }
    }
}
