using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//able to use load scene function
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;



public class SwitchScenes : MonoBehaviour {
    private Button enterGameButton;
    private Button forgotPasswordButton;
    private Button newUserButton;
    private Button submitButton;
    private InputField usernameInputField;
    private InputField passwordInputField;
    private InputField emailInputField;
    private String username;
    private String password;
    private String email;
    private Button registerButton;
	private string token; 
   
	string url;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		url = "https://byteme.online/api/token/";

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

    private void loadGame()
    {
        //Here is where we would validate username and password, GEt/Post 
		WWWForm loginInfo = new WWWForm();
		loginInfo.AddField ("username", username);
		loginInfo.AddField ("password", password);

		var header = loginInfo.headers;
		header ["content-type"] = "application/json";
        //method to service 

		UnityWebRequest www = UnityWebRequest.Post (url, loginInfo);
		
		//WWW www = new WWW(url, loginInfo.data);

		coroutine = LoginToGame (www);
		StartCoroutine(coroutine);
		/*if (token != null) {
			
		}*/
		SceneManager.LoadScene("Test");
	
      
       
    }

	IEnumerator LoginToGame(UnityWebRequest www){
		
		yield return www.Send();

		if (www.isDone) {
			Debug.Log (www.downloadHandler.text);
			JSONObject j = new JSONObject (www.downloadHandler.text);
			accessData (j);

		} else {
			Debug.Log (www.error);
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
