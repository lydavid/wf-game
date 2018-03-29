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

    // Resets all stored info form previous player except for our player index tracking
    void ResetAllPlayerPrefs()
    {
        PlayerPrefs.SetString(Constants.DATE_KEY, "");
        PlayerPrefs.SetFloat(Constants.SCORE_KEY, Mathf.Infinity);
        PlayerPrefs.SetString(Constants.NAME_KEY, "");
        PlayerPrefs.SetInt(Constants.WARPS_KEY, 0);
        PlayerPrefs.SetInt(Constants.KILLS_KEY, 0);
        PlayerPrefs.Save();
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
