using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCube : MonoBehaviour {

	bool canJump = true;
	public float jumpForce = 700;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			jump ();
		}
	}

	void jump(){
		if (canJump) {
			canJump = false;
			GetComponent<Rigidbody2D>().AddForce (this.gameObject.transform.up * jumpForce);
			print ("Up");
		}
	}

	void OnCollisionEnter2D (Collision2D collidingObject) {
		//DEBUGGING: print ("Collided");
		if (collidingObject.gameObject.tag == "Stage") {
			canJump = true;
		}
	}
}
