using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempWinCond : MonoBehaviour {

    public GameObject bossEnemy;
    TrackTime trackTime;

	// Use this for initialization
	void Start () {
        trackTime = GetComponent<TrackTime>();
        if (GameObject.Find("RiggedEnemy (Boss)"))
        {
            bossEnemy = GameObject.Find("RiggedEnemy (Boss)");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (bossEnemy == null)
        {
            // Remember player's time before going to next scene
            PlayerPrefs.SetFloat("TimeInSeconds", trackTime.GetTime());
            SceneManager.LoadScene("Win");
        }
		
	}
}
