﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour {

	[SerializeField]
	private GameObject pausePanel;

	private GameObject cameraOrbitX;

	private GameObject player;

    MonoBehaviour[] playerScripts;
	AudioSource[] playerAudio;

    bool isPaused;
    public EventSystem eventSystem;

	// Use this for initialization
	void Start () {
		cameraOrbitX = GameObject.Find("CameraOrbitX");
		player = GameObject.FindGameObjectWithTag("Player");
		playerScripts = player.GetComponents<MonoBehaviour>();
		playerAudio = player.GetComponents<AudioSource>();

        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Menu Button")) {
			if (pausePanel.activeInHierarchy) {
				ContinueGame();
                isPaused = false;
            } 
			else if (!pausePanel.activeInHierarchy){
				PauseGame();
                isPaused = true;
                StartCoroutine("HighlightButton");
            }
		}
	}


    IEnumerator HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return null;
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
    }

    

    void PauseGame () {
	    Time.timeScale = 0;
	    foreach(MonoBehaviour script in playerScripts)
        {
		    script.enabled = false;
        }
	    foreach(AudioSource audio in playerAudio)
        {
			    audio.Pause();
        }
	    cameraOrbitX.GetComponent<CameraVertical>().enabled = false;
	    pausePanel.SetActive(true);
	}

	void ContinueGame () {
		Time.timeScale = 1;
		foreach(MonoBehaviour script in playerScripts)
     	{
			script.enabled = true;	
     	}
		 foreach(AudioSource audio in playerAudio)
     	{
			 audio.UnPause();
     	}
		cameraOrbitX.GetComponent<CameraVertical>().enabled = true;
		pausePanel.SetActive(false);
	}
}
