using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEnterInitials : MonoBehaviour {

    float waitTime = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            SceneManager.LoadScene("Highscore");
        }
	}
}
