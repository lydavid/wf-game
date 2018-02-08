using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WarpGuideController : MonoBehaviour {

    GameObject player;
    //Vector3 offset;

	public int warpSpeed;

    private float dist; // distance between guide and player
    private bool inSpeedWarp;

    private Text debugText;

    private void Reset()
    {
        player = GameObject.Find("Player");
        //offset = player.transform.position - transform.position;
		warpSpeed = 50;
		dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
        inSpeedWarp = false;
        debugText = GameObject.Find("DebugTextGuide").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        //offset = player.transform.position - transform.position;
		warpSpeed = 50;
		dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
        inSpeedWarp = false;
        debugText = GameObject.Find("DebugTextGuide").GetComponent<Text>();

        SetDebugText();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("1"))
        {

            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.blue;
            }
        }

        // red = superspeed warp
        if (Input.GetKeyDown("2"))
        {

            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.red;
            }
        }





        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            //transform.forward = player.transform.forward;
            transform.position += transform.forward * 2;
			dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
            //offset = player.transform.position - transform.position;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            //transform.forward = player.transform.forward;
            transform.position -= transform.forward * 2;
			dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
            //offset = player.transform.position - transform.position;
        }

        if (inSpeedWarp)
        {
            // send player flying towards this warp guide
			player.GetComponent<Rigidbody>().velocity = player.transform.forward * warpSpeed;
            //inSpeedWarp = false;

            // note that we can't destroy this object until the player reaches it
            // but if it takes too long (ie player may have hit something in the way), make them stop and destroy this anyways
            // should probably also disable the warp guide's maintaining distance to player, otherwise it will move with the player
            // also make it so that lifting right click does not make warp guide disappear under this flag

            
        }

        
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
       Vector3 dir = point - pivot;
       dir = Quaternion.Euler(angles) * dir; // rotate it
       point = dir + pivot; // calculate rotated point
       return point; // return it
}

    void LateUpdate()
    {

        if (!inSpeedWarp)
        {
            transform.position = player.transform.position + player.transform.forward * dist;
            transform.forward = player.transform.forward;
        }

        if (Input.GetButtonDown("Fire1") && !Input.GetButtonUp("Fire2"))
        {

            if (gameObject.tag == "WarpGuide")
            {
                player.transform.position = transform.position;
                Destroy(gameObject);
            } else
            {
                // switch flag to begin speed warping
                inSpeedWarp = true;
            }


            
            
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
        debugText.text += "\nDist: " + dist;
    }
}
