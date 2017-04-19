using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public float speed;
    public bool moveRight;
    public Rigidbody2D rgb2d;
	public Collider2D nose;
	private Animator anim;



	public float direction = 1; //- = left + = right


    // Use this for initialization
    void Start () {
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator> ();

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

	}

	public void reverse()
	{   //change direction of block
		direction = direction * -1;
		rgb2d.velocity = new Vector2 (-speed, rgb2d.velocity.y);
		//change direction of sprite
		if (gameObject.tag.CompareTo ("batEnemy") > 0) {
			if (Input.GetAxis ("Horizontal") < -0.1f && direction != 1) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			} else if (Input.GetAxis ("Horizontal") > 0 && direction == 1) {
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
				//transform.localScale = new Vector3 (1, 1, 1);
			}

		}
	}

}

