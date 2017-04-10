using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float speed = 10f;
	public float jumpPower = 150f;
	Rigidbody2D rb;
	public float maxhspeed = 2f;
	public float groundf = 1f;
	bool canJump = true;
	public float jumpForce = 700;
	public bool grounded;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}//Start
	
	// Update is called once per frame
	void Update () 
	{	float h = Input.GetAxis("Horizontal");
		rb.AddForce ((Vector2.right * speed) * h);
		if (Input.GetAxis ("Horizontal") != 0) 
		{
			rb.drag = 15;
		}

		if (Input.GetAxis ("Horizontal") > 0f) {
			if (rb.velocity.x < maxhspeed) {
				rb.AddForce (Vector2.right * groundf);
<<<<<<< HEAD
				print ("R");
				 

=======
				//print ("R");
>>>>>>> master
			}
		}
		if (Input.GetAxis ("Horizontal") < 0f) {
			if (rb.velocity.x > -maxhspeed) {
				rb.AddForce (Vector2.left * groundf);
				//print ("L");
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (canJump) {
				canJump = false;
				GetComponent<Rigidbody2D>().AddForce (this.gameObject.transform.up * jumpForce);
				//print ("Up");
			}
		}
			
	}

	void OnCollisionEnter2D (Collision2D collidingObject) {
		//DEBUGGING: print ("Collided");
		if (collidingObject.gameObject.tag == "Stage") {
			canJump = true;
			//print ("can jump");
		}
	}
	/*void FixedUpdate(){
		float h = Input.GetAxis("Horizontal");
		rb.AddForce ((Vector2.right * speed) * h);
	}*/

}