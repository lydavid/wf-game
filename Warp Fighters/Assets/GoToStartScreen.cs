using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStartScreen : MonoBehaviour {

    GameObject BGM;

	// Use this for initialization
	void Start () {

        // try to find any audio and destroy them before returning to start
        BGM = GameObject.Find("Audio");
	}
	
	// Update is called once per frame
	void Update () {
        if (InputManager.MenuButton())

            if (BGM)
            {
                Destroy(BGM);
            }
            SceneManager.LoadScene("StartScreen");
        }
	}
}
