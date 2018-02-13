using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	private bool playerNotified = false;
	[SerializeField]
	private GameObject NPC;
	private AudioSource instructions;
	// Use this for initialization
	void Start () {
		instructions = NPC.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c){
		if (!playerNotified){
			instructions.Play();
		}
		playerNotified = true;
		if (c.CompareTag("Player")){
			Debug.Log("player entered");
		}
	}

	/* 
	void onCollisionEnter(Collision c){
		Debug.Log("collision");
		if (c.gameObject.tag == "NPCTrigger"){
			Debug.Log("player entered");
		}
	}*/
}
