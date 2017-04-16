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
	public List<int> item_list = new List<int>();

	//All the Health stuff
	public int health;
	public int total_health;

	//All the Damage stuff
	public int attack;

	//All the speed stuff
	public int speed;

	//All the defense stuff
	public int defence;

	public JSONPlayer() {
		Player player = FindObjectOfType (typeof(Player)) as Player;
		health = player.currentHealth;
		total_health = player.maxHealth;
		attack = player.currentDamage;
		speed = player.currentSpeed;
		defence = player.currentDefense;
		item_list = player.itemList;
	}

	public void setPlayerStats() {
		Player player = FindObjectOfType (typeof(Player)) as Player;
		player.SetHealth (health);
		player.SetDamage (attack);
		player.SetSpeed (speed);
		player.SetDefense (defence);
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
		request.SetRequestHeader ("Authorization", "Token " + token);
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
                result_text += ": " + obj.GetField(item).ToString().Replace("\"", "") + "\n";
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
		request.SetRequestHeader ("Authorization", "Token " + token);
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
			string json = request.downloadHandler.text;

			JSONObject obj = new JSONObject(json);
			long count = obj.GetField("count").i;

			if (count == 1) {
				JSONObject saveData = obj.GetField("results");
				print (saveData [0].ToString ());
				JSONPlayer jsonPlayer = JsonUtility.FromJson<JSONPlayer> (saveData[0].ToString());
				jsonPlayer.setPlayerStats ();
			} else {
				Player player = FindObjectOfType (typeof(Player)) as Player;
				player.SetHealth (100);
				player.SetDamage (20);
				player.SetSpeed (20);
				player.SetDefense (20);
			}
		} else {

			// FOR TESTING DO NOT LEAVE IN PRODUCTION!!!!!!! (... or should we?)

			Player player = FindObjectOfType (typeof(Player)) as Player;
			player.SetHealth (100);
			player.SetDamage (20);
			player.SetSpeed (20);
			player.SetDefense (20);

			/*
			print(request.responseCode);
			print(request.downloadHandler.text);
			JSONObject obj = new JSONObject(request.downloadHandler.text);
			List<string> keyList = obj.keys;

			foreach (string item in keyList) {
				result_text += item;
				result_text += ": " + obj.GetField(item).ToString().Replace("\"", "") + "\n";
			}

			print(result_text);
			result = false;*/
		}

		return new ReturnObject(result, result_text);

	}

}
