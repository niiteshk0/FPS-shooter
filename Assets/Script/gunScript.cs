using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gunScript : MonoBehaviour
{
    public float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float impactForce = 250f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private ParticleSystem muzzleFlash;

    float nextFireTime = 0f;

    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private Camera fpsCam;
    bool isReloading = false;
    int currentAmmo;

    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private AudioSource bulletAudio;
    public ParticleSystem bloodSplashEffect;

    public RaycastHit rayHit;
    Animator anim;
    bool isScope;

    [SerializeField] private TextMeshProUGUI ammoText;

    private void Start()
    {
        currentAmmo = maxAmmo;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ammoText.text = currentAmmo.ToString();

        if (isReloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

    }

    public void PressGunReload()
    {
        if(currentAmmo != maxAmmo)
            StartCoroutine(Reload());
    }

    public void scope()
    {
        if (!isScope)
        {
            anim.SetBool("scope", true);
            isScope = true;
            fpsCam.fieldOfView = 45;   // when scope in zoom in camera
        }

        else
        {
            anim.SetBool("scope", false);
            isScope = false;
            fpsCam.fieldOfView = 60;  // when scope out zoom out camera
        }
    }
    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if(!isReloading)
            {
                bulletAudio.Play();
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

    public void shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        ammoText.text = currentAmmo.ToString();


        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range))
        {
            //Debug.Log(rayHit.transform.name);   

            if(rayHit.rigidbody != null)      // when Bullet touch with object then move the object
            {
                rayHit.rigidbody.AddForce(-rayHit.normal * impactForce);
            }

            if(rayHit.collider.gameObject.CompareTag("Enemy"))
            {
                ParticleSystem bloodInstance =  Instantiate(bloodSplashEffect, rayHit.point, Quaternion.identity);
                bloodInstance.Play();
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
