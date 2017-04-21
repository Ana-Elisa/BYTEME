using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
	public float turnChance = 0.5f;

    public float speed;
    public bool moveRight;
    public Rigidbody2D rgb2d;
	public Collider2D nose;
	private Animator anim;
	public int colliding;

	public float xPos;



	public float direction = 1; //- = left + = right


    // Use this for initialization
    void Start () {
        rgb2d = this.gameObject.GetComponent<Rigidbody2D>();
		anim = this.gameObject.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		anim.SetFloat ("Speed", Mathf.Abs (Input.GetAxis ("Horizontal")));
		///flips animation
		/*if (gameObject.tag.CompareTo ("batEnemy") > 0) {
			if (Input.GetAxis ("Horizontal") < -0.1f && direction != 1) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			} else if (Input.GetAxis ("Horizontal") > 0 && direction ==  1) {
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
				//transform.localScale = new Vector3 (1, 1, 1);
			}*/
		rgb2d.velocity = new Vector2 (direction * speed, rgb2d.velocity.y);

		if (Time.frameCount % 120 == 0 && colliding > 2) {
			rgb2d.velocity = new Vector2 (direction * speed, rgb2d.velocity.y + 10);
//			print ("jump");
			colliding = 0;
		}
		if (Time.frameCount % 120 == 0 && colliding > 0)
			colliding -= 1;

		if (Random.value < turnChance && Time.frameCount % 120 == 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			reverse ();
		}

		/*if (transform.localScale.x < -0.1f && direction == 1) {
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		} else if (transform.localScale.x > 0 && direction != 1) {
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}*/
//		print (transform.localScale.x);
	}

	public void reverse()
	{   //change direction of block
		direction = direction * -1;
		rgb2d.velocity = new Vector2 (-speed, rgb2d.velocity.y);
		//change direction of sprite
		/*if (gameObject.tag.CompareTo ("batEnemy") > 0) {
			if (Input.GetAxis ("Horizontal") < -0.1f && direction != 1) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			} else if (Input.GetAxis ("Horizontal") > 0 && direction == 1) {
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
				//transform.localScale = new Vector3 (1, 1, 1);
			}

		}*/

		colliding += 1;

	}

}

