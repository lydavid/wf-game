using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffects : MonoBehaviour {

    public ParticleSystem ps1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 9)
        {
            //ParticleSystem ps = GetComponent<ParticleSystem>();
            // Collision light burst effect
            ps1.Play();
        }
    }
}
