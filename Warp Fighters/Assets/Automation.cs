using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automation : MonoBehaviour {

    public PhysicMaterial frictionlessMaterial;

	// Use this for initialization
	void Start () {

        /* Automatically make all mesh colliders in scene convex
         * all obj tagged as Wall given frictionless material
         */
        MeshCollider[] meshColliders = GameObject.FindObjectsOfType(typeof(MeshCollider)) as MeshCollider[];
        foreach (MeshCollider mc in meshColliders)
        {
            Debug.Log(mc.gameObject.name);
            mc.convex = true;
            if (mc.gameObject.tag == "Wall")
            {
                mc.material = frictionlessMaterial;
            }
        }
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
