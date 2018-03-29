using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempWinCond : MonoBehaviour {

    public GameObject bossEnemy;
    TrackTime trackTime;
    HumanBullet humanBullet;
    WarpLimiter warpLimiter;

    bool cannotWin;

	// Use this for initialization
	void Start () {
        trackTime = GetComponent<TrackTime>();
        humanBullet = GetComponent<HumanBullet>();
        warpLimiter = GetComponent<WarpLimiter>();
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
                Win();
            }
        }
		
	}

    public void Win()
    {
        // Remember player's time before going to next scene
        // And the rest of their info
        PlayerPrefs.SetFloat(Constants.SCORE_KEY, trackTime.GetTime());
        PlayerPrefs.SetString(Constants.DATE_KEY, System.DateTime.Now.ToString());
        PlayerPrefs.SetInt(Constants.WARPS_KEY, humanBullet.warpCount);
        PlayerPrefs.SetInt(Constants.KILLS_KEY, warpLimiter.kills);

        SceneManager.LoadScene("Win");
    }
}
