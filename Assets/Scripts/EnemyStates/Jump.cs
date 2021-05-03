using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    [Header("This behaviour only works if the actor has a BoxCollider2D.")]
    public float extraRayDistance = 0.5f;

    public float jumpHeight;

    BoxCollider2D boxCollider;

    Rigidbody2D rb;

    public LayerMask groundLayer;

    public bool jumpOnce = true;

    public string jumpEndTrigger;

    bool hasStartedJump;

    public string jumpSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boxCollider = animator.GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            boxCollider = animator.GetComponentInChildren<BoxCollider2D>();
        }

        if (boxCollider == null)
        {
            boxCollider = animator.GetComponentInParent<BoxCollider2D>();
        }

        rb = animator.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = animator.GetComponentInChildren<Rigidbody2D>();
        }

        if (rb == null)
        {
            rb = animator.GetComponentInParent<Rigidbody2D>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsGrounded())
        {
            Vector2 velocity = rb.velocity;
            velocity.y = jumpHeight;
            rb.velocity = velocity;
            hasStartedJump = true;
        }

        animator.SetFloat(jumpSpeed, rb.velocity.y);

        if (IsGrounded() && hasStartedJump && jumpOnce)
        {
            animator.SetTrigger(jumpEndTrigger);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraRayDistance, groundLayer);

        return raycastHit.collider != null;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}



    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
