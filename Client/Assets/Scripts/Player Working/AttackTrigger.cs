using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage = 20;

	void OnCollisionEnter2D(Collision2D col){
       
		if(col.gameObject.tag == "Enemy")
		{
        	//apply damage
            col.gameObject.GetComponent<EnemyHealthManager>().giveDamage(damage);


			//knockback
			var enemy = col.gameObject.GetComponent<Enemy>();
			enemy.knockbackCount = enemy.knockbackLength;

			if (col.transform.position.x < transform.position.x) {
				enemy.knockFromRight = true;
			} else {
				enemy.knockFromRight = false;
			}
        }
	}
}
