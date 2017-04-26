using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	int stageHeight, stageWidth;
	bool genStage = false;
	int frameCount;
	bool test = false;

	void Update () {
		/*if (genStage) {
			SceneManager.LoadScene("Ana'esNEWLevel");
			print ("scene gen");
			StageGeneratorScript stageGen = FindObjectOfType (typeof(StageGeneratorScript)) as StageGeneratorScript;
			stageGen.stageHeight = stageHeight;
			stageGen.stageWidth = stageWidth;
		}*/
		/*print (test);
		if (genStage && Time.frameCount == frameCount + 100) {
			print ("set attr");
			StageGeneratorScript stageGen = FindObjectOfType (typeof(StageGeneratorScript)) as StageGeneratorScript;
			stageGen.stageHeight = stageHeight;
			stageGen.stageWidth = stageWidth;
			genStage = false;
		}*/
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Stage") 
		{
			//print ("Door collided with stage");
			DestroyImmediate (other.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {

			print ("inside the trigger method eyyyyyyylmaaooooo booiiiiii");

			other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
			other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY;
			other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			other.GetComponent<Rigidbody2D> ().gravityScale = 0;
			other.GetComponent<PlayerC> ().enabled = false;
			other.GetComponent<PlayerAttack> ().enabled = false;

			//print ("Player should have froze");

			SwitchScenes.GENSTAGE = true;
			SwitchScenes.frameCount = Time.frameCount;

			/*Player player = FindObjectOfType (typeof(Player)) as Player;
			stageHeight = player.nextLevel + 10;
			stageWidth = player.nextLevel + 10;
			print ("Width " + stageWidth + " Hight " + stageHeight);

			SceneManager.LoadSceneAsync("Ana'esNEWLevel");
			//StageGeneratorScript stageGen = FindObjectOfType (typeof(StageGeneratorScript)) as StageGeneratorScript;
			//stageGen.stageHeight = stageHeight;
			//stageGen.stageWidth = stageWidth;
			genStage = true;
			test = true;
			frameCount = Time.frameCount;
			print (frameCount);*/

		}

	}

}
