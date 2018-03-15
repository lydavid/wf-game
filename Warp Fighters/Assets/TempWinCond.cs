using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempWinCond : MonoBehaviour {

    public GameObject bossEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (bossEnemy == null)
        {
            SceneManager.LoadScene("Win");
        }
		
	}
}
