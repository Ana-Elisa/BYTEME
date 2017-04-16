using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReturnObject {
	public bool retStatus;
	public string text;

	public ReturnObject (bool status, string text) {
		this.retStatus = status;
		this.text = text;
	}

}

[System.Serializable]
public class JSONPlayer : MonoBehaviour {
	public List<int> itemList = new List<int>();

	//All the Health stuff
	public int currentHealth;

	//All the Damage stuff
	public int currentDamage;

	//All the speed stuff
	public int currentSpeed;

	//All the defense stuff
	public int currentDefense;

	public JSONPlayer() {
		Player player = FindObjectOfType (typeof(Player)) as Player;
		currentHealth = player.currentHealth;
		currentDamage = player.currentDamage;
		currentSpeed = player.currentSpeed;
		currentDefense = player.currentDefense;
	}

	public void setPlayerStats() {
		Player player = FindObjectOfType (typeof(Player)) as Player;
		player.SetHealth (currentHealth);
		player.SetDamage (currentDamage);
		player.SetSpeed (currentSpeed);
		player.SetDefense (currentDefense);
	}

	public string ToJSON() {
		return JsonUtility.ToJson(this);
	}

	public static JSONPlayer CreateFromJSON(string jsonString) {
		return JsonUtility.FromJson<JSONPlayer>(jsonString);
	}

}



public class APIActions : MonoBehaviour {

	private static string token;
	private IEnumerator coroutine;

	public static ReturnObject login(string username, string password) {
		string url = "https://byteme.online/api/token/";
		bool result = false;
		string result_text = "";

		WWWForm loginInfo = new WWWForm ();
		loginInfo.AddField ("username", username);
		loginInfo.AddField ("password", password);

		UnityWebRequest request = UnityWebRequest.Post (url, loginInfo);
	

		request.Send ();

		int limit = 3000;
		int counter = 0;
		while ((!request.isDone || !request.downloadHandler.isDone) && counter != limit) {
			//Keep looping until the request finishes or errors
			System.Threading.Thread.Sleep(1);
			counter++;
		}

		print (limit + " " + counter);
		if (request.isError || counter == limit) {
			return new ReturnObject (false, "Could not connect to server");
		}

		if (request.responseCode == 200 ) {
			JSONObject obj = new JSONObject (request.downloadHandler.text);
			token = obj.GetField ("token").ToString();
			result = true;
		} else {
			JSONObject obj = new JSONObject (request.downloadHandler.text);
			List<string> keyList = obj.keys;

			foreach(string item in keyList) {
				result_text += item;
				result_text += ": " + obj.GetField (item)[0].ToString().Replace("\"", "") + "\n";
			}

			print (result_text);
			result = false;
		}

		return new ReturnObject (result, result_text);
			
	}

	public static ReturnObject createUser(string username, string email, string password) {
		string url = "https://byteme.online/api/user/";
		bool result = false;
		string result_text = "";

		WWWForm userInfo = new WWWForm ();
		userInfo.AddField ("username", username);
		userInfo.AddField ("Email", email);
		userInfo.AddField ("password", password);

		UnityWebRequest request = UnityWebRequest.Post (url, userInfo);


		request.Send ();

		int limit = 3000;
		int counter = 0;
		while ((!request.isDone || !request.downloadHandler.isDone) && counter != limit) {
			//Keep looping until the request finishes or errors
			System.Threading.Thread.Sleep(1);
			counter++;
		}

		print (limit + " " + counter);
		if (request.isError || counter == limit) {
			return new ReturnObject (false, "Could not connect to server");
		}

		if (request.responseCode == 200 ) {
			result = true;
			result_text = "Successfull! You may now login.";
		} else {
			JSONObject obj = new JSONObject (request.downloadHandler.text);
			List<string> keyList = obj.keys;

			foreach(string item in keyList) {
				result_text += item;
				result_text += ": " + obj.GetField (item)[0].ToString().Replace("\"", "") + "\n";
			}

			print (result_text);
			result = false;
		}

		return new ReturnObject (result, result_text);

	}

    public static ReturnObject postSave() {
        string url = "https://byteme.online/api/save/";
        bool result = false;
        string result_text = "";

		JSONPlayer jsonPlayer = new JSONPlayer ();
		string body = jsonPlayer.ToJSON ();
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(body);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);

        //print(userInfo.ToString());
        UnityWebRequest request = UnityWebRequest.Post(url, body);
        request.SetRequestHeader ("Authorization", "Token eee37ef9408de55176db375bedcf63e3f24c50f6");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = uh;


        request.Send();

        int limit = 3000;
        int counter = 0;
        while ((!request.isDone || !request.downloadHandler.isDone) && counter != limit) {
            //Keep looping until the request finishes or errors
            System.Threading.Thread.Sleep(1);
            counter++;
        }

        print(limit + " " + counter);
        if (request.isError || counter == limit) {
            return new ReturnObject(false, "Could not connect to server");
        }

        if (request.responseCode == 200) {
            result = true;
            print("Successfull!");
        }
        else {
            print(request.responseCode);
            print(request.downloadHandler.text);
            JSONObject obj = new JSONObject(request.downloadHandler.text);
            List<string> keyList = obj.keys;

            foreach (string item in keyList) {
                result_text += item;
                result_text += " - " + obj.GetField(item).ToString().Replace("\"", "") + "\n";
            }

            print(result_text);
            result = false;
        }

        return new ReturnObject(result, result_text);

    }

	public static ReturnObject getSave() {
		string url = "https://byteme.online/api/save/";
		bool result = false;
		string result_text = "";

		UnityWebRequest request = UnityWebRequest.Get(url);
		request.SetRequestHeader ("Authorization", "Token eee37ef9408de55176db375bedcf63e3f24c50f6");
		request.SetRequestHeader("Content-Type", "application/json");


		request.Send();

		int limit = 3000;
		int counter = 0;
		while ((!request.isDone || !request.downloadHandler.isDone) && counter != limit) {
			//Keep looping until the request finishes or errors
			System.Threading.Thread.Sleep(1);
			counter++;
		}

		print(limit + " " + counter);
		if (request.isError || counter == limit) {
			return new ReturnObject(false, "Could not connect to server");
		}

		if (request.responseCode == 200) {
			print(request.responseCode);
			print(request.downloadHandler.text);
			JSONObject obj2 = new JSONObject(request.downloadHandler.text);
			string obj3 = obj2.GetField ("results")[0].ToString ();
			print (obj3.ToString());
			JSONObject obj = new JSONObject(obj3.ToString());

			var items = obj.GetField ("item_list").ToDictionary();
			print (items);

		} else {
			string json = request.downloadHandler.text;
			JSONPlayer jsonPlayer = JsonUtility.FromJson<JSONPlayer> (json);
			jsonPlayer.setPlayerStats ();
		}

		return new ReturnObject(result, result_text);

	}

}
