using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : StateMachineBehaviour
{
    Actor enemy;

    Rigidbody2D enemyRb;

    PlayerController player;

    public Vector2 distanceMaximum;
    public Vector2 distanceMinimum;

    BoxCollider2D collider;

    public LayerMask layerMask;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Actor>();

        enemyRb = enemy.GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerController>();

        collider = animator.GetComponent<BoxCollider2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 finalPosition = player.transform.position;

        Vector2 distance = new Vector2(Random.Range(distanceMinimum.x, distanceMaximum.x), Random.Range(distanceMinimum.y, distanceMaximum.y));

        finalPosition += distance;

        Vector2 direction = finalPosition - enemyRb.position;
        Ray ray = new Ray(enemyRb.position, direction);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, direction.magnitude))
            enemyRb.MovePosition(finalPosition);
        else
            enemyRb.MovePosition(hit.point);

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
