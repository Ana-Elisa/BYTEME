using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ReturnObject {
	public bool retStatus;
	public string text;

	public ReturnObject (bool status, string text) {
		this.retStatus = status;
		this.text = text;
	}

}

[System.Serializable]
public class JSONPlayer {

	//Item list
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

	//Next level
	public int next_level;

	//Time
	public string time;

	//Garbage needed to make serializer work
	string user_name;


	public JSONPlayer(Player player) {
		health = player.currentHealth;
		total_health = player.maxHealth;
		attack = player.currentDamage;
		speed = player.currentSpeed;
		defence = player.currentDefense;
		item_list = player.itemList;
		next_level = player.nextLevel;

		int remainder;
		float total_time = APIActions.time + Time.time;
		int hours = (int)total_time / 3600;
		remainder = (int)total_time % 3600;
		int minutes = remainder / 60;
		int seconds = remainder % 60;

		time = (hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString());
	}

	public void setPlayerStats(Player player) {
		player.SetHealth (health);
		player.SetDamage (attack);
		player.SetSpeed (speed);
		player.SetDefense (defence);
		player.SetNextLevel (next_level);
		player.SetItemList (item_list);
	}

	public string ToJSON() {
		return JsonUtility.ToJson(this);
	}

}


public class APIActions : MonoBehaviour {

	private static string token;
	public static float time;

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
			token = obj.GetField ("token").ToString().Replace("\"", "");
			result = true;
		} else {
			try {
				JSONObject obj = new JSONObject (request.downloadHandler.text);
				List<string> keyList = obj.keys;

				foreach(string item in keyList) {
					result_text += item;
					result_text += ": " + obj.GetField (item)[0].ToString().Replace("\"", "") + "\n";
				}

				print (result_text);
				result = false;
			} catch (Exception ex) {
				result_text = request.responseCode + ": could not connect to server";
				result = false;
			}
		}

		return new ReturnObject (result, result_text);
			
	}

	public static ReturnObject createUser(string username, string email, string password) {
		string url = "https://byteme.online/api/user/";
		bool result = false;
		string result_text = "";

		WWWForm userInfo = new WWWForm ();
		userInfo.AddField ("username", username);
		userInfo.AddField ("email", email);
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

		print (request.responseCode);
		if (request.responseCode == 201 ) {
			result = true;
			result_text = "Successfull! You may now login.";
		} else {
			try{
				JSONObject obj = new JSONObject (request.downloadHandler.text);
				List<string> keyList = obj.keys;

				foreach(string item in keyList) {
					result_text += item;
					result_text += ": " + obj.GetField (item)[0].ToString().Replace("\"", "") + "\n";
				}

				print (result_text);
				result = false;
			} catch (Exception ex) {
				result_text = request.responseCode + ": could not connect to server";
				result = false;
			}
		}

		return new ReturnObject (result, result_text);

	}

    public static ReturnObject postSave() {
		print (token);

        string url = "https://byteme.online/api/save/";
        bool result = false;
        string result_text = "";

		JSONPlayer jsonPlayer = new JSONPlayer (FindObjectOfType (typeof(Player)) as Player);
		string body = jsonPlayer.ToJSON ();
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(body);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);

        //print(userInfo.ToString());
        UnityWebRequest request = UnityWebRequest.Post(url, body);
		request.SetRequestHeader ("Authorization", string.Concat ("Token ", token));
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

        if (request.responseCode == 201) {
            result = true;
            print("Successfull!");
        }
        else {
			try {
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
			} catch (Exception ex) {
				result_text = request.responseCode + ": could not connect to server";
				result = false;
			}
        }

        return new ReturnObject(result, result_text);

    }

	public static ReturnObject getSave() {
		string url = "https://byteme.online/api/save/";
		bool result = false;
		string result_text = "";

		UnityWebRequest request = UnityWebRequest.Get(url);
		request.SetRequestHeader ("Authorization", string.Concat ("Token ", token));
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
			print (json);

			JSONObject obj = new JSONObject(json);
			long count = obj.GetField("count").i;

			if (count == 1) {
				JSONObject saveData = obj.GetField("results");
				print (saveData [0].ToString ());
				JSONPlayer jsonPlayer = JsonUtility.FromJson<JSONPlayer> (saveData[0].ToString());
				jsonPlayer.setPlayerStats (FindObjectOfType (typeof(Player)) as Player);

				string formatted_time = jsonPlayer.time;
				string[] splitted = formatted_time.Split(':');
				int hours = int.Parse (splitted [0]) * 3600;
				int minutes = int.Parse (splitted [1]) * 60;
				int seconds = int.Parse (splitted [2]);

				time = hours + minutes + seconds;
				result = true;

			} else {
				Player player = FindObjectOfType (typeof(Player)) as Player;
				player.SetHealth (Player.defaultHealth);
				player.SetDamage (Player.defaultDamage);
				player.SetSpeed (Player.defaultSpeed);
				player.SetDefense (Player.defaultDefense);
				player.SetNextLevel (1);
				result = true;
			}
		} else {
			print (request.responseCode);
			result_text = request.responseCode + ": could not connect to server";
			result = false;
		}

		return new ReturnObject(result, result_text);

	}

}
