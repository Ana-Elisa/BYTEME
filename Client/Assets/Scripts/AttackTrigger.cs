using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage = 20;
	void onTriggerEnter2D(Collider2D col){
		//whatever colliding with send damage.
		if (col.isTrigger != true && col.CompareTag("Enemy")) {
			col.SendMessageUpwards ("Damage", damage);
		}
	}
}
