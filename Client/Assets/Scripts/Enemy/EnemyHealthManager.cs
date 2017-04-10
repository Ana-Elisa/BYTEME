using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {


    public int maxHealth;
    public int currentHealth;
    public GameObject deathEffect;

    //points?

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (currentHealth <= 0) {
           // Instantiate(deathEffect, transform.position, transform.rotation);
            //add points
            Destroy(gameObject);
        }


    }
    public void Damage(int damageToGive) {
        currentHealth -= damageToGive;
    }
}
