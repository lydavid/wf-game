using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowCubes : MonoBehaviour {

    public Material[] materials;

    // Use this for initialization
    void Start () {

        int roll = Random.Range(0, materials.Length);
        GetComponent<Renderer>().material = materials[roll];
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
