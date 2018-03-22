using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvIO : MonoBehaviour
{
    //source: https://sushanta1991.blogspot.ca/2015/02/how-to-write-data-to-csv-file-in-unity.html
    private List<string[]> rowData = new List<string[]>();
    private string filePath;


    // Use this for initialization
    void Start()
    {
        filePath = Application.dataPath + "/CSV/" + "Saved_data.csv";
        Save();
        Load();
    }

    void Save()
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[4];
        rowDataTemp[0] = "Initials";
        rowDataTemp[1] = "Elapsed Time";
        rowDataTemp[2] = "Enemies Killed";
        rowDataTemp[3] = "Warp Count";
        rowData.Add(rowDataTemp);


        rowDataTemp = new string[4];
        rowDataTemp[0] = "Initials";
        rowDataTemp[1] = "Elapsed Time";
        rowDataTemp[2] = "Enemies Killed";
        rowDataTemp[3] = "Warp Count";
        rowData.Add(rowDataTemp);
        /*
        for (int i = 0; i < 10; i++)
        {
            rowDataTemp = new string[3];
            rowDataTemp[0] = "Sushanta" + i; // name
            rowDataTemp[1] = "" + i; // ID
            rowDataTemp[2] = "$" + UnityEngine.Random.Range(5000, 10000); // Income
            rowData.Add(rowDataTemp);
        }*/

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    void Load ()
    {
        String data = System.IO.File.ReadAllText(filePath);
        String[] lines = data.Split("\n"[0]);
        //String[] lineData = (lines[0].Trim()).Split(","[0]);
        Debug.Log("0: " + lines[0]);
        Debug.Log("1: " + lines[1]);
    }

    
}