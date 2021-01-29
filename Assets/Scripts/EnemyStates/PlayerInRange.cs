using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : StateMachineBehaviour
{
    public enum SwitchType { Boolean, Trigger }

    public SwitchType switchType;

    public string triggerName;

    public float range;

    public LayerMask playerLayer;

    Transform player;

    Enemy enemy;

    public bool useChance;

    public float chance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>().transform;

        enemy = animator.GetComponent<Enemy>();

        if (enemy == null)
        {
            enemy = animator.GetComponentInParent<Enemy>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Physics2D.OverlapCircle(enemy.transform.position, range, playerLayer))
        {
            float randomValue = Mathf.Round(Random.Range(0, chance));

            if (switchType == SwitchType.Trigger)
            {
                if (!useChance)
                    animator.SetTrigger(triggerName);
                else
                {
                    if (randomValue == 1)
                    {
                        animator.SetTrigger(triggerName);
                    }
                }
            } else if (switchType == SwitchType.Boolean)
            {
                if (!useChance)
                    animator.SetBool(triggerName, true);
                else
                {
                    if (randomValue == 1)
                    {
                        animator.SetBool(triggerName, true);
                    }
                }
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
