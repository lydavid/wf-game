using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class CsvIO : MonoBehaviour
{
    //source: https://sushanta1991.blogspot.ca/2015/02/how-to-write-data-to-csv-file-in-unity.html
    private List<string[]> rowData = new List<string[]>();
    private string filePath;
    public string[] dataOUT;
    public Font font;
    public Canvas canvas;


    // Use this for initialization
    void Start()
    {
        filePath = "Saved_data.csv";//Application.dataPath + "/CSV/" + "Saved_data.csv";
        Save();
        Load();
        DisplayPlayers();
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

        //StringBuilder sb = new StringBuilder();

        //StreamWriter outStream = System.IO.File.AppendText(filePath);
        StreamWriter writer = new StreamWriter(filePath, true); // writes to file, appending if file exists

        for (int index = 0; index < length; index++)
            //ssb = string.Join(delimiter, output[index]);
            //outStream.WriteLine(ssb);
            writer.WriteLine(string.Join(delimiter, output[index]));

        //sb = sb.Replace(System.Environment.NewLine, "");

        
        //writer.WriteLine("Test");
        writer.Close();


        //outStream.Close();
    }

    void Load ()
    {
        String[] lines = System.IO.File.ReadAllLines(filePath);
        

        //dataOUT = lines;

        List<String> unsortedLines = new List<String>(lines);
        unsortedLines.RemoveRange(0, 1);

        foreach (String s in unsortedLines)
        {
            Debug.Log(s);
        }
        unsortedLines.Sort(delegate (String x, String y) {
            if (float.Parse(x.Split(',')[Constants.SCORE_INDEX]) > float.Parse(y.Split(',')[Constants.SCORE_INDEX])) return 1;
            else if (float.Parse(x.Split(',')[Constants.SCORE_INDEX]) < float.Parse(y.Split(',')[Constants.SCORE_INDEX])) return -1;
            else return 0;
            
        });
        foreach (String s in unsortedLines)
        {
            Debug.Log(s);
            
        }
        dataOUT = unsortedLines.ToArray();
    }

    void Update ()
    {
        
       
    }

    

    void DisplayPlayers ()
    {
        float x = 30;
        float y = 55;
        for (int i = 0; i < dataOUT.Length; i++)
        { 
            string[] playerData = (dataOUT[i].Trim()).Split(',');
            foreach (string s in playerData)
            {
                Debug.Log(s);
            }
            GameObject player = new GameObject(""+i);
            player.transform.SetParent(canvas.transform);

            RectTransform trans = player.AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(300, 30);
            trans.localPosition = new Vector2(x, y);

            // text and position
            Text text = player.AddComponent<Text>();
            text.alignment = TextAnchor.UpperLeft;
            text.text = "RANK " + (i + 1) + "   -   " + StringHelpers.TimeInSecondsToFormattedString(float.Parse(playerData[Constants.SCORE_INDEX])) + "   >   " + playerData[Constants.NAME_INDEX];

            // font and color
            text.font = font;
            text.fontSize = 25;
            text.color = Color.gray;
            y -= 20;
        }
    }

    
}