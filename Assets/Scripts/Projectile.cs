using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("Destroying");
            Destroy(gameObject, destroyTime);
        }

        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.GetComponent<Actor>() == null)
        {
            screenShake.Shake(shakeDuration, ShakeAmount);
        }

        if (destroyParticle != null)
        {
            GameObject particleInstance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(particleInstance, 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.GetComponent<Actor>() == null)
        {
            screenShake.Shake(shakeDuration, ShakeAmount);
        }

        if (destroyParticle != null)
        {
            GameObject particleInstance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(particleInstance, 10);
        }
    }

    IEnumerator StartDelay()
    {
        collider2d.enabled = false;

        yield return new WaitForSeconds(collisionStartDelay);

        collider2d.enabled = true;
    }
}
