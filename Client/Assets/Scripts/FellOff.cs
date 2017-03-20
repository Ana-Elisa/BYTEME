using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Megan is testing
public class FellOff : MonoBehaviour {

	Rigidbody2D player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onCollisionEnter (Collision col)
	{
		/*This method will run when there is contact between the player and whatever this script is attached to
		 *This will freeze the player in place and not allow them to move
		 *This is intended to use as a GAMEOVER Screen
		 */

		/*if (col.gameObject.tag == "Player") {
			player.velocity.x = 0;
			player.velocity.y = 0;
			player.gravityScale = 0;
		}//If*/
	}//On Collision
}//Class
