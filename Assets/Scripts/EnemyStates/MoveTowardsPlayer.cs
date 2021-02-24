using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : StateMachineBehaviour
{
    PlayerController player;

    public float moveSpeed;

    Rigidbody2D rb;

    public bool freezeX;
    public bool freezeY;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>();

        rb = animator.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = animator.GetComponentInParent<Rigidbody2D>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            Vector2 movePosition = Vector2.MoveTowards(rb.position, player.transform.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(new Vector2(freezeX ? animator.transform.position.x : movePosition.x, freezeY ? animator.transform.position.y : movePosition.y));
        }
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
