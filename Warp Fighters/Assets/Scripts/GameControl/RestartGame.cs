using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

	//public Button restartBtn;

	// Use this for initialization
	void Start () {
		//restartBtn.onClick.AddListener(TaskOnClick);
    }

	
	// Update is called once per frame
	void Update () {

        // Restart game
		if (InputManager.WindowButton())
        {
            SceneManager.LoadScene(Constants.SCENE_LOADING);  // loading screen will determine which scene to load
        }

        // Return to Start
        if (InputManager.MenuButton())
        {
            SceneManager.LoadScene(Constants.SCENE_START);
        }
	}
}
