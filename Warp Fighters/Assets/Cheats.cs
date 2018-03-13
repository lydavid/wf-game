using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    HPManager HPManager;
    WarpLimiter warpLimiter;

	// Use this for initialization
	void Start () {
        HPManager = gameObject.GetComponent<HPManager>();
        warpLimiter = gameObject.GetComponent<WarpLimiter>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("9"))
        {
            HPManager.healthPoints += 1;
        }
        

        if (Input.GetKeyDown("0"))
        {
            warpLimiter.maxWarpCharges += 1;
            warpLimiter.warpCharges += 1;
        }
    }
}
