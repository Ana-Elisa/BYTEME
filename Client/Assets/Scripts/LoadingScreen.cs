using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public bool show = false;
	public Canvas loadingScreen;

	void Start()
	{
		loadingScreen.enabled = true; 
	}

	void Update ()
	{
		if (show) {
			Time.timeScale = 0;
			loadingScreen.enabled = true;
		} 
		else 
		{
			Time.timeScale = 1;
			loadingScreen.enabled = false;
		}
	}

	/*public void show() {
		Time.timeScale = 0;
		print ("loading");
		loadingScreen.enabled = true;
	}*/

	public void hide() {
		Time.timeScale = 1;
		loadingScreen.enabled = false;
	}
		
}
