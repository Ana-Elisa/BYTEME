using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	Rigidbody2D rb;
	public float maxhspeed = 2f;
	public float groundf = 1f;
	bool canJump = true;
	public float jumpForce = 700;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}//Start
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis ("Horizontal") != 0) 
		{
			rb.drag = 1;
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
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (canJump) {
				canJump = false;
				GetComponent<Rigidbody2D>().AddForce (this.gameObject.transform.up * jumpForce);
				print ("Up");
			}
		}
			
	}

	void OnCollisionEnter2D (Collision2D collidingObject) {
		//DEBUGGING: print ("Collided");
		if (collidingObject.gameObject.tag == "Stage") {
			canJump = true;
			print ("can jump");
		}
	}

}