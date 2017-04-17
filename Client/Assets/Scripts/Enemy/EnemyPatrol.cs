using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public float speed;
    public bool moveRight;
    public Rigidbody2D rgb2d;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;



    // Use this for initialization
    void Start () {
        rgb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
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
            rgb2d.velocity = new Vector2(-speed, rgb2d.velocity.y);
        }
	}
}
