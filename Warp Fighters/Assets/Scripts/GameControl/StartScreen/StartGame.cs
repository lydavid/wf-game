using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	AudioSource bgm;
	public Animator anim;
	public Image black;

	// Use this for initialization
	void Start () 
	{
		bgm = GetComponent<AudioSource>();	
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (InputManager.MenuButton())
        {
            //PlayerPrefs.SetInt(Constants.SCENE_TO_LOAD, 0);// 0); // 0 is Beta 0.0, 1 is Alpha3.0
            bgm.Play();
            StartCoroutine(Fade());
        }

        /*if (InputManager.WindowButton())
        {
            PlayerPrefs.SetInt(Constants.SCENE_TO_LOAD, 1);
            bgm.Play();
            StartCoroutine(Fade());
        }*/
	}


	IEnumerator Fade () 
	{	
		anim.SetBool("Fade", true);
		yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Constants.SCENE_LOADING);
    }
}
