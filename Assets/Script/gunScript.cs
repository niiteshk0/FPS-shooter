using UnityEngine;

public class gunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impaceForce = 50f;

    public Camera fpsCam;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    void shoot()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range))
        {
            Debug.Log(rayHit.transform.name);   

            if(rayHit.rigidbody != null)
            {
                rayHit.rigidbody.AddForce(-rayHit.normal * impaceForce);
            }

            enemyScript Enemy = rayHit.transform.GetComponent<enemyScript>();
            if(Enemy != null)
            {
                Enemy.getDamage(damage);
            }
        }
    }
}
