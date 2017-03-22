using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//able to use load scene function
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SwitchScenes : MonoBehaviour {
    private Button enterGameButton;

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

        if (scene.name == "LoginScreen") {
            //setup buttons
            enterGameButton = GameObject.Find("EnterGameButton").GetComponent<Button>();
            enterGameButton.onClick.AddListener(loadGame);
        }
    }

    private void loadGame()
    {
        SceneManager.LoadScene("Test");
    }

    
}
