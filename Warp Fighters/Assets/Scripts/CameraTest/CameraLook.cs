using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

	[SerializeField]
	private GameObject player;
	Vector3 offset;


	// Use this for initialization
	void Start () {
		offset = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {

			
	}

	void LateUpdate() {
    	float angle = player.transform.eulerAngles.y;
    	Quaternion rotation = Quaternion.Euler(0, angle, 0);
		transform.position = player.transform.position - (rotation * offset);
		transform.LookAt(player.transform);
	}
}
