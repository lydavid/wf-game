using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Menu Button"))
        {
			audio.Play();
            SceneManager.LoadScene("Alpha");

        }
	}
}
