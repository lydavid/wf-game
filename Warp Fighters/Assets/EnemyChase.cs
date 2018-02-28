using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles enemy character persuing the player character and attempting to attack them
public class EnemyChase : MonoBehaviour {

    EnemyDetection enemyDetection;

	// Use this for initialization
	void Start () {
        enemyDetection = GetComponent<EnemyDetection>();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyDetection.enemySpotted)
        {

        }
	}
}
