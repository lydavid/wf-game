using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLighting : MonoBehaviour {

    public GameObject cLight;
    public GameObject yLight;
    public GameObject mLight;
    public GameObject bLight;

    KeyType keyType;

    // Use this for initialization
    void Start () {
        keyType = GetComponent<CollectibleKey>().keyType;
        switch (keyType)
        {
            case KeyType.Blue:
                bLight.SetActive(true);
                break;
            case KeyType.Cyan:
                cLight.SetActive(true);
                break;
            case KeyType.Magenta:
                mLight.SetActive(true);
                break;
            case KeyType.Yellow:
                yLight.SetActive(true);
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
