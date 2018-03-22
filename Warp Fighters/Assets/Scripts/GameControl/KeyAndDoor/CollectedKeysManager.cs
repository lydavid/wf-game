using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedKeysManager : MonoBehaviour {

    List<KeyType> collectedKeys;

	// Use this for initialization
	void Start () {
        collectedKeys = new List<KeyType>();
	}
	
	// Update is called once per frame
	void Update () {
        if (collectedKeys.Count > 0)
        {
            //Debug.Log(collectedKeys[0]);
        }
	}

    // outside scripts should call this to add a key to player's key stash
    public void AddKey(KeyType keyType)
    {
        collectedKeys.Add(keyType);
    }

    public bool HasKey(KeyType keyType)
    {
        return collectedKeys.Contains(keyType);
    }

    // call this to remove a key from player's stash
    // it does not matter which one is removed, just that the right type is removed
    public void ConsumeKey(KeyType keyType)
    {
        collectedKeys.Remove(keyType);
    }
}
