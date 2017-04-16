using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public List<int> itemList = new List<int>();

	//All the Health stuff
	public Slider healthSlider;
	public int startingHealth = 100;
	public int currentHealth;

	//All the Damage stuff
	public Slider damageSlider;
	public int startingDamage = 20;
	public int currentDamage;

	//All the speed stuff
	public Slider speedSlider;
	public int startingSpeed = 20;
	public int currentSpeed;

	//Set-up
	GameObject player;
	bool isDead;
	bool isDamaged;
	public int damage = 10;

	//For Save
	bool isSave = false;


	// Use this for initialization
	void Start () {
		//Find the player
		player = GameObject.Find ("Player");

		//Load players stats NEW GAME MODE
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;

		currentDamage = startingDamage;
		damageSlider.value = currentDamage;

		currentSpeed = startingSpeed;
		speedSlider.value = currentSpeed;

		//For save
		if (isSave == true) 
		{
			//Load the players stats
			healthSlider.value = currentHealth;
			damageSlider.value = currentDamage;
			speedSlider.value = currentSpeed;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" && !this.GetComponent<PlayerAttack>().getAttackStatus()) 
		{
			TakeDamage (damage);
		}

		if (other.gameObject.tag == "Item") 
		{
			PickUpItem (other);
		}
	}

	void TakeDamage(int amountHurt)
	{

		isDamaged = true;
		currentHealth -= amountHurt;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 ) 
		{
			Death ();
		}
	}

	void AddHealth(int amount)
	{
		currentHealth = currentHealth + amount;
		healthSlider.value = currentHealth;
	}

	void AddDamage(int amount)
	{
		currentDamage = currentDamage + amount;
		damageSlider.value = currentDamage + amount;
		
	}

	void AddSpeed(int amount)
	{
		currentSpeed = currentSpeed + amount;
		speedSlider.value = currentSpeed;
	}

	 void Death()
	{
		isDead = true;
		print ("DEAD ggez");
		GameObject.Find("Player").GetComponent<PlayerC> ().enabled = false;
		GameObject.Find("Player").GetComponent<PlayerAttack> ().enabled = false;
	}

	void PickUpItem(Collision2D other)
	{
		if (other.gameObject.name == "YarnBall") 
		{
			print ("YarnBall Collided");
			Destroy (other.gameObject);
			AddDamage (20);

			// Add item id to list
			itemList.Add (1);

		}

		if (other.gameObject.name == "RiceBall") 
		{
			print ("RiceBall Collided");
			Destroy (other.gameObject);
			AddSpeed(20);

			// Add item id to list
			itemList.Add (2);
		}

		if (other.gameObject.name == "RiceBowl") 
		{
			print ("RiceBowl Collided");
			Destroy (other.gameObject);
			AddHealth (20);

			// Add item id to list
			itemList.Add (3);
		}

		if (other.gameObject.name == "ChickenLeg")
		{
			print ("ChickenLeg Collided");
			Destroy (other.gameObject);
			AddHealth (10);

			// Add item id to list
			itemList.Add (4);

		}
		if (other.gameObject.name == "TunaCan")
		{
			print ("TunaCan Collided");
			Destroy (other.gameObject);
			AddHealth (20);

			// Add item id to list
			itemList.Add (5);

		}


	}
}
