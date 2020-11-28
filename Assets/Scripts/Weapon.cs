using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isMelee;

    public GameObject projectilePrefab;

    public float projectileSpeed;

    public GameObject firePoint;

    public float fireScreenShake;
    public float screenShakeDuration;

    public float fireScreenFreeze;

    public float shootWait;
    bool canShoot = true;

    public bool useShootPause;
    public float ShootTime;
    public float shootPauseTime;
    bool shootPause = false;

    ScreenShake screenShake;
    ScreenFreeze screenFreeze;

    public void Awake()
    {
        screenShake = FindObjectOfType<ScreenShake>();
        screenFreeze = FindObjectOfType<ScreenFreeze>();

        if (useShootPause)
        {
            StartCoroutine(ShootPause());
        }
    }

    public void Attack()
    {
        if (canShoot && !shootPause)
        {
            screenShake.Shake(screenShakeDuration, fireScreenShake);
            screenFreeze.Freeze(fireScreenFreeze);

            GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.AddForce(projectile.transform.up * projectileSpeed, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning(projectile.name + " has no rigidbody, please add one.");
            }

            StartCoroutine(ShootWait());
        }
    }

    IEnumerator ShootWait()
    {
        canShoot = false;

        yield return new WaitForSeconds(shootWait);

        canShoot = true;
    }

    IEnumerator ShootPause()
    {
        shootPause = true;

        yield return new WaitForSeconds(shootPauseTime);

        shootPause = false;

        yield return new WaitForSeconds(ShootTime);

        StartCoroutine(ShootPause());
    }
}
