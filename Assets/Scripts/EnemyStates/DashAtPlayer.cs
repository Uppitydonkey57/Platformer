using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAtPlayer : StateMachineBehaviour
{
    GameObject player;

    public bool findObjectWithTag;
    public string findTag;

    Rigidbody2D rb;

    bool isFlipped;

    Transform transform;

    public float dashingSpeed;

    public float dashTime;
    float time;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = dashTime;

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

        Vector2 moveDirection = ((Vector2)player.transform.position - rb.position).normalized;

        rb.velocity = moveDirection * dashingSpeed;

        /*if (player.transform.position.x < rb.position.x && !isFlipped)
        {
            Flip();
        }
        else if (player.transform.position.x > rb.position.x && isFlipped)
        {
            Flip();
        }*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Dashing", false);
        }
    }

    private void Flip()
    {
        isFlipped = !isFlipped;

        rb.transform.Rotate(180, 0, 0);
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
