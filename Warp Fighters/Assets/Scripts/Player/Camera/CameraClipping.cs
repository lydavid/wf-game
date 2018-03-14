using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClipping : MonoBehaviour {

	private Vector3 minPosition;
	private Vector3 maxPosition; // position where camera is usuallly e.g when game starts
	private GameObject player;
	private bool inCollision = false;
	//private bool movedPos = false;

	private Vector3 prevPos;


	private Rigidbody rb;

	// Use this for initialization
	void Start () 
	{	
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
		maxPosition = transform.localPosition;
		prevPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ( "player moved: " + DidPlayerMove() + " collision: "+ inCollision );
		MoveCameraBack ();
	}


	bool DidPlayerMove () 
	{
		Vector3 distanceMoved = player.transform.position - prevPos;
		if (distanceMoved.magnitude > 0.01f) 
		{	
			prevPos = player.transform.position;
			return true;
		} 
		else
		{
			return false;
		}
	}
	
	void OnCollisionStay (Collision hit) 
	{
		inCollision = true;
		transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, 0.08f);
	}

	void OnCollisionExit () 
	{
		inCollision = false;
	}
		
	void MoveCameraBack () 
	{
		if (inCollision == false && DidPlayerMove() == true) 
		{
			//Debug.Log("YOOOOOO");
			transform.localPosition = Vector3.Lerp(transform.localPosition, maxPosition, 0.08f);
		}
	}
}
