using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {

	int frame;

	void Start(){
		frame = 5;
	}

	// Use this for initialization
	void LateUpdate () {
		if (frame == 0) {
			print ("item lateupdate firstrun");
			frame = -1;
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
					print (results [i]);
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
			RaycastHit2D[] cast = new RaycastHit2D[1];
			collider.Cast (Vector2.down, cast);
			if (cast [0] != null) {
				transform.position = cast [0].centroid;
			} else {
				print ("No collisions found for dropping item");
			}
		}
		if (frame > 0) {
			frame--;
		}
	}
}
