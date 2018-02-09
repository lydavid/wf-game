using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotControl : MonoBehaviour {

	private float vertical;
	private float turnSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		vertical = transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		var mouseY = Input.GetAxis("Mouse Y");
		vertical = (vertical - turnSpeed * mouseY) % 360f;
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);	
	}

}
