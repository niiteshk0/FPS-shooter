using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    enemyAIScript enemyScript;
    [SerializeField] Slider healthBar; 
    int Health;
    void Awake()
    {
        enemyScript = GetComponent<enemyAIScript>();
        Health = ((int)enemyScript.health);
        healthBar.value = Health;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeHealth(float healthEnemy)
    {
        //Debug.Log(count++);
        healthBar.value = healthEnemy;
    }
}
