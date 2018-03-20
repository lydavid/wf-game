﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script starts tracking time after given a start signal, until given a stop signal
// Provides functionality for a Best Time score system in our game
public class TrackTime : MonoBehaviour {

    public bool trackTime; // counts while this is true, until it is false
    public float timeInSeconds;
    int minutes;
    int seconds;
    int milliseconds;

    string addZeroForMin;
    string addZeroForSec;
    string addZeroForMS;

    Text displayTimeText;

	// Use this for initialization
	void Start () {
        timeInSeconds = 0.0f;
        displayTimeText = GameObject.Find("TimeDisplay").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if (trackTime)
        {
            timeInSeconds += Time.deltaTime;
        }
        DisplayTime();
	}

    // Display the current time on UI
    private void DisplayTime()
    {
        minutes = (int) (timeInSeconds / 60);
        seconds = (int) (timeInSeconds % 60);
        milliseconds = (int)(timeInSeconds * 100 % 100);

        addZeroForMin = "";
        addZeroForMin = "";
        addZeroForMS = "";
        if (minutes < 10)
        {
            addZeroForMin = "0";
        }
        if (seconds < 10)
        {
            addZeroForSec = "0";
        }
        if (milliseconds < 10)
        {
            addZeroForMS = "0";
        }

        displayTimeText.text = addZeroForMin + minutes.ToString() + "′" + addZeroForSec + seconds.ToString() + "′′"
            + addZeroForMS + milliseconds.ToString();
    }


    // Toggles whether to start/stop tracking time
    // if given true param, set to true -> used in ForcedInstructions
    public void ToggleTrackTime(bool setTo = false)
    {
        if (!setTo)
        {
            if (trackTime)
            {
                trackTime = false;
            }
            else
            {
                trackTime = true;
            }
        } else
        {
            trackTime = true;
        }
    }
}
