using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClipping : MonoBehaviour {

	private Vector3 minPosition;
	private Vector3 maxPosition;

	private GameObject camOrbit;
	private GameObject player;
	private float startTime;
    private float distance;
	private float speed = 1.0f;

	// Use this for initialization
	void Start () {
		
		camOrbit = GameObject.Find("CameraOrbitX");
		player = GameObject.FindGameObjectWithTag("Player");
		maxPosition = camOrbit.transform.position;
		minPosition = player.transform.position;
		startTime = Time.time;
		distance = Vector3.Distance(minPosition, maxPosition);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/* 
	void OnCollisionEnter (Collision hit) {
		Debug.Log("Cam Hit");
	}*/

	void OnTriggerEnter (Collider other) {
		Debug.Log("Cam Hit");
		float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / distance;
        transform.position = Vector3.Lerp(minPosition, maxPosition, fracJourney);
	}
}
