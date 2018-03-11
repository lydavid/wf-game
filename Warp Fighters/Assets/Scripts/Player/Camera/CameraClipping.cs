using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClipping : MonoBehaviour {

	private Vector3 minPosition;
	private Vector3 maxPosition; // position where camera is usuallly e.g when game starts
	private GameObject player;
	private bool inCollision = false;


	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		maxPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCameraBack();

	}
	
	
	void OnCollisionStay (Collision hit) {
		inCollision = true;
		transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, 0.04f);
	}

	void OnCollisionExit () {
		inCollision = false;
	}
		
	void MoveCameraBack () {
		if (!inCollision) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, maxPosition, 0.02f);
		}
	}
}
