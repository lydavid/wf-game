using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDissolve : MonoBehaviour {

    Material mat;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
        Debug.Log(mat.name);
	}
	
	// Update is called once per frame
	void Update () {
        mat.SetFloat("_SliceAmount", mat.GetFloat("_SliceAmount") + 0.05f);
        mat.SetFloat("_BurnSize", mat.GetFloat("_BurnSize") + 0.05f);
        
	}
}
