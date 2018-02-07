using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGuideController : MonoBehaviour {

    GameObject player;
    Vector3 offset;

    private void Reset()
    {
        player = GameObject.Find("Player");
        offset = player.transform.position - transform.position;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        offset = player.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        // moves with player
        //transform.position = player.transform.position - offset;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            transform.position += Vector3.forward * 2;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            transform.position -= Vector3.forward * 2;
        }

        
    }

    private void LateUpdate()
    {

        if (Input.GetButtonDown("Fire1") && !Input.GetButtonUp("Fire2"))
        {
            player.transform.position = transform.position;
            Destroy(gameObject);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            Destroy(gameObject);
        }
    }
}
