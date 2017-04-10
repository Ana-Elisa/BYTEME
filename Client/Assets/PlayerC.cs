using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour {
	public float speed = 50f;
	public float jumpPower = 150f;
	public bool grounded;
	Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Start () {
		rigidBody2D = this.GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		float h = Input.GetAxis("Horizontal");
		rigidBody2D.AddForce ((Vector2.right * speed) * h);
	}
}
