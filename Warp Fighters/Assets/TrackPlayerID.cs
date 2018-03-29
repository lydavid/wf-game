using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script keeps track of the ID of the current player (ie i_th player)
public class TrackPlayerID : MonoBehaviour {

    public int ID;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey(Constants.ID_KEY))
        {
            ID = PlayerPrefs.GetInt(Constants.ID_KEY) + 1;
        }
        else
        {
            ID = 0;
        }
        PlayerPrefs.SetInt(Constants.ID_KEY, ID);
        PlayerPrefs.Save();
    }
}
