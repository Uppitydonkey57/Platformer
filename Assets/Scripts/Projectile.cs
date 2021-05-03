using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public float damage;

    public float collisionStartDelay;

    public float shakeDuration;
    public float ShakeAmount;

    ScreenShake screenShake;

    public GameObject destroyParticle;

    //public bool destroyOffCamera;
    public bool useDestroyTime;
    public float destroyWait;
    public float destroyTime;

    Collider2D collider2d;

    SpriteRenderer rend;

    private void Start()
    {
        screenShake = FindObjectOfType<ScreenShake>();

        collider2d = GetComponent<Collider2D>();

        StartCoroutine(StartDelay());

        if (useDestroyTime)
        {
            StartCoroutine(DestructWait());
        }

        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destruct(collision.gameObject.GetComponent<Actor>(), true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destruct(collision.gameObject.GetComponent<Actor>(), true);
    }

    IEnumerator DestructWait()
    {
        yield return new WaitForSeconds(destroyWait);

        transform.DOScale(new Vector2(0, 0), destroyTime);

        yield return new WaitForSeconds(destroyTime);

        Destruct(null, false);
    }

    IEnumerator StartDelay()
    {
        collider2d.enabled = false;

        yield return new WaitForSeconds(collisionStartDelay);

        collider2d.enabled = true;
    }

    void Destruct(Actor actor, bool showParticle)
    {
        Destroy(gameObject);

        if (actor == null)
        {
            screenShake.Shake(shakeDuration, ShakeAmount);
        }

        if (destroyParticle != null && showParticle)
        {
            GameObject particleInstance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(particleInstance, 10);
        }
    }
}
