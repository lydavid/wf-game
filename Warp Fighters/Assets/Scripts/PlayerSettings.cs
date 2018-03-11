using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A bunch of public variables that other scripts will access to determine how things should be handled based on what we want
// allows for some customization
public class PlayerSettings : MonoBehaviour {

    public bool humanBulletOn;

	// Use this for initialization
	void Start () {
        humanBulletOn = true;
	}
	
	// Update is called once per frame
	void Update () {

        // Toggles human bullet mode for velocity warp
        /* 
		if (Input.GetKeyDown("."))
        {
            if (!humanBulletOn)
            {
                humanBulletOn = true;
            } else
            {
                humanBulletOn = false;
            }
        }*/
	}
}
