using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 250f;
    public float fireRate = 15f;
    public ParticleSystem muzzleFlash;

    float nextFireTime = 0f;

    bool isReloading = false;
    public int maxAmmo = 10;
    int currentAmmo;
    public Camera fpsCam;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            shoot();
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading");
        isReloading = true;

        yield return new WaitForSeconds(1f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;

        RaycastHit rayHit;
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range))
        {
            Debug.Log(rayHit.transform.name);   

            if(rayHit.rigidbody != null)      // when goli touch with object then move the object
            {
                rayHit.rigidbody.AddForce(-rayHit.normal * impactForce);
            }

            enemyScript Enemy = rayHit.transform.GetComponent<enemyScript>();
            if(Enemy != null)
            {
                Enemy.getDamage(damage);
            }
        }
    }
}
