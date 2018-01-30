using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float yaw;
	private float strafe;
	private float walk;
	private float energy;
	private bool warpable;

	public float steer;
	public float speed;
	public float warp;
	public GameObject player;
	public Rigidbody p_physics;



	// Use this for initialization
	void Start () {
		warpable = true;
		energy = 20;
		player.transform.forward = Vector3.forward;

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (energy == 20) {
			warpable = true;
		}
		if (energy == 0) {
			warpable = false;
		}
		Debug.Log (energy.ToString());
		steering ();
	}

		
	void steering() {
		strafe = Input.GetAxis ("Horizontal");
		walk = Input.GetAxis ("Vertical");
		yaw = Input.GetKey ("q")?1:0;
		yaw = yaw - (Input.GetKey ("e")?1:0);

		player.transform.RotateAround (player.transform.position, player.transform.up, yaw * steer);
		p_physics.velocity = player.transform.forward * walk * speed;
		p_physics.velocity = p_physics.velocity + player.transform.right * strafe * speed;

		if (Input.GetKey ("v")) {
			p_physics.velocity = player.transform.forward * warp; 
		}

		if (energy == 20) {
			warpable = true;
		}
		if (energy == 0) {
			warpable = false;
		}

		if (Input.GetKey ("r") && warpable) {
			energy = energy - 1;
			p_physics.velocity = Vector3.zero;
			p_physics.freezeRotation = true;
		}

		if (Input.GetKeyUp("v")) {
			warpable = false;
		}


	}
		
}

