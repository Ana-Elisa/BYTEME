using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickItem : MonoBehaviour {
	
	public static List<int> playerItems = new List<int>();

	public static void Save(){
		
	}

	void OnCollisionEnter2D (Collision2D col){
		
		if (col.gameObject.tag == "Player") {

			print ("Collided with an item");

			if (gameObject.name == "YarnBall") 
			{
				print ("YarnBall Collided");
				Destroy (gameObject);
				playerItems.Add (0);
			}

			if (gameObject.name == "RiceBall") 
			{
				print ("RiceBall Collided");
				Destroy (gameObject);
				playerItems.Add (1);
			}

			if (gameObject.name == "RiceBowl") 
			{
				print ("RiceBowl Collided");
				Destroy (gameObject);
				playerItems.Add (2);
			}

			if (gameObject.name == "ChickenLeg")
			{
				print ("ChickenLeg Collided");
				Destroy (gameObject);
				playerItems.Add (3);
			}

			//PRINTING FOR DEBUGGING
			foreach (int item in playerItems) {
				print (item);
			}

		}
	}
}
