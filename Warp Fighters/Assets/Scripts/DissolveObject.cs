﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveObject : MonoBehaviour {

    Material mat;
    float delayTime;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        delayTime = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (delayTime <= 0)
        {
            mat.SetFloat("_SliceAmount", mat.GetFloat("_SliceAmount") + 0.02f);
            mat.SetFloat("_BurnSize", mat.GetFloat("_BurnSize") + 0.05f);
        } else
        {
            delayTime -= Time.deltaTime;
        }
	}
}
