using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : StateMachineBehaviour
{
    public Vector2 moveSpeed;
    public Vector2 slowingSpeed;

    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.MovePosition(rb.position + (moveSpeed * Time.deltaTime));

        moveSpeed.x = Mathf.Clamp(moveSpeed.x - (slowingSpeed.x * Time.deltaTime), 0, Mathf.Infinity);
        moveSpeed.x = Mathf.Clamp(moveSpeed.y - (slowingSpeed.y * Time.deltaTime), 0, Mathf.Infinity);
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
