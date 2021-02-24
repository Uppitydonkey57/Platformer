
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public bool isMelee;

    public Color weaponColor = Color.red;
    public Vector2 attackRange;
    public Vector2 attackOffset;

    public bool isRaycast;

    public string weaponName;
    public string hitTag;
    public float damage;

    public float shootWait;
    bool canShoot = true;

    public Transform firePoint;

    public GameObject projectilePrefab;
    public float projectileSpeed;

    public AudioSource audioSource;
    public AudioClip[] attackSounds;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        if (canShoot)
        {
            if (attackSounds != null && attackSounds.Length > 0)
            {
                audioSource.PlayOneShot(attackSounds[UnityEngine.Random.Range(0, attackSounds.Length - 1)]);
            }

            if (isMelee)
            {
                foreach (Collider2D collider in Physics2D.OverlapBoxAll((Vector2)transform.position + attackOffset, attackRange, 0))
                {
                    Actor inRangeActor = collider.GetComponent<Actor>();

                    if (collider.GetComponent<Actor>() != null)
                    {
                        if (Array.Exists(inRangeActor.hitTags, element => element == hitTag))
                        {
                            inRangeActor.ChangeHealthKnockback(-damage, transform.right);
                        }
                    }
                }
            }
            else if (isRaycast)
            {

            }
            else
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

                projectileRb.AddForce(projectile.transform.up * projectileSpeed, ForceMode2D.Impulse);
            }

            StartCoroutine(ShootWait());
        }
    }

    private void OnDrawGizmos()
    {
        if (isMelee)
        {
            Gizmos.color = weaponColor;

            Gizmos.DrawWireCube(transform.position + (Vector3)attackOffset, attackRange);
        }
    }

    IEnumerator ShootWait()
    {
        canShoot = false;

        yield return new WaitForSeconds(shootWait);

        canShoot = true;
    }
}