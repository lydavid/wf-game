using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGuideController : MonoBehaviour {

    GameObject target;
    GameObject player;
    Vector3 offset;

    private void Reset()
    {
        target = gameObject;
        player = GameObject.Find("Player");
        offset = player.transform.position - transform.position;
    }

    // Use this for initialization
    void Start () {
        target = gameObject;
        player = GameObject.Find("Player");
        offset = player.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            target.transform.position += Vector3.forward;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            target.transform.position -= Vector3.forward;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            Destroy(gameObject);
        }
    }
}
