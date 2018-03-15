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

    public AudioSource deathAudio;
    public AudioSource bgm;

	// Use this for initialization
	void Start () {
        healthPoints = 5;
        deathAudio = GetComponent<PlayerAudio>().deathAudio;
        bgm = GetComponent<PlayerAudio>().bgmAudio;
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
            GetComponent<MeshExplosion>().SplitMesh(3.0f, 4.0f);
            exploded = true;
        }
    }

    void OnCollisionEnter (Collision hit) {

        // TODO: Touching "Terrain" auto-kills you? Maybe wanna change this
        if (hit.gameObject.tag == "Terrain") {
            SceneManager.LoadScene("GameOver");
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
                //SceneManager.LoadScene("GameOver");
                // now handled in MeshExplosion, exclusively for Player obj
            }
        }
    }

}
