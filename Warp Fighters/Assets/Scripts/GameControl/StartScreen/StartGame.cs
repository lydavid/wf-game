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
		if (Input.GetButtonDown("Menu Button") || Input.GetKeyDown("enter") || Input.GetKeyDown("left shift") || Input.GetKeyDown("return"))
        {
            PlayerPrefs.SetInt("Scene", 1);// 0); // 0 is Beta 0.0, 1 is Alpha3.0
            bgm.Play();
            StartCoroutine(Fade());
        }

        if (Input.GetButtonDown("Window Button") || Input.GetKeyDown("right shift"))
        {
            PlayerPrefs.SetInt("Scene", 1);
            bgm.Play();
            StartCoroutine(Fade());
        }
	}

	IEnumerator Fade () 
	{	
		anim.SetBool("Fade", true);
		yield return new WaitForSeconds(1);
		//SceneManager.LoadScene("JonLevel");
        SceneManager.LoadScene("LoadingScreen");
    }

    /*IEnumerator OtherFade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FullMap");
    }*/


}
