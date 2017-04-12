using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public int damage = 10;
	public Slider healthSlider;
	bool isDead;
	bool damaged;

	PlayerControl move;
	GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" && !this.GetComponent<PlayerAttack>().getAttackStatus()) {
			TakeDamage (20);
		}
	}

	public void TakeDamage(int amount){

		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		if (currentHealth <= 0 ) 
		{
			Death ();
		}

	}

	public void Death(){
		isDead = true;
		print ("DEAD ggez");

		//Screen.lockCursor = true;

		GameObject.Find("Player").GetComponent<PlayerC> ().enabled = false;
		//move.enabled = false;
		//Im going to worry about this later because i'm tilted
		//FreezeCam ();
	}
		
	IEnumerator FreezeCam(){

		Camera.main.clearFlags = CameraClearFlags.Nothing;
		yield return null;
		Camera.main.cullingMask = 0;
	}
}
