using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float yaw;
	private float pitch;
	private float roll;
	private int flappiness;
	private bool flappable = true;

	public int flapcap = 100;
	public float gravity = 20;
	public float flapower = 10;
	public float steer = 2;
	public float speed = 1;
	public GameObject player;
	public Rigidbody p_physics;
	public float lifty = 20;


	// Use this for initialization
	void Start () {
		player.transform.forward = Vector3.forward;
		flappiness = flapcap;
	}

	// Update is called once per frame
	void FixedUpdate () {
		jets();
		//grav();
		//glide ();
		//lift ();
		//flap();
		Debug.Log (flappiness.ToString());
	}

	void Update (){
		steering ();
	}
	void steering() { // Maintaining forward momentum TODO
		yaw = Input.GetAxis ("Horizontal");
		pitch = Input.GetAxis ("Vertical");
		roll = Input.GetKey ("q")?1:0;
		roll = roll - (Input.GetKey ("e")?1:0);

		player.transform.RotateAround (player.transform.position, player.transform.up, yaw * steer);
		player.transform.RotateAround (player.transform.position, player.transform.right, pitch * steer);
		player.transform.RotateAround (player.transform.position, player.transform.forward, roll * steer);
	}

	void glide() {
		if (Vector3.Dot(p_physics.velocity, -player.transform.up) != 0) { //Change to player down velocity TODO
			p_physics.AddForce (player.transform.forward * speed * (-p_physics.velocity.y));
		}
	}

	void jets () {
		p_physics.velocity = player.transform.forward*speed ;
	}

	void lift () { // if the player is moving forward add an up force
		if (Vector3.Dot(p_physics.velocity, player.transform.forward) > 0) {
			p_physics.AddForce (player.transform.up * lifty);
		}
	}

	void flap () {
		if (flappable == true) {
			if (Input.GetKey ("p")) {
				if (flappiness > 0) {
					p_physics.AddForce (flapower*player.transform.forward + flapower*player.transform.up);
					flappiness = flappiness -1;
				} else {
					flappable = false;
				}
			} else {
				if (flappiness < flapcap) {
					flappiness = flappiness + 1;
				}
			}
		} else {
			if (flappiness < flapcap) {
				flappiness = flappiness + 1;
			} else {
				flappable = true;
			}
		}
	}

	void grav () {
		p_physics.AddForce (Vector3.down*gravity);
	}
}

