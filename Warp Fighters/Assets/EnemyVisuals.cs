using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisuals : MonoBehaviour {

    BasicEnemyController BEC;

    [Header("Materials")]
    public Material green;
    public Material red;

    [Header("Light References")]
    public GameObject greenLight;
    public GameObject redLight;

    [Header("Eye Reference")]
    public GameObject eye;

	// Use this for initialization
	void Start () {

        

        BEC = GetComponent<BasicEnemyController>();

        if (BEC.enemyType == EnemyType.a_guard)
        {
            // Color green
            eye.GetComponent<Renderer>().material = green;
            greenLight.SetActive(true);


        } else if (BEC.enemyType == EnemyType.b_patrol)
        {
            // Color red
            eye.GetComponent<Renderer>().material = red;
            redLight.SetActive(true);

        } else
        {
            // ???
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
