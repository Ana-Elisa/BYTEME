using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawsMenu : MonoBehaviour 
{

	bool paused = false;
	public Canvas pawsMenu;
	public Button cont;
	public Button quit;

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
			pawsMenu.enabled = true;
			GameObject.Find ("Player").GetComponent<PlayerC> ().enabled = false;
			GameObject.Find ("Player").GetComponent<PlayerAttack> ().enabled = false;
			Button cBtn = cont.GetComponent<Button> ();
			Button qBtn = quit.GetComponent<Button> ();
			cBtn.onClick.AddListener (cOnClick);
			qBtn.onClick.AddListener (qOnClick);
			
		}
	}

	void cOnClick()
	{
		Time.timeScale = 1;
		paused = false;
		print ("resumed");
		pawsMenu.enabled = false;
		GameObject.Find("Player").GetComponent<PlayerC> ().enabled = true;
		GameObject.Find("Player").GetComponent<PlayerAttack> ().enabled = true;
	}

	void qOnClick()
	{
		Application.Quit ();
	}
		
}
