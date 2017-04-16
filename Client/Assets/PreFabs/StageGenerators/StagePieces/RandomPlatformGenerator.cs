using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatformGenerator : MonoBehaviour {
	public float weight = 0.5f;
	public Transform floor;

	// Use this for initialization
	void Start () {
		int width = (int)transform.localScale.x;
		int height = (int)transform.localScale.y;

		bool[,] choices = RandomArray (width, height, weight);
		while (!Or (choices)) {
			choices = RandomArray (width, height, weight);
		}

		for (int i = 0; i < choices.GetLength (0); i++) {
			for (int j = 0; j < choices.GetLength (1); j++) {
				if (choices [i,j]) {
					Instantiate (floor, transform.position + Vector3.right * i + Vector3.down * j, Quaternion.identity);
				}
			}
		}
		Destroy (gameObject);
	}

	bool[,] RandomArray (int width, int height, float weight){
		bool[,] ret = new bool[width,height];
		for (int i = 0; i < ret.GetLength (0); i++) {
			for (int j = 0; j < ret.GetLength (1); j++) {
				ret [i,j] = Random.value < weight;
			}
		}
		return ret;
	}

	bool Or (bool[,] list){
		for (int i = 0; i < list.GetLength (0); i++) {
			for (int j = 0; j < list.GetLength (1); j++) {
				if (list [i,j]) {
					return true;
				}
			}
		}
		return false;
	}
}
