using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

	public GameObject player;
	public Rigidbody playerRB;

	public int distance;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerRB = player.GetComponent<Rigidbody>();
		distance = 700;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.tag == "Player")
		{	
			Vector3 curVelocity = playerRB.velocity;
			playerRB.velocity = Vector3.zero;
			playerRB.AddForce((transform.up + (0.03f * curVelocity)) * distance );
		}
	}
}
