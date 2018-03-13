using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	AudioSource audio;

	public Animator anim;
	public Image black;

	// Use this for initialization
	void Start () 
	{
		audio = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Menu Button"))
        {
			audio.Play();
            StartCoroutine(Fade());
        }
	}

	IEnumerator Fade () 
	{	
		anim.SetBool("Fade", true);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("JonLevel");
	}
}
