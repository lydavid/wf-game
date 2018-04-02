using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardGUI : MonoBehaviour {

    string[] players;
    CsvIO csv;

	// Use this for initialization
	void Start ()
    {
        csv = GetComponent<CsvIO>();
        players = csv.dataOUT;
        DisplayPlayers();
    }

	// Update is called once per frame
	void Update ()
    {
      
	}

    void DisplayPlayers ()
    {
       
        /*foreach (String s in players)
        {
            //Debug.Log(s);
        }*/

    }
}
