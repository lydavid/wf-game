using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        // Window Button because this is temp in our level as well, which has Start for pause
        /*if (Input.GetButtonDown("Window Button") || Input.GetKeyDown("enter") || Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(Constants.SCENE_BETA);
            SceneManager.LoadScene(Constants.SCENE_ALPHA);
        }*/
	}
}
