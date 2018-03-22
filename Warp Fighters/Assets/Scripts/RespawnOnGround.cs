using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QoL Script
// This script tracks the last position the player was standing on classified as "Ground"
// and when the player falls off, respawn them there.
public class RespawnOnGround : MonoBehaviour {

    Vector3 lastGroundPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            lastGroundPosition = transform.position;
            //Debug.Log(lastGroundPosition);

        } else if (other.gameObject.tag == "InvisibleWall")
        {
            // Respawn player at last ground position
            transform.position = lastGroundPosition;

            // Remove any velocity in case player had warped off from there
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
