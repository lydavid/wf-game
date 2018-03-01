using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAudio : MonoBehaviour {

	public AudioClip instantWarpClip;
	public GameObject Helper;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake () {
		AudioSource instantWarpAudio = Helper.GetComponent<AudioController>().AddAudio(gameObject, instantWarpClip, false, false, 0.2f);
	}
}

