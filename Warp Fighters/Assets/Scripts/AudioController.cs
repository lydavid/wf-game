using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	AudioSource AddAudio (GameObject obj, AudioClip clip, bool loop, bool playOnAwake, float volume) {
		AudioSource audio = obj.AddComponent<AudioSource>();
		audio.clip = clip;
		audio.loop = loop;
		audio.playOnAwake = playOnAwake;
		audio.volume = volume;
		return audio;
	}

}
