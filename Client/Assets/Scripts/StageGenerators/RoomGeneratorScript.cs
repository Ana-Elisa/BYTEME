using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneratorScript : MonoBehaviour {

	public Transform[] prefabs;
	public int choice = -1;
	//Set this to a specific value in the editor to test your layout

	void Start () {

		if (choice < 0) {
			choice = (int)(Random.value*prefabs.Length);
		}

		Transform obj = prefabs [choice];

		Instantiate (obj, transform.position, Quaternion.identity);
		Destroy (gameObject);
			
	}
}
