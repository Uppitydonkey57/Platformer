using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointWeaponAtPlayer : StateMachineBehaviour
{
    Weapon weapon;

    Transform firepointTransform;

    Rigidbody2D enemyRb;

    public bool parentOfFirepoint;

    public bool resetOnExit;

    PlayerController player;

    public string weaponName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyRb = animator.GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerController>();

        weapon = animator.GetComponent<Weapon>();

        if (weapon == null)
        {
            weapon = animator.GetComponentInChildren<Weapon>();
        }

        if (parentOfFirepoint)
        {
            firepointTransform = weapon.firePoint.transform.parent.transform;
        } else
        {
            firepointTransform = weapon.firePoint.transform;
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

            firepointTransform = weapon.firePoint;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 tagetPosition = player.transform.position - firepointTransform.position;
        float angle = Mathf.Atan2(tagetPosition.y, tagetPosition.x) * Mathf.Rad2Deg - 90f;
        firepointTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetOnExit)
            firepointTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
