﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

	public GameObject player;
	public Rigidbody playerRB;

	public int distance;

    int strength = 2;

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
			//Vector3 curVelocity = playerRB.velocity;
            //Debug.Log(playerRB.velocity.magnitude);
            //float magnitude = playerRB.velocity.magnitude;
            //playerRB.velocity = Vector3.zero;
            //playerRB.angularVelocity = Vector3.zero;
            //playerRB.AddForce((transform.up + (0.03f * curVelocity)) * distance );

            //playerRB.velocity = new Vector3(0, Mathf.Sqrt(Mathf.Pow(playerRB.velocity.x, 2) + Mathf.Pow(playerRB.velocity.z, 2)) * strength, 0);
            playerRB.velocity = new Vector3(0, playerRB.velocity.magnitude * strength, 0);
        }
	}
}
