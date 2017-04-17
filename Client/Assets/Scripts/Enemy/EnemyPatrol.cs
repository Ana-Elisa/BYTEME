using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public float speed;
<<<<<<< HEAD
=======
    public bool moveRight;
>>>>>>> refs/remotes/origin/master
    public Rigidbody2D rgb2d;
	public Collider2D nose;

    /*public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;*/
	public float direction = 1; //- = left + = right


    // Use this for initialization
    void Start () {
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        /*hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        if (hittingWall) {
            moveRight = !moveRight;
        }


        //patrols
        if (moveRight) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rgb2d.velocity = new Vector2(speed, rgb2d.velocity.y);
        }else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
<<<<<<< HEAD
            rgb2d.velocity = new Vector2(-moveSpeed, rgb2d.velocity.y);
        }*/

		rgb2d.velocity = new Vector2 (direction*speed, rgb2d.velocity.y);
	}

	/*void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Wall") {
			print ("Wall Collided");
			direction = direction * -1;
			transform.localScale = new Vector3 (transform.localScale.x *-1, transform.localScale.y);
		}


	}

	void onTriggerEnter2D (Collider2D col)
	{
		print ("HERE");
		if (col.gameObject.tag == "Stage") {
			print ("Wall Collided");
			direction = direction * -1;
			transform.localScale = new Vector3 (transform.localScale.x *-1, transform.localScale.y,1f);
		}
	}*/

	public void reverse()
	{
		direction = direction * -1;
=======
            rgb2d.velocity = new Vector2(-speed, rgb2d.velocity.y);
        }
>>>>>>> refs/remotes/origin/master
	}
}
