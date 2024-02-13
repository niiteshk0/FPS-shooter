using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAIScript : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;

    [SerializeField] private float onDetectionRadius = 10f;
    [SerializeField] private float health = 50f;
    [SerializeField] private float deathDestroyTime = 3f;
    int points = 5;
    bool enemyDeath;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyDeath)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= onDetectionRadius)
        {
            anim.SetBool("walk", true);
            navMeshAgent.SetDestination(player.transform.position);

            if(navMeshAgent.stoppingDistance >= distance)
            {
                anim.SetBool("punch", true);
                anim.SetBool("walk", false);
            }
            else
            {
                anim.SetBool("punch", false);
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, onDetectionRadius);
    }

    public void getDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if(!enemyDeath)
            {
                GameManager.instance.updateScore(points);
                
            }

            enemyDeath = true;

            anim.SetBool("death", true);
            anim.SetBool("walk", false);

            StartCoroutine(Afterdeathanimation());
        }
    }

    IEnumerator Afterdeathanimation()
    {
        yield return new WaitForSeconds(deathDestroyTime);
        Destroy(this.gameObject);
    }
}
