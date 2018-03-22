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

    private String initials = "";
    private int curLetPos, prevLetPos;
    private int prevDButtonX, prevDButtonY;
    private int curDButtonX, curDButtonY;



    // Use this for initialization
    void Start () 
	{
        alpha = "abcdefghijklmnopqrstuvwxyz";
        width = Screen.width;
        height = Screen.height;
 
        // draw the 26 letters on canvas
        initLetters();

        // tracks selection position
        prevLetPos = 0;
        curLetPos = 0;

        prevDButtonX = curDButtonX = DPadButton.countX;
        prevDButtonY = curDButtonY = DPadButton.countY;
        
    }
	
	// Update is called once per frame
	void Update () 
	{
        Controls();
        LetterSelect();
        EnterInitials();
    }

    void EnterInitials ()
    {
        if (Input.GetButtonDown("Menu Button"))
        {
           // go to next scene
        }
        if (Input.GetButtonDown("A Button"))
        {
            if (initials.Length < 3)
            {
                initials = initials + individualLetters[curLetPos].text;
            }
            
        }
        if (Input.GetButtonDown("B Button"))
        {
            if (initials.Length > 0)
            {
                initials = initials.Substring(0, initials.Length - 1);
            }

        }
    }

    void Controls ()
    {
        curDButtonX = DPadButton.countX;
        if (curDButtonX != prevDButtonX)
        {
            if (DPadButton.right)
            {
                if (curLetPos == 9)
                {
                    prevLetPos = 8;
                }
                else if (curLetPos == 19)
                {
                    prevLetPos = 18;
                }
                else if (curLetPos == 25)
                {
                    prevLetPos = 24;
                }
                else
                {
                    prevLetPos = curLetPos;
                    curLetPos += 1;
                }
            }
            else if (DPadButton.left)
            {
                if (curLetPos == 0)
                {
                    prevLetPos = 1;
                }
                else if (curLetPos == 10)
                {
                    prevLetPos = 11;
                }
                else if (curLetPos == 20)
                {
                    prevLetPos = 21;
                }
                else
                {
                    prevLetPos = curLetPos;
                    curLetPos -= 1;
                }
            }
            prevDButtonX = curDButtonX;
        }

        curDButtonY = DPadButton.countY;
        if (curDButtonY != prevDButtonY)
        {
            if (DPadButton.up)
            {
                if (curLetPos <= 9)
                {
                    prevLetPos = 10;
                }
                else
                {
                    prevLetPos = curLetPos;
                    curLetPos -= 10;
                }
            }
            else if (DPadButton.down)
            {
                if (curLetPos >= 20 || curLetPos == 16 || curLetPos == 17 || curLetPos == 18 || curLetPos == 19)
                {
                    prevLetPos = 10;
                }
                else
                {
                    prevLetPos = curLetPos;
                    curLetPos += 10;
                }
            }
            prevDButtonY = curDButtonY;
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

    IEnumerator Pause()
    {
       yield return new WaitForSeconds(5);
    }
}
