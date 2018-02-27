using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndDoor : MonoBehaviour {

	GameObject button;
	GameObject door;
	

	// Use this for initialization
	void Start () {

		// We assume that the button and door are children of the gameobject with this script
        // and have the appropriate tags
		foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.tag == "Button")
            {
                button = child.gameObject;
            } else if (child.tag == "Door")
            {
                door = child.gameObject;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
