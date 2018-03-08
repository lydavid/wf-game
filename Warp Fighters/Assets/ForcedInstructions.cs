using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedInstructions : MonoBehaviour {

    public GameObject smallerInstructions;

    bool moved;
    bool looked;

	// Use this for initialization
	void Start () {
        //smallerInstructions = GameObject.FindGameObjectWithTag("Instructions");
	}
	
	// Update is called once per frame
	void Update () {
        // once player has moved and looked around, change big-ass instructions into smaller one
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            moved = true;
        }

        if (Input.GetAxis("Right Stick X") != 0 || Input.GetAxis("Right Stick Y") != 0 || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            looked = true;
        }

        if (moved && looked)
        {
            smallerInstructions.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
