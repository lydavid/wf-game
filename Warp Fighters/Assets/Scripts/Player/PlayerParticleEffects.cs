using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffects : MonoBehaviour {

    public ParticleSystem ps1;
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
        if (humanBullet.bulletMode)
        {
            warpParticles.Simulate(0);
        }
        else
        {
            warpParticles.Pause();
        }
    }



    private void OnCollisionEnter(Collision other)
    {
     //   if (other.gameObject.layer != 9)
       // {
            //ParticleSystem ps = GetComponent<ParticleSystem>();
            // Collision light burst effect
         //   ps1.Play();
        //}
    }
}
