using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawsMenu : MonoBehaviour 
{

	bool paused = false;
	public Canvas pawsMenu;

	void Start()
	{
		pawsMenu.enabled = false; 
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Time.timeScale = 0;
			print ("paused");
			paused = true;
			if (paused) 
			{
				pawsMenu.enabled = true;
			} 
			else if (!paused) 
			{
				pawsMenu.enabled = false;
			}
		}
	}

	void onClick()
	{
		//print ("continue");
		//paused = false;
		//Time.timeScale = 1;
	}
}
