using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedInstructions : MonoBehaviour {

    //public GameObject smallerInstructions;

    bool moved;
    bool looked;
    bool warped;
    bool locked;

    TrackTime trackTime;

	// Use this for initialization
	void Start () {
        //smallerInstructions = GameObject.FindGameObjectWithTag("Instructions");
        trackTime = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackTime>();
	}
	
	// Update is called once per frame
	void Update () {
        // once player has moved and looked around, change big-ass instructions into smaller one
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            moved = true;
            trackTime.ToggleTrackTime(true);
        }

        if (Input.GetAxis("Right Stick X") != 0 
            || Input.GetAxis("Right Stick Y") != 0 
            || Input.GetAxis("Mouse X") != 0 
            || Input.GetAxis("Mouse Y") != 0)
        {
            looked = true;
            trackTime.ToggleTrackTime(true);
        }

        if (Input.GetButtonDown("A Button") 
            || Input.GetMouseButtonDown(0)
            || Input.GetButton("X Button"))
        {
            warped = true;
            trackTime.ToggleTrackTime(true);
        }

        if (Input.GetAxis("Right Trigger") > 0 
            || Input.GetMouseButtonDown(1)
            || Input.GetAxis("R2 (PS4)") != 0)
        {
            locked = true;
            trackTime.ToggleTrackTime(true);
        }

        if (moved && looked && warped && locked)
        {
            //smallerInstructions.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
