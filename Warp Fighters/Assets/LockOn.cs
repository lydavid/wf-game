using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour {

    public bool targetLockedOn; // let's other scripts know that the player has a target locked on
    public GameObject target;

    public Camera cam;
    Vector3 camOriginalPos;
    Quaternion camOriginalRot;

    public GameObject body;

	// Use this for initialization
	void Start () {
        targetLockedOn = false;
        camOriginalPos = cam.transform.localPosition;
        camOriginalRot = cam.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {

        
		
	}

    private void LateUpdate()
    {
        if (Input.GetButton("Right Trigger") || Input.GetMouseButton(1))
        {
            Debug.Log("Yee boi");
            targetLockedOn = true;
            target = GetComponent<HumanBullet>().target;
            //transform.LookAt(target.GetComponent<Center>().center.transform.position);
            cam.transform.LookAt(target.GetComponent<Center>().center.transform.position); // rather than lock on to the transform position (often times their feet), lock on to the center of the object
            //body.transform.LookAt(target.GetComponent<Center>().center.transform.position);
        }
        else
        {
            targetLockedOn = false;
            cam.transform.localPosition = camOriginalPos;
            cam.transform.localRotation = camOriginalRot;
        }

    }
}
