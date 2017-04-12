using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	//public int currentHealth;
	//public int maxHealth = 30;
    Rigidbody2D rgb2d;
    public float knockback;
    public float knockbackLength;//how long enemy knocked back for
    public float knockbackCount;
    public bool knockFromRight;

	void Start(){
		//currentHealth = maxHealth;
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {  
        
        //reaction to hit
        if (knockbackCount <= 0)
        {
            //move enemy around
        }
        else {
            if (knockFromRight) {
                rgb2d.velocity = new Vector2(-knockback, knockback);
            }
            if (!knockFromRight) {
                rgb2d.velocity = new Vector2(knockback, knockback);
            }
            knockbackCount -= Time.deltaTime;
        }
    }
   

}
