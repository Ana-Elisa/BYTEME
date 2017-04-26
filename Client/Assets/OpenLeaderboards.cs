using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLeaderboards : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Application.OpenURL ("https://byteme.online/charts/");
			Destroy (gameObject);
		}
	}
}
