using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayReticle : MonoBehaviour {

    public GameObject reticle;

    PlayerSettings playerSettings;


	// Use this for initialization
	void Start () {
        playerSettings = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSettings>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerSettings.humanBulletOn)
        {
            reticle.SetActive(true);
        } else
        {
            reticle.SetActive(false);
        }
	}
}
