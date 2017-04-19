using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {

	bool firstrun = true;

	// Use this for initialization
	void LateUpdate () {
		if (firstrun) {
			firstrun = false;
			Collider2D collider = gameObject.GetComponent<Collider2D> ();
			bool check = true;
			int iter = 100;
			Collider2D[] results = new Collider2D[8];
			while (check && iter > 0) {
				check = false;
				for (int i = 0; i < results.Length; i++) {
					results [i] = null;
				}
				collider.OverlapCollider (new ContactFilter2D (), results);
				for (int i = 0; i < results.Length; i++) {
					if (results [i] != null && results [i].gameObject.CompareTag ("Stage")) {
						check = true;
						transform.position = transform.position + Vector3.up * 0.1f;
						break;
					}
				}
				iter--;
			}
			if (iter <= 0) {
				print ("INFINITE LOOP");
			}
		}
	}
}
