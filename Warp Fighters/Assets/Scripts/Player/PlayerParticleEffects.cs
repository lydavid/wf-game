using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEffects : MonoBehaviour {

    public ParticleSystem collisonParticles;
    public ParticleSystem warpParticles;

    HumanBullet humanBullet;

	// Use this for initialization
	void Start () {
        humanBullet = GetComponent<HumanBullet>();
	}
	
	// Update is called once per frame
	void Update () {
        DisplayWarpTrailEffect();
	}


    void DisplayCollisionEffect ()
    {
        if (humanBullet.bulletMode)
        {
            collisonParticles.Play();
        }
    }

    void DisplayWarpTrailEffect ()
    {
        if (humanBullet.bulletMode)
        {
            warpParticles.Play();
        }
        else
        {
            warpParticles.Stop();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        DisplayCollisionEffect();
    }
}
