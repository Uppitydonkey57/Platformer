using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMoving : StateMachineBehaviour
{
    public string triggerName;

    public bool shouldReturnFloat;

    public bool roundFloat;

    Vector2 previousPosition;

    public bool reversed;

    [Tooltip("There's no real point in using this.")]public float ignoreDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float movementAmount = Mathf.Abs(Vector2.Distance((Vector2)animator.transform.position, previousPosition) * 100);

        if (!shouldReturnFloat)
        {
            if (!reversed && movementAmount > ignoreDistance)
            {
                animator.SetTrigger(triggerName);
            }

            if (reversed && movementAmount <= ignoreDistance)
            {
                animator.SetTrigger(triggerName);
            }
        } else
        {
            if ((!reversed && movementAmount > ignoreDistance) || (reversed && movementAmount <= ignoreDistance))
            {
                if (!roundFloat)
                {
                    animator.SetFloat(triggerName, movementAmount / 10);
                } else
                {
                    animator.SetFloat(triggerName, Mathf.Round(movementAmount / 10));
                }
            }
        }

        previousPosition = animator.transform.position;
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
