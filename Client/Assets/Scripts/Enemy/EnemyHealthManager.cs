using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {


    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public GameObject deathEffect;
	public int scoreCnt = 0;

    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
		scoreCnt = 0;
    }

    private void Update()
    {
        if (enemyCurrentHealth <= 0) 
		{
			//This would be the animation for the dead body but we don't have one :C
           // Instantiate(deathEffect, transform.position, transform.rotation);

			//Enemy is destroyed
            Destroy(gameObject);

			//Send something to the other script
			Player shit = FindObjectOfType (typeof(Player)) as Player;

			shit.GetComponent<Player> ().killCounter++;

        }


    }
    public void giveDamage(int damageToGive) {
        enemyCurrentHealth -= damageToGive;
    }


}
