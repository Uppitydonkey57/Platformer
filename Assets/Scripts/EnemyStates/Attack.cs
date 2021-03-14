using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    Weapon weapon;

    public bool attackOnExit;

    public string weaponName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        weapon = animator.GetComponent<Weapon>();

        if (weapon == null)
        {
            weapon = animator.GetComponentInChildren<Weapon>();
        }
        
        if (weapon == null)
        {
            weapon = animator.GetComponentInParent<Weapon>();
        }

        if (!string.IsNullOrEmpty(weaponName))
        {
            foreach (Weapon weapon in animator.GetComponents<Weapon>())
            {
                if (weapon.weaponName == weaponName)
                {
                    this.weapon = weapon;
                }
            }

            foreach (Weapon weapon in animator.GetComponentsInChildren<Weapon>())
            {
                if (weapon.weaponName == weaponName)
                {
                    this.weapon = weapon;
                }
            }

            foreach (Weapon weapon in animator.GetComponentsInParent<Weapon>())
            {
                if (weapon.weaponName == weaponName)
                {
                    this.weapon = weapon;
                }
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!attackOnExit)
        {
            weapon.Attack();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackOnExit)
        {
            weapon.Attack();
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
