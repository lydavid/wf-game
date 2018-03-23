using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedInstructions : MonoBehaviour {

    //public GameObject smallerInstructions;

    GameObject player;

    bool moved;
    bool looked;
    bool warped;
    bool locked;

    TrackTime trackTime;
    TPSPlayerController TPSPlayerController;

	// Use this for initialization
	void Start () {
        //smallerInstructions = GameObject.FindGameObjectWithTag("Instructions");
        player = GameObject.FindGameObjectWithTag("Player");
        trackTime = player.GetComponent<TrackTime>();
        TPSPlayerController = player.GetComponent<TPSPlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        // once player has moved and looked around, change big-ass instructions into smaller one
        if (InputManager.MoveX() != 0 || InputManager.MoveY() != 0)
        {
            moved = true;
            trackTime.ToggleTrackTime(true);
        }

        if (InputManager.LookX() > 0 || InputManager.LookY() > 0)
        {
            looked = true;
            trackTime.ToggleTrackTime(true);
        }

        if (InputManager.WarpButton(TPSPlayerController.controllerType))
        {
            warped = true;
            trackTime.ToggleTrackTime(true);
        }

        if (InputManager.LockOnButton(TPSPlayerController.controllerType))
        {
            locked = true;
            trackTime.ToggleTrackTime(true);
        }

        if (moved && looked && warped && locked)
        {
            gameObject.SetActive(false);
        }
	}
}
