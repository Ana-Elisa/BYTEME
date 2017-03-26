using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

	float speed = .05f;
	float jump = 15f;
	bool grounded;
	Vector3 lastDir;

	Animator anim;

	GameObject character;

	void Start()
	{
		lastDir = new Vector3 (1, 0, 0);
		character = GameObject.Find ("cat");
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{
		anim.SetInteger ("Status", 0);
		float horizontal = Input.GetAxis ("Horizontal");

		var move = new Vector3 (horizontal, 0, 0);
	
		if (horizontal != 0) 
		{
			if (anim.GetInteger ("Status") != 1) 
			{
				anim.SetInteger ("Status", 1);	
			}
			lastDir = move;
		}

		transform.rotation = Quaternion.LookRotation (lastDir);	

		character.transform.position += move * speed;
	}

}
