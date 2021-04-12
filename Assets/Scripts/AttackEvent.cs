using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    Weapon weapon;

    Animator animator;

    public void Attack(string whichWeapon)
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (animator == null)
        {
            animator = GetComponentInParent<Animator>();
        }

        if (!string.IsNullOrEmpty(whichWeapon))
        {
            foreach (Weapon weapon in animator.GetComponents<Weapon>())
            {
                if (weapon.weaponName == whichWeapon)
                {
                    this.weapon = weapon;
                }
            }

            foreach (Weapon weapon in animator.GetComponentsInChildren<Weapon>())
            {
                if (weapon.weaponName == whichWeapon)
                {
                    this.weapon = weapon;
                }
            }

            foreach (Weapon weapon in animator.GetComponentsInParent<Weapon>())
            {
                if (weapon.weaponName == whichWeapon)
                {
                    this.weapon = weapon;
                }
            }
        }
    }
}
