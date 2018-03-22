﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadGameAsync : MonoBehaviour {

    int sceneToLoad;

	// Use this for initialization
	void Start () {
        sceneToLoad = PlayerPrefs.GetInt("Scene");
        StartCoroutine(LoadYourAsyncScene());
    }

    void Update()
    {
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad;
        if (sceneToLoad == 0)
        {
            //asyncLoad = SceneManager.LoadSceneAsync("Beta 0.0");
            asyncLoad = SceneManager.LoadSceneAsync("Alpha3.0");
        } else
        {
            asyncLoad = SceneManager.LoadSceneAsync("Alpha3.0");
        }

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
