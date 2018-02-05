using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

	public GameObject player;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		RaycastHit hit;
		//Vector3 up = transform.TransformDirection(Vector3.up) * 100;
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
		Vector3 start = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

		/* 
		Vector3 right = Quaternion.Euler(0, 30, 0) * forward;
		Vector3 left = Quaternion.Euler(0, -30, 0) * forward;
        Debug.DrawRay(start, forward, Color.green);
		Debug.DrawRay(start, left, Color.green);
		Debug.DrawRay(start, right, Color.green);*/

		
		for (float i = -30; i <= 30; i+=0.5f) {
			Vector3 dir = Quaternion.Euler(0, i, 0) * forward;
			Debug.DrawRay(start, dir, Color.red);
		}

		for (float i = -30; i <= 30; i+=0.5f) {
			Vector3 dir = Quaternion.Euler(0, i, 0) * forward;
			if (Physics.Raycast(start, dir, out hit)){
				
				if (hit.rigidbody == rb){
					Debug.Log("Player hit!");
					break;
				}
			}
		}

	}
}
