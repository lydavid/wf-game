using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script updates the HP sprites above the enemies head based on factors
// such as whether enemy is a boss type and how much HP remains
public class EnemyHPDisplay : MonoBehaviour {

    BasicEnemyController BEC;
    EnemyType enemyType;

    public GameObject hp1; // farthest right
    public GameObject hp2;
    public GameObject hp3; // center hp
    public GameObject hp4;
    public GameObject hp5; // farthest left

    // hp depletes from right to left

    // Use this for initialization
    void Start () {
        BEC = GetComponent<BasicEnemyController>();
        enemyType = BEC.enemyType;

        /*hp1 = transform.Find("HP1").gameObject;
        hp2 = transform.Find("HP2").gameObject;
        hp3 = transform.Find("HP3").gameObject;
        hp4 = transform.Find("HP4").gameObject;
        hp5 = transform.Find("HP5").gameObject;*/
        


    }

    // this way we ensure BEC has the correct hp set up before calling this
    public void SetUpHPSprites()
    {
        if (BEC.healthPoints > 0)
        {
            hp3.SetActive(true);
        }

        if (BEC.healthPoints > 1)
        {
            hp4.SetActive(true);
        }

        if (BEC.healthPoints > 2)
        {
            hp2.SetActive(true);
        }

        if (BEC.healthPoints > 3)
        {
            hp1.SetActive(true);
        }

        if (BEC.healthPoints > 4)
        {
            hp5.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeHPSprites(int hpLeft)
    {
        if (enemyType != EnemyType.c_boss)
        {
            hp3.SetActive(false); // only account for nonboss enemies having 1 HP
        } else
        {
            if (hpLeft == 4)
            {
                hp5.SetActive(false);
            } else if (hpLeft == 3)
            {
                hp4.SetActive(false);
            }
            else if (hpLeft == 2)
            {
                hp3.SetActive(false);
            }
            else if (hpLeft == 1)
            {
                hp2.SetActive(false);
            }
            else if (hpLeft == 0)
            {
                hp1.SetActive(false);
            }
        }
    }
}
