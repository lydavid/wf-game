using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrackingCamera : MonoBehaviour {

    Vector3 offset;
    public GameObject player;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetKeyDown("space"))
        {
            transform.position += new Vector3(0, 0, 8);
        }
        
	}
}
