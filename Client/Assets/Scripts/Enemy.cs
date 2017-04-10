using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int currentHealth;
	public int maxHealth = 30;
	void Start(){
		currentHealth = maxHealth;
	}

	public void Damage(int damage){
		currentHealth -= damage;
		//create dying animation!
		//GameObject.GetComponent<Animation>().Play("Player RedFlash");
	
	}
}
