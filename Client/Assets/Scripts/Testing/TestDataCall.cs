using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") 
		{
			Player player = FindObjectOfType (typeof(Player)) as Player;
			player.AddDefense (10);
			APIActions.postSave ();
			//string shit = "{\"user_name\":\"test\",\"attack\":0,\"defence\":10,\"speed\":0,\"health\":0,\"total_health\":0,\"next_level\":1,\"time\":\"00:00:00\"}";
			//JSONPlayer jsonPlayer = JsonUtility.FromJson<JSONPlayer> (shit);
			//print (jsonPlayer.attack);
		}

	}

}
