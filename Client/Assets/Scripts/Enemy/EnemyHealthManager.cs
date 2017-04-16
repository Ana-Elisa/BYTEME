using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {


    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public GameObject deathEffect;



    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
    private void Update()
    {
        if (enemyCurrentHealth <= 0) {
           // Instantiate(deathEffect, transform.position, transform.rotation);
            //add points
            Destroy(gameObject);
        }


    }
    public void giveDamage(int damageToGive) {
        enemyCurrentHealth -= damageToGive;
    }
}
