	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage = 20;

	void OnCollisionEnter2D(Collision2D col){
        //whatever colliding with send damage.
        /*if (col.isTrigger != true && col.CompareTag("Enemy")) {
			col.SendMessageUpwards ("Damage", damage);
		}
        */
		if(col.gameObject.tag == "Enemy")
		{
        	print("Attack trigger hit");
            Destroy(col.gameObject);
            //col.GetComponent<EnemyHealthManager>().giveDamage(damage);

        }
	}
}
