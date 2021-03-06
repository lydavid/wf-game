﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;


// IO for CSV file, but seems to also handle everything else for Leaderboard scene
public class CsvIO : MonoBehaviour
{
    // modified from: https://sushanta1991.blogspot.ca/2015/02/how-to-write-data-to-csv-file-in-unity.html
    private List<string[]> rowData = new List<string[]>();
    private string filePath;
    public string[] dataOUT;
    public Font font;
    public Canvas canvas;
    

    int numPages = 0;
    int curPageNum = 0;
    List<GameObject> currentPage = new List<GameObject>();

    Color highlightThisPlayerColor = Color.cyan;
    int playerPageNum = 0;


    // Use this for initialization
    void Start()
    {
        filePath = "Saved_data.csv";//Application.dataPath + "/CSV/" + "Saved_data.csv";
        Save();
        Load();
        SetPlayerPageNum();
        DisplayPlayers(playerPageNum);
    }

    void Save()
    {
        string[] rowDataTemp = new string[6];
        if (!File.Exists(filePath))
        {
            // Creating First row of titles manually..
            rowDataTemp[Constants.ID_INDEX] = Constants.ID_KEY;
            rowDataTemp[Constants.DATE_INDEX] = Constants.DATE_KEY;
            rowDataTemp[Constants.SCORE_INDEX] = Constants.SCORE_KEY;
            rowDataTemp[Constants.NAME_INDEX] = Constants.NAME_KEY;
            rowDataTemp[Constants.WARPS_INDEX] = Constants.WARPS_KEY;
            rowDataTemp[Constants.KILLS_INDEX] = Constants.KILLS_KEY;
            rowData.Add(rowDataTemp);
        }

        rowDataTemp = new string[6];
        rowDataTemp[Constants.ID_INDEX] = "" + PlayerPrefs.GetInt(Constants.ID_KEY);
        rowDataTemp[Constants.DATE_INDEX] = PlayerPrefs.GetString(Constants.DATE_KEY);
        rowDataTemp[Constants.SCORE_INDEX] = "" + PlayerPrefs.GetFloat(Constants.SCORE_KEY);
        rowDataTemp[Constants.NAME_INDEX] = PlayerPrefs.GetString(Constants.NAME_KEY);
        rowDataTemp[Constants.WARPS_INDEX] = "" + PlayerPrefs.GetInt(Constants.WARPS_KEY);
        rowDataTemp[Constants.KILLS_INDEX] = "" + PlayerPrefs.GetInt(Constants.KILLS_KEY);
        rowData.Add(rowDataTemp);
        

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StreamWriter writer = new StreamWriter(filePath, true); // writes to file, appending if file exists

        for (int index = 0; index < length; index++)
        {
            writer.WriteLine(string.Join(delimiter, output[index]));
        }
        writer.Close();
    }

    void Load ()
    {
        String[] lines = System.IO.File.ReadAllLines(filePath);
        List<String> unsortedLines = new List<String>(lines);
        unsortedLines.RemoveRange(0, 1);

        unsortedLines.Sort(delegate (String x, String y) {
            if (float.Parse(x.Split(',')[Constants.SCORE_INDEX]) > float.Parse(y.Split(',')[Constants.SCORE_INDEX])) return 1;
            else if (float.Parse(x.Split(',')[Constants.SCORE_INDEX]) < float.Parse(y.Split(',')[Constants.SCORE_INDEX])) return -1;
            else if (float.Parse(x.Split(',')[Constants.SCORE_INDEX]) == float.Parse(y.Split(',')[Constants.SCORE_INDEX]) &&
                int.Parse(x.Split(',')[Constants.ID_INDEX]) < int.Parse(y.Split(',')[Constants.ID_INDEX])) return 1;
            else if (float.Parse(x.Split(',')[Constants.SCORE_INDEX]) == float.Parse(y.Split(',')[Constants.SCORE_INDEX]) &&
                int.Parse(x.Split(',')[Constants.ID_INDEX]) > int.Parse(y.Split(',')[Constants.ID_INDEX])) return -1;
            else return 0;
            
        });

        dataOUT = unsortedLines.ToArray();
        numPages = (int)Mathf.Ceil((float)dataOUT.Length / 10);
    }

    void Update ()
    {

        // LB: First page
        if (Input.GetButtonDown("Left Bumper"))
        {
            DisplayPlayers(0);
        }


        // RB: Last page
        if (Input.GetButtonDown("Right Bumper"))
        {
            DisplayPlayers(numPages - 1);
        }


        // Left D-pad: Prev page
        if (DPadButton.left)
        {
            Debug.Log("left");
            Debug.Log(curPageNum);
            if (curPageNum > 0)
            {
                DisplayPlayers(curPageNum - 1);
            }
        }


        // Right D-pad: Next page
        if (DPadButton.right)
        {
            Debug.Log("right");
            Debug.Log(curPageNum);
            if (curPageNum < numPages - 1)
            {
                DisplayPlayers(curPageNum + 1);
            }
        }


        // Window Button: Player's page
        if (InputManager.WindowButton())
        {
            DisplayPlayers(playerPageNum);
        }
    }


    void SetPlayerPageNum()
    {
        for (int i = 0; i < dataOUT.Length; i++)
        {
            string[] playerData = (dataOUT[i].Trim()).Split(',');
            if (int.Parse(playerData[Constants.ID_INDEX]) == PlayerPrefs.GetInt(Constants.ID_KEY))
            {
                playerPageNum = (int)Mathf.Floor((float)i / 10); // 0-9 -> pg0, 10-19 -> pg1
            }
        }
        Debug.Log(playerPageNum);
    }
    

    void DisplayPlayers (int pageNum)
    {

        // update current page number
        // we need to know this for our buttons
        curPageNum = pageNum;

        // empty anything on screen first before displaying
        foreach (GameObject playerEntry in currentPage)
        {
            Destroy(playerEntry);
        }
        currentPage.Clear();


        float x = 0; // 30
        float y = 55;


        // now make it only display 10 at a time depending on page number
        // page 1 -> i=0 to 9, page 2 -> i=10 to 19
        Debug.Log(pageNum);
        for (int i = 0 + pageNum * 10; i < Mathf.Min(10 + pageNum * 10, dataOUT.Length); i++)
        {
            string[] playerData = (dataOUT[i].Trim()).Split(',');
            foreach (string s in playerData)
            {
                //Debug.Log(s);
            }
            GameObject player = new GameObject(""+i);
            player.transform.SetParent(canvas.transform);
            player.layer = 5; // UI
            currentPage.Add(player);

            RectTransform trans = player.AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(400, 30);
            trans.localPosition = new Vector2(x, y);
            trans.localScale = new Vector2(1, 1);

            // text and position
            Text text = player.AddComponent<Text>();
            text.alignment = TextAnchor.MiddleCenter;
            text.text = StringHelpers.FormatRank(i + 1) + " - "
                + StringHelpers.TimeInSecondsToFormattedString(float.Parse(playerData[Constants.SCORE_INDEX])) + " - " 
                + playerData[Constants.NAME_INDEX] + " - " 
                + playerData[Constants.WARPS_INDEX].ToString() + " - " 
                + playerData[Constants.KILLS_INDEX].ToString();


            // font
            text.font = font;
            text.fontSize = 25;

            // color
            // highlight the text of the current player's score
            if (int.Parse(playerData[Constants.ID_INDEX]) == PlayerPrefs.GetInt(Constants.ID_KEY))
            {
                text.color = highlightThisPlayerColor;
            }
            else
            {
                text.color = Color.white;
            }

            y -= 20;
        }
    }
}
