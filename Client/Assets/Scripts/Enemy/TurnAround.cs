using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour {

	public GameObject parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		print ("COLLIDE");
		transform.parent.gameObject.transform.localScale = new Vector3 (transform.parent.gameObject.transform.localScale.x *-1, transform.parent.gameObject.transform.localScale.y,1f);
		transform.parent.gameObject.GetComponent<EnemyPatrol>().reverse();
	}
}
