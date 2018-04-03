using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Timestamp the end date and go to initial input screen after a period of time
public class WinScreenProcessing : MonoBehaviour {

    public GameObject progressBar;
    Slider slider;

    float startingWaitTime = 10.0f;
    float waitTime;

	// Use this for initialization
	void Start () {

        waitTime = startingWaitTime;

        slider = progressBar.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {

        // Update progressBar's value based on waitTime
        slider.value = (startingWaitTime - waitTime) / startingWaitTime;

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
