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
		if (Input.GetButtonDown("Menu Button") || Input.GetKeyDown("enter"))
        {
			bgm.Play();
            StartCoroutine(Fade());
        }

        if (Input.GetButtonDown("Window Button") || Input.GetKeyDown("right shift"))
        {
            bgm.Play();
            StartCoroutine(OtherFade());
        }
	}

	IEnumerator Fade () 
	{	
		anim.SetBool("Fade", true);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("JonLevel");
	}

    IEnumerator OtherFade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FullMap");
    }


}
