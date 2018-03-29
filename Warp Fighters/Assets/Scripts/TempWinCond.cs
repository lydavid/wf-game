using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempWinCond : MonoBehaviour {

    public GameObject bossEnemy;
    TrackTime trackTime;

    bool cannotWin;

	// Use this for initialization
	void Start () {
        trackTime = GetComponent<TrackTime>();
        /*if (GameObject.Find("RiggedEnemy (Boss)"))
        {
            bossEnemy = GameObject.Find("RiggedEnemy (Boss)");
        } else*/
        if (!bossEnemy) // if a boss is not assigned, we cannot win, this prevents instant winning
        {
            cannotWin = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (!cannotWin)
        {
            if (bossEnemy == null)
            {
                // Remember player's time before going to next scene
                PlayerPrefs.SetFloat(Constants.SCORE_KEY, trackTime.GetTime());
                SceneManager.LoadScene("Win");
            }
        }
		
	}
}
