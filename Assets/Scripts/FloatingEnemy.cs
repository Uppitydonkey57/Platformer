using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    Rigidbody2D rb;

    enum MovementState { WANDERING, TARGETING, DASHING }

    MovementState state;

    public LayerMask playerLayer;

    GameObject player;

    public float wanderSpeed;

    public float targetSpeed;
    public float targetRadius;

    public float dashingSpeed;
    public float dashTime;
    public float dashRadius;

    bool isDashing = false;

    bool isFlipped;

    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerController>().gameObject;

        animator = GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case MovementState.WANDERING:
                Collider2D playerCheck = Physics2D.OverlapCircle(transform.position, targetRadius, playerLayer);

                if (playerCheck != null && playerCheck.gameObject.CompareTag("Player"))
                {
                    state = MovementState.TARGETING;

                    animator.SetTrigger("Following");
                }
                break;

            case MovementState.TARGETING:
                MoveTowardsPlayer();

                playerCheck = Physics2D.OverlapCircle(transform.position, dashRadius, playerLayer);

                if (playerCheck != null && playerCheck.gameObject.CompareTag("Player"))
                {
                    state = MovementState.DASHING;

                    animator.SetBool("Dashing", true);
                }

                break;

            case MovementState.DASHING:
                if (!isDashing)
                {
                    isDashing = true;
                    StartCoroutine(Dash());
                }
                break;
        }
    }

    void MoveTowardsPlayer()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, targetSpeed * Time.deltaTime));

        Vector2 lookDirection = (Vector2)player.transform.position - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        rb.rotation = angle;

        if (player.transform.position.x < rb.position.x && !isFlipped)
        {
            Flip();
        } else if (player.transform.position.x > rb.position.x && isFlipped)
        {
            Flip();
        }
    }

    IEnumerator Dash()
    {
        if (player.transform.position.x < rb.position.x && !isFlipped)
        {
            Flip();
        }
        else if (player.transform.position.x > rb.position.x && isFlipped)
        {
            Flip();
        }

        Vector2 moveDirection = ((Vector2)player.transform.position - rb.position).normalized;

        rb.velocity = moveDirection * dashingSpeed;

        yield return new WaitForSeconds(dashTime);

        state = MovementState.TARGETING;

        rb.velocity = Vector2.zero;

        animator.SetBool("Dashing", false);

        isDashing = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, targetRadius);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, dashRadius);
    }

    private void Flip()
    {
        isFlipped = !isFlipped;

        transform.Rotate(180, 0, 0);
    }
}
