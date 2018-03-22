using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A bunch of public variables that other scripts will access to determine how things should be handled based on what we want
// allows for some customization
public class PlayerSettings : MonoBehaviour {

    public bool humanBulletOn;
    

   // TrackTime time;
   // public float elapsedTime;
    public static int enemiesKilled;
    public static int warpCount;
    

	// Use this for initialization
	void Start ()
    {
        humanBulletOn = true;
        enemiesKilled = 0;
        warpCount = 0;
	}
	
    void Update ()
    {
       // elapsedTime = time.GetTime();
    }

}
