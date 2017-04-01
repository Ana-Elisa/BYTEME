using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGeneratorScript : MonoBehaviour {

	public Transform brick;
	public int choice = -1;
	//Set this to a specific value in the editor to test your layout

	void Start () {

		//To add another layout, create a function like the ones seen below and add it to this array
		GenDel[] generators = {HallwayA,HallwayB};

		if (choice < 0) {
			choice = (int)(Random.value*generators.Length);
		}

		GenDel gen = generators [choice];

		gen ();
		Destroy (gameObject);
			
	}

	delegate void GenDel();

	//Different choices are added as individual functions that manually construct the room

	void HallwayA () {
		for (int x = -5; x < 5; x++) {
			Instantiate (brick, transform.position + (Vector3.up * 3) + (Vector3.right * x), Quaternion.identity);
		}
		for (int x = -5; x < 5; x++) {
			Instantiate (brick, transform.position + (Vector3.down * 3) + (Vector3.right * x), Quaternion.identity);
		}
	}

	void HallwayB () {
		for (int x = -5; x < 5; x++) {
			Instantiate (brick, transform.position + (Vector3.up * 3) + (Vector3.right * x), Quaternion.identity);
		}
		for (int x = -5; x < 5; x++) {
			Instantiate (brick, transform.position + (Vector3.down * 3) + (Vector3.right * x), Quaternion.identity);
		}
		Instantiate (brick, transform.position + (Vector3.down * 2) + (Vector3.right * 2), Quaternion.identity);
		Instantiate (brick, transform.position + (Vector3.down * 2) + (Vector3.left * 3), Quaternion.identity);
		Instantiate (brick, transform.position + (Vector3.left * 0.5f), Quaternion.identity);
	}
}
