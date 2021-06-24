using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : StateMachineBehaviour
{
    GameObject player;
    public bool findObjectWithTag;
    public string findTag;

    Rigidbody2D rb;

    bool isFlipped;

    Transform transform;

    public bool shouldFlip = true;

    public bool resetOnExit;

    public float offset;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = animator.GetComponentInParent<Rigidbody2D>();
        }

        transform = animator.transform;

        if (rb == null)
        {
            transform = animator.GetComponentInParent<Transform>();
        }

        player = FindObjectOfType<PlayerController>().gameObject;

        if (findObjectWithTag)
        {
            player = GameObject.FindGameObjectWithTag(findTag);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            Vector2 lookDirection = (Vector2)player.transform.position - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            rb.rotation = angle + offset;


            if (shouldFlip)
            {
                if (player.transform.position.x < rb.position.x && !isFlipped)
                {
                    Flip();
                }
                else if (player.transform.position.x > rb.position.x && isFlipped)
                {
                    Flip();
                }
            }
        }
    }

    private void Flip()
    {
        isFlipped = !isFlipped;

        rb.transform.Rotate(180, 0, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetOnExit)
        {
            rb.rotation = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
