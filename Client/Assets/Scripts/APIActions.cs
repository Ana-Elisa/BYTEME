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

public class APIActions : MonoBehaviour {

	private string token;
	private IEnumerator coroutine;

	public ReturnObject login(string username, string password) {
		string url = "https://byteme.online/api/token/";
		bool result = false;
		string result_text = "";

		WWWForm loginInfo = new WWWForm ();
		loginInfo.AddField ("username", username);
		loginInfo.AddField ("password", password);

		UnityWebRequest request = UnityWebRequest.Post (url, loginInfo);
		//request.SetRequestHeader ("content-type", "application/json");
	

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
				result_text += " - " + obj.GetField (item)[0].ToString().Replace("\"", "") + "\n";
			}

			print (result_text);
			result = false;
		}

		return new ReturnObject (result, result_text);
			
	}

	public ReturnObject createUser(string username, string email, string password) {
		string url = "https://byteme.online/api/user/";
		bool result = false;
		string result_text = "";

		WWWForm userInfo = new WWWForm ();
		userInfo.AddField ("username", username);
		userInfo.AddField ("Email", email);
		userInfo.AddField ("password", password);


		UnityWebRequest request = UnityWebRequest.Post (url, userInfo);
		//request.SetRequestHeader ("content-type", "application/json");


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
				result_text += " - " + obj.GetField (item)[0].ToString().Replace("\"", "") + "\n";
			}

			print (result_text);
			result = false;
		}

		return new ReturnObject (result, result_text);

	}
		
}
