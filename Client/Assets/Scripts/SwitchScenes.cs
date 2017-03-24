using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//able to use load scene function
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SwitchScenes : MonoBehaviour {
    private Button enterGameButton;
    private Button forgotPasswordButton;
    private Button newUserButton;

	// Use this for initialization
	void Start () {
        UnityEngine.Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    //use this to attach scene events
    private void OnEnable()
    {
        SceneManager.sceneLoaded += setupScene;

    }

    //use this to detach scene events
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= setupScene;
    }

    //setup scene before calling scene
    private void setupScene(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "LoginScreen")
        {
            //setup buttons
            enterGameButton = GameObject.Find("EnterGameButton").GetComponent<Button>();
            forgotPasswordButton = GameObject.Find("ForgotPasswordButton").GetComponent<Button>();
            newUserButton = GameObject.Find("NewUserButton").GetComponent<Button>();
            //if enter game button clicked go to game scene
            enterGameButton.onClick.AddListener(loadGame);
            //forgot password button clicked -> forgotpasswordScreen
            forgotPasswordButton.onClick.AddListener(loadForgotPasswordScreen);
            //new User clicked -> new User screen
            newUserButton.onClick.AddListener(loadNewUserScreen);


        }
      /*  else if (scene.name == "ForgotPasswordScreen")
        {


        }
        else if (scene.name == "NewUserScreen") {

        }
        */
    }

    private void loadGame()
    {
        SceneManager.LoadScene("Test");
    }
    private void loadForgotPasswordScreen() {
        SceneManager.LoadScene("ForgotPasswordScreen");
    }
    private void loadNewUserScreen() {
        SceneManager.LoadScene("NewUserScreen");
    }

    
}
