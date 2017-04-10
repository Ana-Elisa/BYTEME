﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour {
	public float speed = 50f;
    public float maxSpeed = 3; 
	public float jumpPower = 150f;
	public bool grounded;
	private Rigidbody2D rb2d;
    private Animator anim;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        anim.SetBool("Grounded",grounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        //flip sprite
        if (Input.GetAxis("Horizontal") < -0.1f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }
    void FixedUpdate(){
		float h = Input.GetAxis("Horizontal");
       //moves player L R
        rb2d.AddForce((Vector2.right * speed) * h);
        //Limits speed of player
        if (rb2d.velocity.x > maxSpeed) {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed) {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
		
	}
}
