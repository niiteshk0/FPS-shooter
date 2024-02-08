using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAIScript : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent navMeshAgent;

    public float onDetectionRadius = 10f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= onDetectionRadius)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, onDetectionRadius);
    }
}
