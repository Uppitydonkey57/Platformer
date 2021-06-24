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

    Actor enemy;

    public bool useChance;

    public float chance;

    public bool reverse;

    public bool booleanValue = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>().transform;

        enemy = animator.GetComponent<Actor>();

        if (enemy == null)
        {
            enemy = animator.GetComponentInParent<Actor>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!reverse && Physics2D.OverlapCircle(enemy.transform.position, range, playerLayer) != null)
        {
            Set(animator);
        } else if (reverse && Physics2D.OverlapCircle(enemy.transform.position, range, playerLayer) == null)
        {
            Set(animator);
        }
    }

    void Set(Animator animator)
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
        }
        else if (switchType == SwitchType.Boolean)
        {
            if (!useChance)
                animator.SetBool(triggerName, booleanValue);
            else
            {
                if (randomValue == 1)
                {
                    animator.SetBool(triggerName, booleanValue);
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
