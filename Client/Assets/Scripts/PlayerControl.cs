using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	Rigidbody2D rb;
	public float maxhspeed = 2f;
	public float groundf = 1f;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}//Start
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis ("Horizontal") != 0) 
		{
			rb.drag = 10;
		}

		if (Input.GetAxis ("Horizontal") > 0f) {
			if (rb.velocity.x < maxhspeed) {
				rb.AddForce (Vector2.right * groundf);
				print ("R");
			}
		}
		if (Input.GetAxis ("Horizontal") < 0f) {
			if (rb.velocity.x > -maxhspeed) {
				rb.AddForce (Vector2.left * groundf);
				print ("L");
			}
		}
			
	}

}