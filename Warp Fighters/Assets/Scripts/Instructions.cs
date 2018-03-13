using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

	public GameObject line1;
	public GameObject line2;
	public GameObject line3;

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider hit) 
	{
		Debug.Log("ting");
	
		player.transform.Translate(new Vector3(0,-1,0));
		
	}
}
