using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffects : MonoBehaviour {

    public ParticleSystem collisonParticles;
    public ParticleSystem warpParticles;

    HumanBullet humanBullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void DisplayWarpEffect ()
    {

    }


    void DisplayCollisionEffect ()
    {
        //if (humanBullet.bulletMode)
       // {
            collisonParticles.Play();
        //}
    }


    private void OnCollisionEnter(Collision other)
    {
        DisplayCollisionEffect();
        

        //   if (other.gameObject.layer != 9)
        // {
        //ParticleSystem ps = GetComponent<ParticleSystem>();
        // Collision light burst effect
        //   ps1.Play();
        //}
    }
}
