using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

	float speed = .05f;
	float jump = 15f;
	bool inMotion;
	Vector3 lastDir;

	Animator anim;

	GameObject character;

	void Start()
	{
		lastDir = new Vector3 (1, 0, 0);
		character = GameObject.Find ("cat");
		inMotion = false;
		anim = GetComponent<Animator> ();
		anim.SetBool ("Walking", true);
		anim.SetBool ("Grounded", true);
	}

	// Update is called once per frame
	void Update () 
	{
		
		float horizontal = Input.GetAxis ("Horizontal");

		var move = new Vector3 (horizontal, 0, 0);
	
		if (horizontal != 0) {
			if (!inMotion) 
			{
				anim.SetBool("Walking", true);
				inMotion = true;
			}
			lastDir = move;

		} 
		else 
		{
			if (inMotion) 
			{
				anim.SetBool ("Walking", false);
				inMotion = false;
			}
		}

		transform.rotation = Quaternion.LookRotation (lastDir);	

		character.transform.position += move * speed;
	}

}
