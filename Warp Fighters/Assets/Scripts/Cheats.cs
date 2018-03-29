using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour {

    HPManager HPManager;
    WarpLimiter warpLimiter;

    int point;

	// Use this for initialization
	void Start () {
        HPManager = gameObject.GetComponent<HPManager>();
        warpLimiter = gameObject.GetComponent<WarpLimiter>();
	}
	
	// Update is called once per frame
	void Update () {
        
        // Determine whether to gain or lose a point in whatever
        if (Input.GetKey("left shift"))
        {
            point = -1;
        } else
        {
            point = 1;
        }

        if (Input.GetKeyDown("1"))
        {
            HPManager.Damage(-point);
        }
        

        if (Input.GetKeyDown("2"))
        {
            warpLimiter.GainMaxChargeAndRefill(point);
        }

        if (Input.GetKeyDown("3"))
        {
            // win game
            SceneManager.LoadScene("Win");
        }
    }
}
