
using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    private float vertical;
    private float turnSpeed = 4.0f;
    void Start ()
    {
        vertical = transform.eulerAngles.x;
    }
	
	void Update ()
    {
        //var mouseVertical = Input.GetAxis("Mouse Y");
        var mouseVertical = Input.GetAxis("Left Stick Y");
        vertical = (vertical - turnSpeed * mouseVertical) % 360f;
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }
}
