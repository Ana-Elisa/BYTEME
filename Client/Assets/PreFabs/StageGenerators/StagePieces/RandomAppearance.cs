using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppearance : MonoBehaviour {

	public float chanceToStay = 0.5f;

	// Use this for initialization
	void Start () {
		if (Random.value > chanceToStay) {
			Destroy (gameObject);
		}
	}
}
