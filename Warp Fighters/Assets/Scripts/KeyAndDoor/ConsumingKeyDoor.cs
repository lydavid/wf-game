using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumingKeyDoor : MonoBehaviour {

    public KeyType doorType;
    public Material transparentMaterial; // transparent version of material that indicates this door is warp-through-able
    

    GameObject player;  // These types of doors will need to keep a reference to the player at start
    Material originalMaterial;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        originalMaterial = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

        // Turn object transparent to indicate player can warp through
		if (player.GetComponent<CollectedKeysManager>().HasKey(doorType))
        {
            gameObject.GetComponent<Renderer>().material = transparentMaterial;
        } else
        {
            // otherwise change object material back to original
            gameObject.GetComponent<Renderer>().material = originalMaterial;
        }
	}

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // consume one of the keys in player's stash
            other.gameObject.GetComponent<CollectedKeysManager>().ConsumeKey(doorType);
        }
    }*/
}
