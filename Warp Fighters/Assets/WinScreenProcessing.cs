using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Timestamp the end date and go to initial input screen after a period of time
public class WinScreenProcessing : MonoBehaviour {

    float waitTime = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // goes to next scene after a delay (when victory tune finishes)
        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            SceneManager.LoadScene("Highscore");
        }


        // manually go to next scene
        if (InputManager.MenuButton())
        {
            SceneManager.LoadScene("Highscore");
        }
	}
}
