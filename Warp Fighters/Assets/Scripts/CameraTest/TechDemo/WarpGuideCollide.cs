using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGuideCollide : MonoBehaviour {

	public bool guideHit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "Wall") {
			Debug.Log("Hit Wall");
			guideHit = true;
		}
	}

	 
	void OnCollisionExit(Collision c) {
		if (c.gameObject.tag == "Wall"){
			Debug.Log("awoll");
			guideHit = false;
		}
    }
}
