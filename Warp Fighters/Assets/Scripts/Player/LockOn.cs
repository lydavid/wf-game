﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    List<GameObject> interactables; // list of interactable objects in the scene

    // Use this for initialization
    void Start () {
        targetLockedOn = false;
        camOriginalPos = cam.transform.localPosition;
        camOriginalRot = cam.transform.localRotation;
        bodyOriginalPos = body.transform.localPosition;
        bodyOriginalRot = body.transform.localRotation;

        controller = GetComponent<TPSPlayerController>();

        interactables = new List<GameObject>();
        GameObject[] GOs = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject GO in GOs)
        {
            if (GO.layer == 10)
            {
                interactables.Add(GO);
            }
        }
        
    }

    // returns whether viewpoint coords are considered on screen
    private bool OnScreen(Vector3 screenPoint)
    {
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1 && screenPoint.z > 0;
    }


    private bool CloserToCenter(Vector3 A, Vector3 B)
    {
        float center = 0.5f;
        if (A.x == B.x && A.y == B.y)
        {
            return A.z < B.z; // if both are in the center, return true if A is closer
        }
        else
        {
            A.z = 0;
            B.z = 0;
            Vector3 centerOfScreen = new Vector3(center, center, 0);
            return Vector3.Distance(A, centerOfScreen) < Vector3.Distance(B, centerOfScreen);
        }

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
            )//&& GetComponent<HumanBullet>().target != null)
        {
            targetLockedOn = true;




            target = GetComponent<HumanBullet>().target;

            if (target == null)
            {
                //float closestMagnitude = Mathf.Infinity;
                foreach (GameObject GO in interactables)
                {
                    if (GO != null)
                    {
                        Vector3 viewPoint = cam.WorldToViewportPoint(GO.transform.position);

                        // find the closest one and store it, thus when we try to lock-on, we will lock on to this automatically

                        if (OnScreen(viewPoint))
                        {

                            // Find whether it is actually not behind a wall with raycast
                            Ray ray;
                            RaycastHit hit;
                            LayerMask layerMask = 1 << 10;
                            layerMask |= 1 << 12;
                            layerMask = ~layerMask;
                            //if (!Physics.Linecast(transform.position, GO.transform.position, layerMask))
                            Debug.DrawRay(transform.position, Vector3.Normalize(GO.GetComponent<Center>().GetCenter() - transform.position) * 100, Color.black);
                            if (Physics.Raycast(transform.position, Vector3.Normalize(GO.GetComponent<Center>().GetCenter() - transform.position) *100, out hit))
                            {
                                Debug.Log(hit.transform.root.name + " vs " + GO.transform.root.name);
                                if (hit.transform.root.name != GO.transform.root.name)
                                {
                                    
                                    Debug.Log("blocked");
                                }
                                else
                                {


                                    Debug.Log(GO.name + ": " + OnScreen(viewPoint));
                                    if (target == null)
                                    {
                                        target = GO;
                                    }
                                    else
                                    {
                                        Vector3 targetViewPoint = cam.WorldToViewportPoint(target.transform.position);
                                        float currentMagnitude = Vector3.Distance(targetViewPoint, viewPoint);
                                        if (CloserToCenter(viewPoint, targetViewPoint))
                                        {
                                            //closestMagnitude = currentMagnitude;
                                            target = GO;
                                        }
                                        //if (CloserToCenter(GO.transform.position))
                                    }
                                }
                            }
                        }
                    }
                }
                
            }

            if (target != null)
            {
                //transform.LookAt(target.GetComponent<Center>().center.transform.position);
                Debug.Log(target.name);
                targetCenter = target.GetComponent<Center>().GetCenter();
                cam.transform.LookAt(targetCenter); // rather than lock on to the transform position (often times their feet), lock on to the center of the object
                body.transform.LookAt(targetCenter);
            } else
            {
                targetLockedOn = false;
            }
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
