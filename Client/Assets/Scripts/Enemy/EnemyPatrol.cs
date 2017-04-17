using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public float speed;
    public bool moveRight;
    public Rigidbody2D rgb2d;
	public Collider2D nose;

	public float direction = 1; //- = left + = right


    // Use this for initialization
    void Start () {
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

		rgb2d.velocity = new Vector2 (direction*speed, rgb2d.velocity.y);
	}

	public void reverse()
	{
		direction = direction * -1;
            rgb2d.velocity = new Vector2(-speed, rgb2d.velocity.y);
        }
	}

