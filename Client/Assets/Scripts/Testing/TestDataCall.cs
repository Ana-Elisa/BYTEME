using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") 
		{
			Player player = FindObjectOfType (typeof(Player)) as Player;

			int health;
			health = player.currentHealth;
			print (health);
			player.currentHealth = 100;
			player.healthSlider.value = player.currentHealth;

			foreach(int item in player.itemList) {
				print(item);
			}

			player.itemList.Add (3);

		}

	}

}
