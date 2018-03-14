
using UnityEngine;
using System.Collections;

public class CameraVertical : MonoBehaviour {

    LockOn lockOn;

    private float vertical;
    private float turnSpeed = 2.5f;
    void Start ()
    {
        vertical = transform.eulerAngles.x;
        lockOn = transform.parent.gameObject.GetComponent<LockOn>();
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
        if (Input.GetAxis("Right Stick Y") != 0)
        {
            mouseVertical = Input.GetAxis("Right Stick Y");
        }
        //}

        vertical = (vertical - turnSpeed * mouseVertical) % 360f;
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }

}
