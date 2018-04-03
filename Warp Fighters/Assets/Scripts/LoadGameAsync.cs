using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameAsync : MonoBehaviour {

    public GameObject loadingBar;
    Slider loadingBarSlider;

    int sceneToLoad;
    float progress;

    GameObject startScreenBGM;

	// Use this for initialization
	void Start () {

        loadingBarSlider = loadingBar.GetComponent<Slider>();

        startScreenBGM = GameObject.Find("StartBGM");

        sceneToLoad = PlayerPrefs.GetInt(Constants.SCENE_TO_LOAD);
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
            asyncLoad = SceneManager.LoadSceneAsync("Beta 0.3");
            //asyncLoad = SceneManager.LoadSceneAsync("Alpha3.0");
        } else
        {
            asyncLoad = SceneManager.LoadSceneAsync("Alpha3.0");
        }

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            progress = asyncLoad.progress / 0.9f;  // since unity load goes from 0.0 to 0.9
            loadingBarSlider.value = progress;
            Debug.Log(progress);

            if (loadingBarSlider.value == 1.0f)  // loading is done
            {
                // kill the music carried over from start screen
                if (startScreenBGM)
                {
                    Destroy(startScreenBGM);
                }
            }

            yield return null;
        }
    }
}
