using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Designates a child gameObject as the center representative of this entire gameobject, used for locking on target
public class Center : MonoBehaviour {

    public bool useCenterOfRenderer;

    [Header("If above is not checked")]
    public GameObject centerRep;
    public Vector3 center;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    // Process and returns the center or centerRep of this gameobject
    // by making this a function rather than updating center constantly in Update, we can improve performance
    public Vector3 GetCenter()
    {
        
        if (!useCenterOfRenderer)
        {
            //Debug.Log(centerRep.name);
            center = centerRep.GetComponent<Renderer>().bounds.center;
        }
        else
        {
            center = gameObject.GetComponent<Renderer>().bounds.center;
        }
        return center;
    }
}
