using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script updates the HP sprites above the enemies head based on factors
// such as whether enemy is a boss type and how much HP remains
public class EnemyHPDisplay : MonoBehaviour {

    BasicEnemyController BEC;
    EnemyType enemyType;

    GameObject hp1; // farthest right
    GameObject hp2;
    GameObject hp3; // center hp
    GameObject hp4;
    GameObject hp5; // farthest left

    // hp depletes from right to left

    // Use this for initialization
    void Start () {
        BEC = GetComponent<BasicEnemyController>();
        enemyType = BEC.enemyType;

        Transform hp;

        foreach (Transform child in transform)
        {
            if (child.name == "HealthBar")
            {
                hp = child;
                hp1 = hp.Find("HP1").gameObject;
                hp2 = hp.Find("HP2").gameObject;
                hp3 = hp.Find("HP3").gameObject;
                hp4 = hp.Find("HP4").gameObject;
                hp5 = hp.Find("HP5").gameObject;
            }
        }




        SetUpHPSprites();
    }

    // this way we ensure BEC has the correct hp set up before calling this
    // note that we can't do it that way, cause this script refers to BEC, and so if we try to call this script form BEC's
    // Start method, BEC will not have been instantiated yet...
    public void SetUpHPSprites()
    {
        if (BEC.healthPoints > 0)
        {
            hp3.SetActive(true);
        } else
        {
            hp3.SetActive(false);
        }

        if (BEC.healthPoints > 1)
        {
            hp4.SetActive(true);
        }
        else
        {
            hp4.SetActive(false);
        }

        if (BEC.healthPoints > 2)
        {
            hp2.SetActive(true);
        }
        else
        {
            hp2.SetActive(false);
        }

        if (BEC.healthPoints > 3)
        {
            hp1.SetActive(true);
        }
        else
        {
            hp1.SetActive(false);
        }

        if (BEC.healthPoints > 4)
        {
            hp5.SetActive(true);
        }
        else
        {
            hp5.SetActive(false);
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
