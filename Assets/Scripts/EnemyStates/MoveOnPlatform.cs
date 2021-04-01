using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPlatform : StateMachineBehaviour
{

    public float speed;

    public float rayDistance;

    public Vector2 rayOffset;

    public bool movingRight = true;

    public LayerMask platformLayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Translate(animator.transform.right * (movingRight ? 1 : -1) * speed * Time.deltaTime);

        Vector2 checkPosition = (Vector2)animator.transform.position + (rayOffset * (movingRight ? 1 : -1));
        RaycastHit2D groundInfo = Physics2D.Raycast(checkPosition, Vector2.down, rayDistance, platformLayer);

        if (groundInfo.collider == null)
        {
            if (movingRight)
            {
                animator.transform.eulerAngles = new Vector2(0, -180);
                movingRight = false;
            }
            else
            {
                animator.transform.eulerAngles = new Vector2(0, 0);
                movingRight = true;
            }
        } else
        {
            Debug.Log(groundInfo.collider);
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
