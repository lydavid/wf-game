using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInstructions : MonoBehaviour {

    GameObject inst;

    bool isEnabled;

	// Use this for initialization
	void Start () {
        inst = GameObject.FindGameObjectsWithTag("Instructions")[0];
        isEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Window Button"))
        {
            if (isEnabled)
            {
                inst.SetActive(false);
                isEnabled = false;
            } else
            {
                inst.SetActive(true);
                isEnabled = true;
            }
        }
	}
}
