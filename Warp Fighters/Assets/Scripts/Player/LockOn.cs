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
    Vector3 bodyOriginalPos;
    Quaternion bodyOriginalRot;

    public Vector3 targetCenter;

    TPSPlayerController controller;

    GameObject[] interactables; // list of interactable objects in the scene

    // Use this for initialization
    void Start () {
        targetLockedOn = false;
        camOriginalPos = cam.transform.localPosition;
        camOriginalRot = cam.transform.localRotation;
        bodyOriginalPos = body.transform.localPosition;
        bodyOriginalRot = body.transform.localRotation;

        controller = GetComponent<TPSPlayerController>();

        
    }
	
	// Update is called once per frame
	void Update () {

        
		
	}

    private void LateUpdate()
    {
        //Debug.Log(Input.GetAxis("Right Trigger"));
        if (/*(Input.GetAxis("Right Trigger") > 0 && controller.controllerType == ControllerType.xbox
            || Input.GetMouseButton(1)
            || Input.GetAxis("R2") > 0 && controller.controllerType == ControllerType.ps)*/
            InputManager.LockOnButton(controller.controllerType)
            && GetComponent<HumanBullet>().target != null)
        {
            targetLockedOn = true;
            target = GetComponent<HumanBullet>().target;
            //transform.LookAt(target.GetComponent<Center>().center.transform.position);
            targetCenter = target.GetComponent<Center>().GetCenter();
            cam.transform.LookAt(targetCenter); // rather than lock on to the transform position (often times their feet), lock on to the center of the object
            body.transform.LookAt(targetCenter);
        }
        else
        {
            targetLockedOn = false;
            GetComponent<HumanBullet>().target = null;
            cam.transform.localPosition = camOriginalPos;
            cam.transform.localRotation = camOriginalRot;
            body.transform.localPosition = bodyOriginalPos;
            body.transform.localRotation = bodyOriginalRot;
        }

    }
}
