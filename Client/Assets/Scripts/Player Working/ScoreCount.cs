using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour {

	public int killCounter = 0;

	public void SetScore(int score)
	{
		killCounter = score;
	}
}
