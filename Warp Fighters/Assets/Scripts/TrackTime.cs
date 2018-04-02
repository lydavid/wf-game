using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script starts tracking time after given a start signal, until given a stop signal
// Provides functionality for a Best Time score system in our game
public class TrackTime : MonoBehaviour {

    private bool trackTime; // counts while this is true, until it is false
    private float timeInSeconds;

    Text displayTimeText;


	// Use this for initialization
	void Start () {
        timeInSeconds = 0.0f;
        displayTimeText = GameObject.Find("TimeDisplay").GetComponent<Text>();

        // Reset this var to 0 at start of game, since it persists through game sessions
        PlayerPrefs.SetFloat("TimeInSeconds", 0.0f);
        trackTime = true;
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
        displayTimeText.text = StringHelpers.TimeInSecondsToFormattedString(timeInSeconds);
    }

    public float GetTime ()
    {
        return timeInSeconds;
    }


    // used in cheats only
    public void AddToTime(int seconds)
    {
        timeInSeconds += seconds;
    }


    // Toggles whether to start/stop tracking time
    // if given true param, set to true -> used in ForcedInstructions
    public void SetTrackTime(bool setTo)
    {
        trackTime = setTo;
        /*if (!setTo)
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
        }*/
    }
}
