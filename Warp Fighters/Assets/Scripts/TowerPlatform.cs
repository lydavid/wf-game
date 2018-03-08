using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlatform : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		//anim.SetBool("PlatformStepped", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Player") {
			anim.SetBool("PlatformStepped", true);
		}

	}

	void OnCollisionExit (Collision hit) {
		if (hit.gameObject.tag == "Player") {
			anim.SetBool("PlatformStepped", false);
		}
	}

	IEnumerator Pause () {
		 yield return new WaitForSeconds(1);
	}
}
