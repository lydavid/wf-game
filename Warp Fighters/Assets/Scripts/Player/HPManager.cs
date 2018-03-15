using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script deals with the player's HP, taking dmg and dealing dmg
// maybe rename to CombatManager
public class HPManager : MonoBehaviour {

    public int healthPoints;  // instead of a health bar, let's use something like hearts (ie hits)


	// Use this for initialization
	void Start () {
        healthPoints = 5;
	}

    void OnCollisionEnter (Collision hit) {

        // TODO: Touching "Terrain" auto-kills you? Maybe wanna change this
        if (hit.gameObject.tag == "Terrain") {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Damage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {

            // Before loading GameOver screen, make player character play death sound, and explode
            GetComponent<PlayerAudio>().deathAudio.Play();

            // Make character fall over
            transform.Rotate(new Vector3(0, 0, 90));

            // Then explode
            GetComponent<MeshExplosion>().SplitMesh(3.0f, 4.0f);

            // GAME OVER
            //SceneManager.LoadScene("GameOver");
            // now handled in MeshExplosion, exclusively for Player obj
        }
    }

}
