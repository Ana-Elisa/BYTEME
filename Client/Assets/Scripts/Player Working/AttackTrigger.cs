using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTrigger : MonoBehaviour {

	public Slider damageSlider;
	public int damage;

	void Start(){

	}

	void OnCollisionEnter2D(Collision2D col){

		damage = (int)damageSlider.value;
       
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
