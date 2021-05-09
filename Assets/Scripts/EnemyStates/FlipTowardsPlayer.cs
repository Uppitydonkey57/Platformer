using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTowardsPlayer : StateMachineBehaviour
{
    bool isFacingRight;

    Animator animator;

    PlayerController player;

    public bool flip;

    Vector3 initialScale;

    public bool resetOnExit = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;

        player = FindObjectOfType<PlayerController>();

        initialScale = animator.transform.localScale;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool abovePlayer = Mathf.Round(player.transform.position.x) ==  Mathf.Round(animator.transform.position.x);

        if ((player.transform.position.x - animator.transform.position.x) * (flip ? -1 : 1) < 0 && !isFacingRight && !abovePlayer)
        {
            Flip();
        }
        else if ((player.transform.position.x - animator.transform.position.x) * (flip ? -1 : 1) > 0 && isFacingRight && !abovePlayer)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = animator.transform.localScale;
        scale.x *= -1;
        animator.transform.localScale = scale;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetOnExit)
            animator.transform.localScale = initialScale;
    }

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
