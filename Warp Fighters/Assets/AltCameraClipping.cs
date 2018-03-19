using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltCameraClipping : MonoBehaviour {

    GameObject player;
    Camera cam;
    string playerTag;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponentInChildren<Camera>();
        playerTag = player.tag;
	}
	
	// Update is called once per frame
	void Update () {

        // raycast from player to camera, when something comes between them, move in until it is not
        /*Vector3 pos = cam.WorldToScreenPoint(player.transform.position);
        Ray ray = cam.ScreenPointToRay(pos);
        ray.direction = -ray.direction;
        ray.origin = player.transform.position;*/
        
        // raycast from camera to player
        // above code runs into problem that the raycast always goes through the player (starts from its front)
        Vector3 pos = cam.WorldToScreenPoint(player.transform.position);
        Ray ray = cam.ScreenPointToRay(pos);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

        RaycastHit hit;
        //bool hit = Physics.Raycast(ray, out hitInfo, 10);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);

        }

        // reset to default position when no longer the case

    }
}
