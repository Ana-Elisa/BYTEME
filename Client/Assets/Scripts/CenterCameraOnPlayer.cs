using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCameraOnPlayer : MonoBehaviour {

	Transform pTransform = null;
	public int multiplier;
	public float leftBound = 0;
	public float rightBound = 10;
	public float topBound = 0;
	public float bottomBound = -8;
	float camHWidth;
	float camHHeight;

	void Start(){
		Camera ccam = FindObjectOfType<Camera> ();
		camHHeight = ccam.orthographicSize;
		camHWidth = ccam.aspect * camHHeight;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (pTransform == null) {
			GameObject target = GameObject.FindGameObjectWithTag ("Player");
			if (target) {
				pTransform = target.transform;

				Player player = FindObjectOfType (typeof(Player)) as Player;
				multiplier = player.nextLevel + 1;

				LateUpdate ();
			}
		} else {
			float targetX = pTransform.position.x;
			float targetY = pTransform.position.y;
			if (targetX - camHWidth < leftBound) {
				targetX = leftBound + camHWidth;
			}
			if (targetX + camHWidth > rightBound * multiplier) {
				targetX = rightBound * multiplier - camHWidth;
			}
			if (targetY - camHHeight < bottomBound * multiplier) {
				targetY = bottomBound * multiplier + camHHeight;
			}
			if (targetY + camHHeight > topBound) {
				targetY = topBound - camHHeight;
			}
			transform.position = (Vector3.right * targetX) + (Vector3.up * targetY) + (Vector3.back * 10);
		}
	}
}
