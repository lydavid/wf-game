using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAudio : MonoBehaviour {

	public AudioClip instantWarpClip;
	public AudioClip bgmClip;
	//public GameObject Helper;


	private AudioSource bgmAudio;

	// Use this for initialization
	void Start () {
		bgmAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake () {
		AudioSource instantWarpAudio = AddAudio(gameObject, instantWarpClip, false, false, 0.2f);
		bgmAudio = AddAudio(gameObject, bgmClip, true, true, 0.05f);
	}

	AudioSource AddAudio (GameObject obj, AudioClip clip, bool loop, bool playOnAwake, float volume) {
		AudioSource audio = obj.AddComponent<AudioSource>();
		audio.clip = clip;
		audio.loop = loop;
		audio.playOnAwake = playOnAwake;
		audio.volume = volume;
		return audio;
	}



}

