using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSpawner : MonoBehaviour {

	public Transform leaderboard;

	// Use this for initialization
	void Start () {
		Instantiate (leaderboard, transform);
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (gameObject.GetComponentInChildren<OpenLeaderboards> () == null) {
				Instantiate (leaderboard, transform);
			}
		}
	}
}
