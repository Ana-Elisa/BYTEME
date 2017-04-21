using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	//Default stats
	public static int defaultHealth = 100;
	public static int defaultDamage = 10;
	public static int defaultSpeed = 0;
	public static int defaultDefense = 0;

	public List<int> itemList = new List<int>();

	//All the Health stuff
	public Slider healthSlider;
	public int currentHealth;
	public int maxHealth;

	//All the Damage stuff
	public Slider damageSlider;
	public int currentDamage;

	//All the speed stuff
	public Slider speedSlider;
	public int currentSpeed;
	public int newSpeed;

	//All the defense stuff
	public Slider defenseSlider;
	public int currentDefense;

	//All other stuff
	public int nextLevel;

	//Set-up
	GameObject player;
	bool isDead;
	bool isDamaged;
	public int damage = 20;

	//For Save
	bool showPopup = false;
	string popupText = "";


	// Use this for initialization
	void Start () {
		ReturnObject result = APIActions.getSave();
		bool status = result.retStatus;
		SwitchScenes.popupText = result.text;

		if (status == false) {
			SwitchScenes.showPopup = true;
			print ("Show popup");
			SwitchScenes.GENSTAGE = true;
		}

		if (currentSpeed > 0) {
			int speedo;
			speedo = (int)GetComponent<PlayerC> ().speed;
			//print (speedo); //This confirmed the value gotten

			int addSpeed;
			addSpeed = (int)((speedSlider.value / 100) * currentSpeed);
			//print (addSpeed);

			newSpeed = speedo + addSpeed;
			//print (newSpeed);
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

		if (damageSlider.value > 0) {
			int dmgReduction;
			dmgReduction = (int)((damageSlider.value / 100) * amountHurt);
			amountHurt = amountHurt - dmgReduction;

			currentHealth -= amountHurt;
			healthSlider.value = currentHealth;

			//DEBUGGING prints
			//print (amountHurt);
			//print (currentHealth);
		} else {

			currentHealth -= amountHurt;
			healthSlider.value = currentHealth;
		}

		if (currentHealth <= 0 ) 
		{
			Death ();
		}
	}

	public void AddHealth(int amount)
	{
		currentHealth = currentHealth + amount;
		healthSlider.value = currentHealth;

		if (currentHealth > 100) {
			SetHealth (100);
		}
	}

	public void AddDamage(int amount)
	{
		currentDamage = currentDamage + amount;
		damageSlider.value = currentDamage;
		if (currentDamage > 100) {
			SetDamage (100);
		}
		
	}

	public void AddSpeed(int amount)
	{
		currentSpeed = currentSpeed + amount;
		speedSlider.value = currentSpeed;

		if (currentSpeed > 0 && currentSpeed != 100) {
			int speedo;
			speedo = (int)GetComponent<PlayerC> ().speed;
			//print (speedo); //This confirmed the value gotten

			int addSpeed;
			addSpeed = (int)((speedSlider.value / 100) * currentSpeed);
			//print (addSpeed);

			newSpeed = speedo + addSpeed;
			//print (newSpeed);
		}

		if (currentSpeed > 100) {
			SetSpeed (100);
		}
	}

	public void AddDefense(int amount)
	{
		currentDefense = currentDefense + amount;
		defenseSlider.value = currentDefense;
		if (currentDefense > 99) {
			SetDefense (99);
		}
	}

	public void SetHealth(int amount)
	{
		currentHealth = amount;
		healthSlider.value = currentHealth;
		if (currentHealth > 100) {
			SetHealth (100);
		}
	}

	public void SetDamage(int amount)
	{
		currentDamage = amount;
		damageSlider.value =  currentDamage;
	}

	public void SetSpeed(int amount)
	{
		currentSpeed = amount;
		speedSlider.value = currentSpeed;
	}

	public void SetDefense(int amount)
	{
		currentDefense = amount;
		defenseSlider.value = currentDefense;
	}

	public void SetNextLevel(int level)
	{
		nextLevel = level;
	}

	public void SetItemList(List<int> items)
	{
		itemList = items;
	}

	 void Death()
	{
		isDead = true;
		print ("DEAD ggez");

		nextLevel = 1;
		currentHealth = defaultHealth;
		currentSpeed = defaultSpeed;
		currentDamage = defaultDamage;
		currentDefense = defaultDefense;
		APIActions.postSave ();

		SceneManager.LoadScene("GameOver");
	}

	void PickUpItem(Collision2D other)
	{

		if (other.gameObject.name.Contains("YarnBall") == true) 
		{
			print ("YarnBall Collided");
			Destroy (other.gameObject);
			AddDamage (20);

			// Add item id to list
			itemList.Add (1);

		}

		if (other.gameObject.name.Contains("RiceBall") == true) 
		{
			print ("RiceBall Collided");
			Destroy (other.gameObject);
			AddSpeed(20);

			// Add item id to list
			itemList.Add (2);
		}

		if (other.gameObject.name.Contains("RiceBowl") == true) 
		{
			print ("RiceBowl Collided");
			Destroy (other.gameObject);
			AddHealth (20);

			// Add item id to list
			itemList.Add (3);
		}

		if (other.gameObject.name.Contains("ChickenLeg") == true)
		{
			print ("ChickenLeg Collided");
			Destroy (other.gameObject);
			AddHealth (10);

			// Add item id to list
			itemList.Add (4);

		}
		if (other.gameObject.name.Contains("TunaCan") == true)
		{
			print ("TunaCan Collided");
			Destroy (other.gameObject);
			AddHealth (20);

			// Add item id to list
			itemList.Add (5);

		}
		if (other.gameObject.name.Contains("Litter") == true)
		{
			print ("Litter Collided");
			Destroy (other.gameObject);
			AddDefense (2);

			// Add item id to list
			itemList.Add (6);

		}

		if (other.gameObject.name.Contains("redStocking") == true)
		{
			print ("Stocking Collided");
			Destroy (other.gameObject);
			AddSpeed(5);

			// Add item id to list
			itemList.Add (7);
		}

		if (other.gameObject.name.Contains("openBox") == true)
        {
            print("Box Collided");
            Destroy(other.gameObject);
            AddDefense(5);

            // Add item id to list
            itemList.Add(8);
        }

		if (other.gameObject.name.Contains("BlueBall") == true)
        {
            print("BlueBall Collided");
            Destroy(other.gameObject);
            AddDamage(5);

            // Add item id to list
            itemList.Add(9);
        }

		if (other.gameObject.name.Contains("CatFood") == true)
        {
            print("Cat Food Collided");
            Destroy(other.gameObject);
            AddHealth(4);

            // Add item id to list
            itemList.Add(10);
        }
    }
		
}
