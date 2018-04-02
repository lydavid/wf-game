using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Maybe rename to PlayerAudioManager
// Set up all the AudioClip and AudioSource in this script and allow for other scripts to call them from here.
public class PlayerAudio : MonoBehaviour {

    [Header("Clips")]
	public AudioClip bgmClip;
    public AudioClip warpClip;
    public AudioClip impactClip;
    public AudioClip deathClip;
    public AudioClip healthLowClip;

    [Header("Audio Sources")]
	public AudioSource bgmAudio;
    public AudioSource warpAudio;
    public AudioSource impactAudio;
    public AudioSource deathAudio;
    public AudioSource healthLowAudio;


    // Use this for initialization
    void Start () {
		bgmAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake () {

        bgmAudio = AddAudio(gameObject, bgmClip, true, true, 1f);

        warpAudio = AddAudio(gameObject, warpClip, false, false, 1f);
        impactAudio = AddAudio(gameObject, impactClip, false, false, 1f);
        deathAudio = AddAudio(gameObject, deathClip, false, false, 1f);
        healthLowAudio = AddAudio(gameObject, healthLowClip, false, false, 1f);
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

