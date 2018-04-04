using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script deals with the player's HP, taking dmg and dealing dmg
// maybe rename to CombatManager
public class HPManager : MonoBehaviour {

    public int healthPoints;  // instead of a health bar, let's use something like hearts (ie hits)
    public bool isDead;
    bool exploded;

    PlayerAudio playerAudio;

    AudioSource bgm;
    AudioSource healthLowAudio;
    AudioSource deathAudio;
    

	// Use this for initialization
	void Start () {
        healthPoints = 5;

        playerAudio = GetComponent<PlayerAudio>();

        bgm = playerAudio.bgmAudio;
        healthLowAudio = playerAudio.healthLowAudio;
        deathAudio = playerAudio.deathAudio;
        

        isDead = false;
        exploded = false;
	}

    private void Update()
    {
        if (isDead)
        {
            WaitAndExplode();
        }
    }

    void WaitAndExplode()
    {
        if (!deathAudio.isPlaying && !exploded)
        {
            // Explode character upon end of death sound
            GetComponent<MeshExplosion>().SplitMesh(3.0f, 50f);
            exploded = true;
        }
    }

    void OnCollisionEnter (Collision hit) {

        // TODO: Touching "Terrain" auto-kills you? Maybe wanna change this
        if (hit.gameObject.tag == "Terrain") {
            SceneManager.LoadScene(Constants.SCENE_GAME_OVER);
        }
    }

    public void Damage(int damage)
    {
        if (!isDead)
        {
            healthPoints -= damage;
            if (healthPoints <= 0)
            {

                // Before loading GameOver screen, make player character play death sound, and stop bgm
                bgm.Stop();
                deathAudio.Play();
                isDead = true;

                // Make character fall over
                //transform.Rotate(new Vector3(0, 0, 90));



                // GAME OVER
                
                // now handled in MeshExplosion, exclusively for Player obj
            } else if (healthPoints <= 1)
            {
                healthLowAudio.Play();
            }
        }
    }

}
