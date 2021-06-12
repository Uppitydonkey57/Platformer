using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : StateMachineBehaviour
{
    GameObject player;

    public float moveSpeed;

    public bool findObjectWithTag;
    public string findTag;

    Rigidbody2D rb;

    public bool freezeX;
    public bool freezeY;

    public bool useVelocity;

    public Vector2 offset;

    public Vector2 velocitySpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>().gameObject;

        if (findObjectWithTag)
        {
            player = GameObject.FindGameObjectWithTag(findTag);
        }

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
            if (!useVelocity)
            {
                Vector2 movePosition = Vector2.MoveTowards(rb.position, (Vector2)player.transform.position + offset, moveSpeed * Time.deltaTime);
                rb.MovePosition(new Vector2(freezeX ? rb.transform.position.x : movePosition.x, freezeY ? rb.position.y : movePosition.y));
            } else
            {
                Vector2 direction = new Vector2();
                direction.x = player.transform.position.x + offset.x < animator.transform.position.x ? -1 : 1;
                direction.y = player.transform.position.y + offset.y < animator.transform.position.y ? -1 : 1;
                rb.velocity = new Vector2(freezeX ? rb.velocity.x : velocitySpeed.x * direction.x, freezeY ? rb.velocity.y : velocitySpeed.y * direction.y);
            }
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
