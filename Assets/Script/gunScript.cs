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

    Animator anim;
    public float reloadTime = 1f;
    bool isScope;

    private void Start()
    {
        currentAmmo = maxAmmo;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo <= 0 || Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

    }

    public void scope()
    {
        if (!isScope)
        {
            anim.SetBool("scope", true);
            isScope = true;
            fpsCam.fieldOfView = 45;
        }

        else
        {
            anim.SetBool("scope", false);
            isScope = false;
            fpsCam.fieldOfView = 60;
        }
    }
    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if(!isReloading)
            {
                nextFireTime = Time.time + 1f / fireRate;
                shoot();
            }
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading");

        isReloading = true;

        anim.SetBool("Reload", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);

        anim.SetBool("Reload", false);
        yield return new WaitForSeconds( 0.25f);

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

            if(rayHit.rigidbody != null)      // when Bullet touch with object then move the object
            {
                rayHit.rigidbody.AddForce(-rayHit.normal * impactForce);
            }

            // for the decrease health of the object or doing damage
            enemyAIScript Enemy = rayHit.transform.GetComponent<enemyAIScript>();
            if(Enemy != null)
            {
                Enemy.getDamage(damage);
            }
        }
    }
}
