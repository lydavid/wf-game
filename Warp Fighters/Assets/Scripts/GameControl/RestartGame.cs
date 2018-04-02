using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

	public Button restartBtn;

	// Use this for initialization
	void Start () {
		restartBtn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        if (PlayerPrefs.GetInt(Constants.SCENE_TO_LOAD) == 0)
        {
            SceneManager.LoadScene("Beta 0.3");
        }
        else if (PlayerPrefs.GetInt(Constants.SCENE_TO_LOAD) == 1)
        {
            SceneManager.LoadScene("Alpha3.0");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
