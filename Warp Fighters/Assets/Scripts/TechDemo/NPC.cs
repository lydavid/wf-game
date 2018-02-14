using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	[SerializeField]
	private GameObject player;

    private bool played = false; // flag

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);

        
        if (!played && CheckCloseToTag("Player", 10))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            played = true;
 
        }

    }

    bool CheckCloseToTag(string tag, float minimumDistance)
    {
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < goWithTag.Length; ++i)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
                return true;
        }

        return false;
    }
}
