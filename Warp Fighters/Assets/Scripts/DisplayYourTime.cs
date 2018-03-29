using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayYourTime : MonoBehaviour {

    Text yourTimeText;

	// Use this for initialization
	void Start () {
        yourTimeText = GetComponent<Text>();
        yourTimeText.text = StringHelpers.TimeInSecondsToFormattedString(PlayerPrefs.GetFloat(Constants.SCORE_KEY));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
