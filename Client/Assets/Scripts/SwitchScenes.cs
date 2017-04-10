using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//able to use load scene function
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;



public class SwitchScenes : MonoBehaviour {
	private APIActions api = new APIActions();

    private Button enterGameButton;
    private Button forgotPasswordButton;
    private Button newUserButton;
    private Button submitButton;
    private InputField usernameInputField;
    private InputField passwordInputField;
    private InputField emailInputField;
    private String username = "";
    private String password = "";
    private String email = "";
    private Button registerButton;
	private string token; 
   
	private bool showPopup = false;
	string popupText = "";

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
			enterGameButton = GameObject.Find ("EnterGameButton").GetComponent<Button> ();
			forgotPasswordButton = GameObject.Find ("ForgotPasswordButton").GetComponent<Button> ();
			newUserButton = GameObject.Find ("NewUserButton").GetComponent<Button> ();
			//setup input fields
			usernameInputField = GameObject.Find ("UsernameInputField").GetComponent<InputField> ();
			passwordInputField = GameObject.Find ("PasswordInputField").GetComponent<InputField> ();
			//after user is done editing input, 
			usernameInputField.onEndEdit.AddListener (delegate {
				UpdateUserName (usernameInputField.text);
			});
			passwordInputField.onEndEdit.AddListener (delegate {
				UpdatePassword (passwordInputField.text);
			});




			//if enter game button clicked go to game scene

			enterGameButton.onClick.AddListener (loadGame);
			//forgot password button clicked -> forgotpasswordScreen
			forgotPasswordButton.onClick.AddListener (loadForgotPasswordScreen);
			//new User clicked -> new User screen
			newUserButton.onClick.AddListener (loadNewUserScreen);


		}
        else if (scene.name == "NewUserScreen") {
            registerButton = GameObject.Find("Register").GetComponent<Button>();
            passwordInputField = GameObject.Find("PasswordInputField").GetComponent<InputField>();
            passwordInputField.onEndEdit.AddListener(delegate { UpdatePassword(passwordInputField.text); });
            usernameInputField = GameObject.Find("UsernameInputField").GetComponent<InputField>();
            usernameInputField.onEndEdit.AddListener(delegate { UpdateUserName(usernameInputField.text); });
            //could make these into call methods..
            emailInputField = GameObject.Find("EmailInputField").GetComponent<InputField>();
            emailInputField.onEndEdit.AddListener(delegate { UpdateEmail(emailInputField.text); });


            //SUBMIT POST/GET METHOD HERE>
            registerButton.onClick.AddListener(loadLoginScreen);
        }
        
    }
    private void UpdateEmail(string arg0) {
        email = arg0;
        Debug.Log(arg0);
    }
    private void UpdatePassword(string arg0) {
        password = arg0;
        Debug.Log(arg0);
        
    }
    private void UpdateUserName(string arg0) {
        username = arg0;
        Debug.Log(arg0);
    }

    private void loadGame() {
		ReturnObject result = api.login(username, password);
		bool status = result.retStatus;
		popupText = result.text;

		if (status == true)
			SceneManager.LoadScene ("PlayerHealth");
		else {
			showPopup = true;
		}

    }
		
	void OnGUI()
	{
		if (showPopup)
		{
			GUI.color = new Color(1,1,1,100f);
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 250), ShowGUI, "Error");
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 250), ShowGUI, "Error");
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 250), ShowGUI, "Error");
			GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 250), ShowGUI, "Error");

		}
	}

	void ShowGUI(int windowID)
	{
		// You may put a label to show a message to the player

		GUI.Label(new Rect(65, 40, 200, 200), popupText);

		// You may put a button to close the pop up too

		if (GUI.Button(new Rect(50, 150, 75, 30), "OK"))
		{
			showPopup = false;
			// you may put other code to run according to your game too
		}

	}

	void accessData(JSONObject obj){
		token = obj.GetField ("token").ToString();
	
	}
    private void loadForgotPasswordScreen() {
		Application.OpenURL ("https://byteme.online/password_reset/");
        //SceneManager.LoadScene("ForgotPasswordScreen");
    }
    private void loadNewUserScreen() {
        SceneManager.LoadScene("NewUserScreen");
    }
    private void loadLoginScreen() {
        SceneManager.LoadScene("LoginScreen");
    }

    
}
