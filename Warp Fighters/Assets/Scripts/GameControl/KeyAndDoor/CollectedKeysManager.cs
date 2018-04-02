using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedKeysManager : MonoBehaviour {

    List<KeyType> collectedKeys;

    bool hasCyanKey;
    bool hasMagentaKey;
    bool hasYellowKey;

	// Use this for initialization
	void Start () {
        collectedKeys = new List<KeyType>();
	}
	

    // outside scripts should call this to add a key to player's key stash
    public void AddKey(KeyType keyType)
    {
        collectedKeys.Add(keyType);
        switch (keyType)
        {
            case KeyType.Cyan:
                hasCyanKey = true;
                break;
            case KeyType.Magenta:
                hasMagentaKey = true;
                break;
            case KeyType.Yellow:
                hasYellowKey = true;
                break;

        }
    }

    public bool HasKey(KeyType keyType)
    {
        return collectedKeys.Contains(keyType);
    }

    public bool HasAllKeys()
    {
        return hasCyanKey && hasMagentaKey && hasYellowKey;
    }

    // call this to remove a key from player's stash
    // it does not matter which one is removed, just that the right type is removed
    /*public void ConsumeKey(KeyType keyType)
    {
        collectedKeys.Remove(keyType);
    }*/
}
