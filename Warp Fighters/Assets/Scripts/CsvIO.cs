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

        if (File.Exists(filePath))
        {
            Debug.Log("yes");
        }
        else
        {
            Debug.Log("no");
            // Creating First row of titles manually..
            // ID, Initials, Elapsed Time, Enemies Killed, Warp Count
            string[] rowDataTemp = new string[4];


            rowDataTemp[0] = "Initials";
            rowDataTemp[1] = "Elapsed Time";
            rowDataTemp[2] = "Enemies Killed";
            rowDataTemp[3] = "Warp Count";
            rowData.Add(rowDataTemp);
        }

        
 
        for (int i = 0; i < 1; i++)
        {
            string[] rowDataTemp = new string[4];
            rowDataTemp[0] = "ABB";
            rowDataTemp[1] = "" + UnityEngine.Random.Range(1, 200);
            rowDataTemp[2] = "" + UnityEngine.Random.Range(0, 5);
            rowDataTemp[3] = "" + UnityEngine.Random.Range(1, 20);
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

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
            if (int.Parse(x.Split(',')[1]) > int.Parse(y.Split(',')[1])) return 1;
            else if (int.Parse(x.Split(',')[1]) < int.Parse(y.Split(',')[1])) return -1;
            else return 0;
            
        });
        foreach (String s in unsortedLines)
        {
            Debug.Log(s);
        }
    }

    void Update ()
    {
        
       
    }

    void DisplayPlayers ()
    {
        float x = 30;
        float y = 55;
        for (int i = 1; i <= dataOUT.Length; i++)
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
            text.text = "RANK " + i + "   -   " + playerData[0] + "   >   " + playerData[1] + "s";

            // font and color
            text.font = font;
            text.fontSize = 25;
            text.color = Color.gray;
            y -= 20;
        }
    }

    
}