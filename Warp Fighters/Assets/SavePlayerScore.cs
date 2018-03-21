using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


// This script handles writing all relevant user data to a csv file
// which will be used for the leaderboards
public class SavePlayerScore : MonoBehaviour {

    public GameObject b;
    Text c;
    int ID;

	// Use this for initialization
	void Start () {
        c = b.GetComponent<Text>();
        if (PlayerPrefs.HasKey(Constants.ID_KEY))
        {
            ID = PlayerPrefs.GetInt(Constants.ID_KEY) + 1;
        } else
        {
            ID = 0;
        }
        PlayerPrefs.SetInt(Constants.ID_KEY, ID);
        PlayerPrefs.Save();

        c.text = ID.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
