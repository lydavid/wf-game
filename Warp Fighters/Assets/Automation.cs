using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automation : MonoBehaviour {

    PhysicMaterial frictionlessMaterial;

	// Use this for initialization
	void Start () {

        /* Automatically make all mesh colliders in scene convex
         * all obj tagged as Wall given frictionless material
         */
        MeshCollider[] meshColliders = GameObject.FindObjectsOfType(typeof(MeshCollider)) as MeshCollider[];
        foreach (MeshCollider mc in meshColliders)
        {
            /*if (mc.gameObject.tag != "Player" && mc.gameObject.layer != 10) // don't do this for the player and interactables
            {
                if (mc.gameObject.tag == "Wall")
                {
                    TurnIntoConvexTriangles b = mc.transform.root.gameObject.AddComponent<TurnIntoConvexTriangles>();
                    b.SplitMesh(frictionlessMaterial);

                } else {
                    //mc.material = frictionlessMaterial;
                    TurnIntoConvexTriangles b = mc.transform.root.gameObject.AddComponent<TurnIntoConvexTriangles>();
                    b.SplitMesh();
                }

            }*/
            //Debug.Log(mc.gameObject.name);
            //mc.convex = true;
            //mc.gameObject.AddComponent<TurnIntoConvexTriangles>();
            
        }
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
