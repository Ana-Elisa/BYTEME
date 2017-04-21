using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//able to use load scene function
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;



public class SwitchScenes : MonoBehaviour {

	public static bool GENSTAGE = false;
	public static bool LOADSTAGE = false;
	public static bool GENPLAYER = false;
	public static bool SETSTAGE = false;
	public static bool CALLSTAGEGEN = false;
	public static int frameCount;
	int stageHeight, stageWidth;
	GameObject ui;
	public Transform prePlayer;
	private Button backButton;
    private Button enterGameButton;
    private Button forgotPasswordButton;
    private Button newUserButton;
    private Button submitButton;
	private Button playAgainButton;
	private Button leaderBoardButton;
	private Button quitButton;
    private InputField usernameInputField;
    private InputField passwordInputField;
    private InputField emailInputField;
    private String username = "";
    private String password = "";
    private String email = "";
    private Button registerButton;
	private string token; 
   
	public static bool showPopup = false;
	public static string popupText = "";

	// Use this for initialization
	void Start () {
        UnityEngine.Object.DontDestroyOnLoad(this);
       
    }
	
	// Update is called once per frame
	void Update () {
		if (GENSTAGE && Time.frameCount == frameCount + 30) {
			print ("Generating...");

			try {
				Player player = FindObjectOfType (typeof(Player)) as Player;
				player.nextLevel += 1;
				ReturnObject result = APIActions.postSave();
				bool status = result.retStatus;
				popupText = result.text;

				if (status == false) {
					showPopup = true;
				}
			} catch (Exception ex){
				print (ex);
			}

			LoadingScreen loadingScreen = FindObjectOfType (typeof(LoadingScreen)) as LoadingScreen;
			loadingScreen.show = true;

			Time.timeScale = 0;

			//SceneManager.LoadScene("Ana'esNEWLevel");

			GENSTAGE = false;
			LOADSTAGE = true;
			frameCount = Time.frameCount;
		}
		if (LOADSTAGE && Time.frameCount == frameCount + 30) {
			SceneManager.LoadScene ("Ana'esNEWLevel");
			LOADSTAGE = false;
			GENPLAYER = true;
			frameCount = Time.frameCount;
		}
		if (GENPLAYER && Time.frameCount == frameCount + 30) {
			print ("player time...");
			Instantiate (prePlayer);

			ui = GameObject.Find ("HUDCanvas");
			ui.SetActive (false);

			GENPLAYER = false;
			SETSTAGE = true;
			frameCount = Time.frameCount;
		}
		if (SETSTAGE && Time.frameCount == frameCount + 30) {
			print ("Setting...");
			Player player = FindObjectOfType (typeof(Player)) as Player;
			stageHeight = player.nextLevel + 1;
			stageWidth = player.nextLevel + 1;
			print ("Width " + stageWidth + " Hight " + stageHeight);

			StageGeneratorScript stageGen = FindObjectOfType (typeof(StageGeneratorScript)) as StageGeneratorScript;
			stageGen.stageHeight = stageHeight;
			stageGen.stageWidth = stageWidth;
			SETSTAGE = false;
			stageGen.GenerateStage ();

			LoadingScreen loadingScreen = FindObjectOfType (typeof(LoadingScreen)) as LoadingScreen;
			loadingScreen.show = false;
			ui.SetActive (true);
			Time.timeScale = 1;
		}
       
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


		} else if (scene.name == "NewUserScreen") {
			registerButton = GameObject.Find ("Register").GetComponent<Button> ();
			passwordInputField = GameObject.Find ("PasswordInputField").GetComponent<InputField> ();
			passwordInputField.onEndEdit.AddListener (delegate {
				UpdatePassword (passwordInputField.text);
			});
			usernameInputField = GameObject.Find ("UsernameInputField").GetComponent<InputField> ();
			usernameInputField.onEndEdit.AddListener (delegate {
				UpdateUserName (usernameInputField.text);
			});
			//could make these into call methods..
			emailInputField = GameObject.Find ("EmailInputField").GetComponent<InputField> ();
			emailInputField.onEndEdit.AddListener (delegate {
				UpdateEmail (emailInputField.text);
			});
			backButton = GameObject.Find ("BackButton").GetComponent<Button> ();
			backButton.onClick.AddListener (loadLoginScreen);

			//SUBMIT POST/GET METHOD HERE>
			registerButton.onClick.AddListener (createUser);
		} else if (scene.name == "GameOver") {
			playAgainButton = GameObject.Find ("PlayAgain").GetComponent<Button> ();
			leaderBoardButton = GameObject.Find ("Leaderboards").GetComponent <Button> ();
			quitButton = GameObject.Find ("Quit").GetComponent<Button> ();

			playAgainButton.onClick.AddListener (playAgain);
			leaderBoardButton.onClick.AddListener (leaderboard);
			quitButton.onClick.AddListener (quit);
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
		ReturnObject result = APIActions.login(username, password);
		bool status = result.retStatus;
		popupText = result.text;

		if (status == true) {
			//SceneManager.LoadScene ("Ana'esNEWLevel");
			GENSTAGE = true;
			frameCount = Time.frameCount;
		} else {
			showPopup = true;
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
	private void createUser() {
		ReturnObject result = APIActions.createUser(username, email, password);
		bool status = result.retStatus;
		popupText = result.text;

		if (status == true) {
			GENSTAGE = true;
			frameCount = Time.frameCount;
			APIActions.login (username, password);
		} else {
			showPopup = true;
		}

	}
	private void playAgain() {
		GENSTAGE = true;
		frameCount = Time.frameCount;
	}
	private void leaderboard() {
		Application.OpenURL ("https://byteme.online/charts/");
	}
	private void quit() {
		Application.Quit ();
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
		  
}
