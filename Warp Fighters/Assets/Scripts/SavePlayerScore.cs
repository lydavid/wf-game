using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.UI;


// This script handles writing all relevant user data to a csv file
// which will be used for the leaderboards
public class SavePlayerScore : MonoBehaviour {


    private string path = Application.dataPath + "Data/data.json";
    public GameObject b;
    Text c;
    int ID;

    TrackTime t;


    [Serializable]
    public class PlayerData
    {
        public string playerInitials;
        public float timeElapsed;
        public int enemiesKilled;
        public int warpCount;
    }

    PlayerData playerObj = new PlayerData();

    List<PlayerData> players = new List<PlayerData>(); // read data file into this array

	// Use this for initialization
	void Start () {
        /*c = b.GetComponent<Text>();
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

        playerObj.timeElapsed = 24;
        playerObj.playerInitials = "POO";
        playerObj.enemiesKilled = 0;
        playerObj.warpCount = 21;

        SaveAsJSON();*/
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    

    
    
    private void LoadGameData()
    {
    }

    private void SaveAsJSON()
    {
        string dataAsJson = JsonUtility.ToJson(playerObj);
        File.WriteAllText(path, dataAsJson);
    }
}
