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
        SceneManager.LoadScene("JonLevel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
