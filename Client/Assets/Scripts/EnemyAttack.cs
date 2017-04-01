using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public int attackDamage = 10;

	GameObject player;
	PlayerHealth playerHealth;

	void OnCollisionEnter2D(Collision2D col)
	{
		//print ("CONTACT MADE");
		if (col.gameObject.tag == "Player") {
			print ("INSIDE OBJECT TAG");
			//iunno something
			Attack();
		}
		
	}

	void Attack(){
		print ("INSIDE ATTACK");
		if (playerHealth.currentHealth > 0) {
			print ("INSIDE IF STATEMENT");
			playerHealth.TakeDamage (attackDamage);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
