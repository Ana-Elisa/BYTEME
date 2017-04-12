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

    public ReturnObject postSave()
    {
        string url = "https://byteme.online/api/save/";
        bool result = false;
        string result_text = "";

        /*WWWForm userInfo = new WWWForm();
        userInfo.AddField("item_list", items);
        userInfo.AddField("attack", 5);
        userInfo.AddField("defence", 6);
        userInfo.AddField("speed", 10);
        userInfo.AddField("health", 80);
        userInfo.AddField("total_health", 100);
        userInfo.AddField("next_level", 2);
        userInfo.AddField("time", "1:04:00");*/

        /*List<int> items = new List<int>();
        items.Add(1);
        items.Add(2);

        JSONObject userInfo = new JSONObject();
        //userInfo.AddField("item_list", "[1, 2]");
        userInfo.AddField("attack", 5);
        userInfo.AddField("defence", 6);
        userInfo.AddField("speed", 10);
        userInfo.AddField("health", 80);
        userInfo.AddField("total_health", 100);
        userInfo.AddField("next_level", 2);
        userInfo.AddField("time", "1:04:00");*/

        string shit = "{\"attack\" : 100, \"item_list\" : [1, 2] }";
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(shit);
        UploadHandlerRaw uh = new UploadHandlerRaw(bytes);

        //print(userInfo.ToString());
        UnityWebRequest request = UnityWebRequest.Post(url, shit);
        request.SetRequestHeader ("Authorization", "Token eee37ef9408de55176db375bedcf63e3f24c50f6");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = uh;



        request.Send();

        int limit = 3000;
        int counter = 0;
        while ((!request.isDone || !request.downloadHandler.isDone) && counter != limit)
        {
            //Keep looping until the request finishes or errors
            System.Threading.Thread.Sleep(1);
            counter++;
        }

        print(limit + " " + counter);
        if (request.isError || counter == limit)
        {
            return new ReturnObject(false, "Could not connect to server");
        }

        if (request.responseCode == 200)
        {
            result = true;
            print("Successfull!");
        }
        else
        {
            print(request.responseCode);
            print(request.downloadHandler.text);
            JSONObject obj = new JSONObject(request.downloadHandler.text);
            List<string> keyList = obj.keys;

            foreach (string item in keyList)
            {
                result_text += item;
                result_text += " - " + obj.GetField(item).ToString().Replace("\"", "") + "\n";
            }

            print(result_text);
            result = false;
        }

        return new ReturnObject(result, result_text);

    }

	public ReturnObject getSave()
	{
		string url = "https://byteme.online/api/save/";
		bool result = false;
		string result_text = "";

		UnityWebRequest request = UnityWebRequest.Get(url);
		request.SetRequestHeader ("Authorization", "Token eee37ef9408de55176db375bedcf63e3f24c50f6");
		request.SetRequestHeader("Content-Type", "application/json");


		request.Send();

		int limit = 3000;
		int counter = 0;
		while ((!request.isDone || !request.downloadHandler.isDone) && counter != limit)
		{
			//Keep looping until the request finishes or errors
			System.Threading.Thread.Sleep(1);
			counter++;
		}

		print(limit + " " + counter);
		if (request.isError || counter == limit)
		{
			return new ReturnObject(false, "Could not connect to server");
		}

		if (request.responseCode == 200)
		{
			print(request.responseCode);
			print(request.downloadHandler.text);
			JSONObject obj2 = new JSONObject(request.downloadHandler.text);
			string obj3 = obj2.GetField ("results")[0].ToString ();
			print (obj3.ToString());
			JSONObject obj = new JSONObject(obj3.ToString());

			var items = obj.GetField ("item_list").ToDictionary();
			print (items);

			//Michael this is causing an error 
			/*foreach (string item in items) 
			{
				print (item);
			}*/
		}
		else
		{
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

}
