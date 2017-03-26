using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	GameObject target;
	Vector3 offset;

	void Start()
	{
		target = GameObject.Find ("cat");
		offset = transform.position - target.transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	void LateUpdate()
	{
		transform.position = target.transform.position + offset;
	}
}
