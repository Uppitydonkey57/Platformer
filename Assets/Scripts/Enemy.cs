using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    
    int hp;

    public MonoBehaviour[] behaviours;

    public Animator[] animatorBehaviours;

    public Color radiusColor = Color.white;

    public float activationRange;

    public LayerMask playerLayer;

    string[] Damageboxes;

    Rigidbody2D rb;

    public float knockbackSpeed;

    public float knockbackTime;

    public float deathWait;

    PlayerController player;

    ScreenFreeze screenFreeze;

    ScreenShake screenShake;

    [Range(0f, 1f)] public float shakeDurationHit;
    [Range(0f, 1f)] public float shakeAmountHit;
    [Space]
    [Range(0f, 1f)] public float shakeDurationDead;
    [Range(0f, 1f)] public float shakeAmountDead;

    public GameObject deathParticle;

    // Start is called before the first frame update
    void Start()
    {
        screenFreeze = FindObjectOfType<ScreenFreeze>();

        screenShake = FindObjectOfType<ScreenShake>();

        rb = GetComponent<Rigidbody2D>();

        hp = maxHp;

        player = FindObjectOfType<PlayerController>();

        if (behaviours[0] != null)
                foreach (MonoBehaviour behaviour in behaviours)
                {
                    behaviour.enabled = false;
                }

        if (animatorBehaviours != null)
            foreach (Animator animator in animatorBehaviours)
            {
                animator.enabled = false;
            }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D playerCheck = Physics2D.OverlapCircle(transform.position, activationRange, playerLayer);

        if (playerCheck != null)
        {
            Activate();
        }
    }

    void ChangeHealth(int amount)
    {
        if (hp > hp + amount)
        {
            screenShake.Shake(shakeDurationHit, shakeAmountHit);
        }

        hp += amount;

        if (hp <= 0)
        {
            if (deathParticle != null)
                Instantiate(deathParticle, transform.position, Quaternion.identity);

            screenShake.Shake(shakeDurationDead, shakeAmountDead);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = radiusColor;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Damageboxes == null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerController>().isDashing)
                {
                    ChangeHealth(-1);
                }
            } else if (other.gameObject.CompareTag("PlayerProjectile"))
            {
                Activate();
                ChangeHealth(-1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Damageboxes == null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerController>().isDashing)
                {
                    ChangeHealth(-1);
                }
            } else if (other.gameObject.CompareTag("PlayerProjectile"))
            {
                Activate();
                ChangeHealth(-1);
            }
        }
    }

    void Activate()
    {
        if (behaviours[0] != null)
            foreach (MonoBehaviour behaviour in behaviours)
            {
                behaviour.enabled = true;
            }

        if (animatorBehaviours != null)
            foreach (Animator animator in animatorBehaviours)
            {
                animator.enabled = true;
            }
    }
}
