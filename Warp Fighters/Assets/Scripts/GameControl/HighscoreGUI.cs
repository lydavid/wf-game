using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreGUI : MonoBehaviour {

	public Text title;
    public Font font;
    public GameObject letters;
    private RectTransform lettersTransform;

	private float width, height;
    private List<Text> individualLetters = new List<Text>();
    private string alpha;


    private int curLetPos, prevLetPos;



    // Use this for initialization
    void Start () 
	{
        alpha = "abcdefghijklmnopqrstuvwxyz";
        //string[] res = UnityEditor.UnityStats.screenRes.Split('x');
        //width = int.Parse(res[0]);
        //height = int.Parse(res[1]);
        width = Screen.width;
        height = Screen.height;
        lettersTransform = letters.GetComponent<RectTransform>(); 
        Debug.Log(height);

        initLetters();
        prevLetPos = 0;
        curLetPos = 0;
        
    }
	
	// Update is called once per frame
	void Update () 
	{
        Controls();
		LetterSelect();
        Debug.Log(individualLetters);
    }

    void Controls ()
    {
        float x = Input.GetAxis("D-Pad X Axis");
        float y = Input.GetAxis("D-Pad Y Axis");

        if (Math.Abs(x) != 0)
        {
            // move right
            if (x > 0)
            {
                if (curLetPos == 9)
                {
                    curLetPos = 0;
                    prevLetPos = 9;
                }
                else if (curLetPos == 19)
                {
                    curLetPos = 10;
                    prevLetPos = 19;
                }
                else
                {
                    prevLetPos = curLetPos;
                    curLetPos += 1;
                }
            }

            // move left
            if (x < 0)
            {
                if (curLetPos == 0)
                {
                    curLetPos = 9;
                    prevLetPos = 0;
                }
                else if (curLetPos == 10)
                {
                    curLetPos = 19;
                    prevLetPos = 10;
                }
                else
                {
                    prevLetPos = curLetPos;
                    curLetPos -= 1;
                }
            }
        }
        else if (Math.Abs(y) != 0)
        {
            //move up

            //move down

        }
    }

    
	void LetterSelect ()
	{
        individualLetters[prevLetPos].color = Color.white;
        individualLetters[curLetPos].color = Color.yellow;
	}

    void initLetters ()
    {
        float x = -270;
        float y = 75;

        for (int i = 0; i < alpha.Length; i++)
        {
            if (i % 10 == 0 && i != 0)
            {
                x = -270;
                y -= 70;
            }
            char c = Char.ToUpper(alpha[i]);
            Debug.Log(c);
            GameObject let = new GameObject("" + c);
            let.transform.SetParent(letters.transform);

            RectTransform trans = let.AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(60, 70);
            trans.localPosition = new Vector2(x, y);

            // text and position
            Text text = let.AddComponent<Text>();
            text.alignment = TextAnchor.MiddleCenter;
            text.text = "" + c;

            // font and color
            text.font = font;
            text.fontSize = 30;
            text.color = Color.white;

            individualLetters.Add(text);
            x += 60;
        }
    }
}
