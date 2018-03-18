using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");

       	transform.Translate(mH * speed * Time.deltaTime, 0f, mV * speed * Time.deltaTime);
	}
}
