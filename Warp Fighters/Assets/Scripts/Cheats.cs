using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        

        if (Input.GetKey("left shift"))
        {
            point = -1;
        } else
        {
            point = 1;
        }

        if (Input.GetKeyDown("9"))
        {
            HPManager.Damage(-point);
        }
        

        if (Input.GetKeyDown("0"))
        {
            warpLimiter.maxWarpCharges += point;
            warpLimiter.warpCharges += point;
        }
    }
}
