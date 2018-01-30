using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player;
	public float tracking =1;
	private float rot_x;
	private float rot_y;
	private float rot_z;
	private float distance;
	private float yaw, pitch, roll;

	// Use this for initialization
	void Start () {
		distance = Vector3.Distance(transform.position, player.transform.position);
	}

	void Update() {

	}
	
	// Update is called once per frame

	void LateUpdate () {
		transform.position = player.transform.position + ((-distance)*transform.forward) ; //update position
		cameraRotate ();

	}
	void cameraRotate (){
		if (Input.GetKey ("f")) {
			rot_z = 0;
			rot_x = Input.GetAxis ("Mouse X");
			rot_y = Input.GetAxis ("Mouse Y");
			rot_z = Input.GetMouseButton (0) ? 1 : 0;
			rot_z = rot_z - (Input.GetMouseButton (1) ? 1 : 0);
			transform.RotateAround (player.transform.position, transform.up, (8 / 3) * rot_x * tracking);
			transform.RotateAround (player.transform.position, transform.right, (8 / 3) * rot_y * tracking);
			transform.RotateAround (player.transform.position, transform.forward, (8 / 3) * rot_z * tracking);

			distance = distance + Input.mouseScrollDelta.y;
		}
	}
	void cameraFollow() {

			yaw = Input.GetAxis ("Horizontal");
			pitch = Input.GetAxis ("Vertical");
			roll = Input.GetKey ("q")?1:0;
			roll = roll - (Input.GetKey ("e")?1:0);

			transform.RotateAround (player.transform.position, transform.up, (2/3) * yaw);
			transform.RotateAround (player.transform.position, transform.right, (2/3) * pitch);
			transform.RotateAround (player.transform.position, transform.forward, (2/3) * roll);
	}
}

