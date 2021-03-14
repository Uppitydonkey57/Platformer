using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTouch : MonoBehaviour
{
    public float collisionWaitTime;
    public float damageStayTime;

    public float canDamageWait;
    bool canAttack = true;

    public string switchTag = "DamageZone";
    string initialTag;

    bool isTouchingPlayer;

    bool isAttacking;

    private void Start()
    {
        initialTag = tag;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void Update()
    {
        if (canAttack && isTouchingPlayer)
        {
            Debug.Log("Attacked");
            StartCoroutine(ActivateAttack());
        }

        if (isAttacking && isTouchingPlayer)
        {
            FindObjectOfType<PlayerController>().Kill();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    IEnumerator ActivateAttack()
    {
        canAttack = false;

        yield return new WaitForSeconds(collisionWaitTime);

        transform.tag = switchTag;
        isAttacking = true;

        yield return new WaitForSeconds(damageStayTime);

        transform.tag = initialTag;
        isAttacking = false;

        yield return new WaitForSeconds(canDamageWait);

        canAttack = true;
    }
}
