using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataCall : MonoBehaviour {

	private bool postSave = false;
	private int frameCount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (postSave && Time.frameCount == frameCount + 10) {
			print ("posting");
			LoadingScreen loadingScreen = FindObjectOfType (typeof(LoadingScreen)) as LoadingScreen;
			APIActions.postSave ();
			loadingScreen.show = false;
			postSave = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") 
		{
			//Player player = FindObjectOfType (typeof(Player)) as Player;
			LoadingScreen loadingScreen = FindObjectOfType (typeof(LoadingScreen)) as LoadingScreen;
			//player.AddDefense (10);
			loadingScreen.show = true;
			postSave = true;

			frameCount = Time.frameCount;
		}

	}

}
