
using UnityEngine;
using System.Collections;

public class CameraVertical : MonoBehaviour {

    LockOn lockOn;

    private float vertical;
    private float turnSpeed = 2.5f;

    public TPSPlayerController controller;

    void Start ()
    {
        vertical = transform.eulerAngles.x;
        lockOn = transform.parent.gameObject.GetComponent<LockOn>();
        controller = transform.parent.gameObject.GetComponent<TPSPlayerController>();
    }
	
	void Update ()
    {
        float mouseVertical = 0;

        // unlike for hori camera, this is already locked when locking on
        //if (!lockOn.targetLockedOn)
        if (Input.GetAxis("Mouse Y") != 0)
        {
            mouseVertical = Input.GetAxis("Mouse Y");
        }
        if (Input.GetAxis("Right Stick Y") != 0 && controller.controllerType == ControllerType.xbox)
        {
            mouseVertical = Input.GetAxis("Right Stick Y");
        }
        if (Input.GetAxis("Right Stick Y (PS4)") != 0 && controller.controllerType == ControllerType.ps)
        {
            mouseVertical = Input.GetAxis("Right Stick Y (PS4)");
        }
        //}

        vertical = (vertical - turnSpeed * mouseVertical) % 360f;
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }

}
