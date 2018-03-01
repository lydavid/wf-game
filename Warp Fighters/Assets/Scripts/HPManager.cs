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
        if (hit.gameObject.tag == "Terrain") {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Damage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            // GAME OVER
            SceneManager.LoadScene("GameOver");
        }
    }
}
