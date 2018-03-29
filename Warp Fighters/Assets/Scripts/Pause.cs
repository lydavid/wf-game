using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	[SerializeField]
	private GameObject pausePanel;

	private GameObject cameraOrbitX;

	private GameObject player;

    MonoBehaviour[] playerScripts;
	AudioSource[] playerAudio;

    bool isPaused;
    public EventSystem eventSystem;

    public GameObject resumeButtonGO;
    public GameObject quitButtonGO;

    public GameObject quitOptions;

    public GameObject quitToStartGO;
    public GameObject quitToDesktopGO;

    bool inQuitOptions;

	// Use this for initialization
	void Start () {
		cameraOrbitX = GameObject.Find("CameraOrbitX");
		player = GameObject.FindGameObjectWithTag("Player");
		playerScripts = player.GetComponents<MonoBehaviour>();
		playerAudio = player.GetComponents<AudioSource>();

        isPaused = false;

        Button resumeButtton = resumeButtonGO.GetComponent<Button>();
        resumeButtton.onClick.AddListener(ResumeButtonClick);

        Button quitButton = quitButtonGO.GetComponent<Button>();
        quitButton.onClick.AddListener(QuitButtonClick);


        Button quitToStartButton = quitToStartGO.GetComponent<Button>();
        quitToStartButton.onClick.AddListener(QuitToStartButtonClick);

        Button quitToDesktopButton = quitToDesktopGO.GetComponent<Button>();
        quitToDesktopButton.onClick.AddListener(QuitToDesktopButtonClick);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Menu Button")) {
			if (pausePanel.activeInHierarchy || quitOptions.activeInHierarchy) {
				ContinueGame();
                isPaused = false;
                inQuitOptions = false;
            } 
			else { //if (!pausePanel.activeInHierarchy){
				PauseGame();
                isPaused = true;
                StartCoroutine("HighlightButton");
            }
		}

        if (inQuitOptions)
        {
            if (Input.GetKeyDown("escape") || Input.GetButtonDown("B Button"))
            {
                quitOptions.SetActive(false);
                pausePanel.SetActive(true);
                eventSystem.SetSelectedGameObject(resumeButtonGO);
                inQuitOptions = false;
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
        quitOptions.SetActive(false);
	}

    void ResumeButtonClick()
    {
        ContinueGame();
        isPaused = false;
    }

    void QuitButtonClick()
    {
        // Brings up a new panel with option to quit to start or to desktop
        pausePanel.SetActive(false);
        quitOptions.SetActive(true);
        eventSystem.SetSelectedGameObject(quitToStartGO);
        inQuitOptions = true;
    }

    void QuitToStartButtonClick()
    {
        SceneManager.LoadScene("StartScreen");
    }

    void QuitToDesktopButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
