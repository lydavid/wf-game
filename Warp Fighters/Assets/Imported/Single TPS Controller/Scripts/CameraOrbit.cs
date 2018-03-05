
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
        float mouseVertical = 0;
        if (Input.GetAxis("Mouse Y") != 0)
        {
            mouseVertical = Input.GetAxis("Mouse Y");
        }
        if (Input.GetAxis("Right Stick Y") != 0)
        {
            mouseVertical = Input.GetAxis("Right Stick Y");
        }

        vertical = (vertical - turnSpeed * mouseVertical) % 360f;
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }
}
