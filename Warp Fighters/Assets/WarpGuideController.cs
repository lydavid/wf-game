using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WarpGuideController : MonoBehaviour {

    GameObject player;
    Vector3 offset;

    private Text debugText;

    private void Reset()
    {
        player = GameObject.Find("Player");
        offset = player.transform.position - transform.position;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        offset = player.transform.position - transform.position;

        debugText = GameObject.Find("DebugTextGuide").GetComponent<Text>();

        SetDebugText();
    }
	
	// Update is called once per frame
	void Update () {

        // moves with player
        //transform.position = player.transform.position - offset;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            transform.position += player.transform.forward * 2;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            transform.position -= player.transform.forward * 2;
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

        SetDebugText();
    }

    void SetDebugText()
    {
        debugText.text = "Guide: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
        debugText.text += "\nRot: (" + transform.eulerAngles.x + ", " + transform.eulerAngles.y + ", " + transform.eulerAngles.z + ")";
    }
}
