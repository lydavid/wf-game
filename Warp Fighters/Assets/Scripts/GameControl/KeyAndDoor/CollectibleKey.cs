﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType { Cyan, Magenta, Yellow, Blue };

public class CollectibleKey : MonoBehaviour {

    public KeyType keyType;  // specifies the type of key this gameobject is

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            //Debug.Log("Player entered");
            //this.gameObject.SetActive(false);
            // indicate on the Player that they have collected this
            other.gameObject.GetComponent<CollectedKeysManager>().AddKey(keyType);
            // destroy this object
            Destroy(transform.gameObject);

        }
    }
}
