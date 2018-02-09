using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotX : MonoBehaviour {

	private float horizontal;
	private float turnSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		horizontal = transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		var mouseHorizontal = Input.GetAxis("Mouse X");
        horizontal = (horizontal + turnSpeed * mouseHorizontal) % 360f;
        transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);
	}
}
