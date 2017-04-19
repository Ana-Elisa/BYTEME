using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGeneratorScript : MonoBehaviour {

	public Transform[] roomGenerators = new Transform[16];
	public Transform[] items = new Transform[0];
	public Transform[] enemies = new Transform[0];
	public float chanceToDropItem = 0.5f;
	public float chanceToDropEnemy = 0.5f;
	public GameObject prePlayer;
	public Transform player;
	public Transform exit;
	public int stageWidth = 4;
	public int stageHeight = 4;
	public float roomWidth = 10f;
	public float roomHeight = 8f;
	int Up = 1;
	int Right = 2;
	int Down = 4;
	int Left = 8;
	int startX;
	int endX;
	//This must have 16 entries, and must be inserted in a specific way
	//  think of the directions the room opens into like a binary number in the following way:
	//   U - 1
	//   R - 2
	//   D - 4
	//   L - 8
	//  sum the open directions the generator generates, and assign it accordingly.
	//   ex: the UDL generator should be in index 1+4+8 = 13

	// Use this for initialization
	public void GenerateStage () {
		int[,] directions = new int[stageWidth, stageHeight];
		Stack<Vector2> stack = new Stack<Vector2> ();
		HashSet<Vector2> visited = new HashSet<Vector2> ();
		startX = (int)(Random.value * stageWidth);
		//startX = 0;
		endX = (int)(Random.value * stageWidth);
		stack.Push (new Vector2 (startX, 0));
		while (stack.Count > 0) {
			List<int> candid = new List<int> ();
			Vector2 cur = stack.Peek ();
			visited.Add (cur);
			int x = (int)cur.x;
			int y = (int)cur.y;
			if (y > 0 && !visited.Contains (new Vector2 (x, y - 1))) {
				candid.Add (Up);
			}
			if (y < stageHeight - 1 && !visited.Contains (new Vector2 (x, y + 1))) {
				candid.Add (Down);
			}
			if (x > 0 && !visited.Contains (new Vector2 (x - 1, y))) {
				candid.Add (Left);
			}
			if (x < stageHeight - 1 && !visited.Contains (new Vector2 (x + 1, y))) {
				candid.Add (Right);
			}
			if (candid.Count == 0) {
				stack.Pop ();
			} else {
				int[] c = candid.ToArray ();
				int dir = c [(int)(Random.value * candid.Count)];
				directions [x, y] = directions [x, y] | dir;
				int opp = dir >> 2;
				if (opp == 0) {
					opp = dir << 2;
				}
				Vector2 target;
				switch (dir) {
				case 1:
					target = cur + Vector2.down;
					y -= 1;
					break;
				case 2:
					target = cur + Vector2.right;
					x += 1;
					break;
				case 4:
					target = cur + Vector2.up;
					y += 1;
					break;
				case 8:
					target = cur + Vector2.left;
					x -= 1;
					break;
				default:
					target = Vector2.left * 5;
					break;
				}
				if (target.x < -3) {
					stack.Pop ();
				} else {
					directions [x, y] = directions [x, y] | opp;
					stack.Push (target);
				}
			}
		}

		float xoff = 0f;
		float yoff = 0f;
		for (int y = 0; y < stageHeight; y++) {
			for (int x = 0; x < stageWidth; x++) {
				Instantiate (roomGenerators [directions [x, y]], transform.position + Vector3.right * xoff + Vector3.down * yoff, Quaternion.identity);
				if (y == 0 && x == startX) {

					//We are getting the player
					prePlayer = GameObject.FindGameObjectWithTag ("Player");
					//We are moving the player
					//prePlayer.transform.position.Set(Mathf.Abs((prePlayer.transform.position.x + xoff + (roomWidth / 2))), Mathf.Abs((prePlayer.transform.position.y + yoff + (roomHeight / 2))), 0);
					prePlayer.transform.position = (Vector3.right * (xoff + (roomWidth / 2))+ Vector3.down * (yoff + (roomHeight / 2)));
					//Instantiate (player, transform.position + Vector3.right * (xoff + (roomWidth / 2)) + Vector3.down * (yoff + (roomHeight / 2)), Quaternion.identity);
				} else {
					if (y == stageHeight - 1 && x == endX) {
						Instantiate (exit, transform.position + Vector3.right * (xoff + (roomWidth / 2)) + Vector3.down * (yoff + (roomHeight / 2)), Quaternion.identity);
					} else {
						if (items.Length > 0 && Random.value < chanceToDropItem) {
							int choice = (int)(Random.value * items.Length);
							Instantiate (items [choice], transform.position + Vector3.right * (xoff + (roomWidth / 2)) + Vector3.down * (yoff + (roomHeight / 2)), Quaternion.identity);
						}
						if (enemies.Length > 0 && Random.value < chanceToDropEnemy) {
							int choice = (int)(Random.value * items.Length);
							Instantiate (enemies [choice], transform.position + Vector3.right * (xoff + (roomWidth / 2)) + Vector3.down * (yoff + (roomHeight / 2)), Quaternion.identity);
						}
					}
				}
				xoff += roomWidth;
			}
			xoff = 0f;
			yoff += roomHeight;
		}

		Destroy (gameObject);
	}
}
