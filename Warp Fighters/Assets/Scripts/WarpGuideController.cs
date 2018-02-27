using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WarpGuideController : MonoBehaviour {

    GameObject player;
    //Vector3 offset;

	public int warpSpeed;
    public int warpGuideSpeed;

    public bool isSpeedWarpGuide; // whether this guide is a speed warp version or not (standard)

    private float dist; // distance between guide and player


    
    private bool inSpeedWarp; // flag to indicate that player has entered speed warp

    float scroll;
    float scroll1;

    private bool warpGuideToggleAvailable;

    private Text debugText;

    public void SetAsSpeedWarpGuide(bool speedWarpGuide)
    {
        isSpeedWarpGuide = speedWarpGuide;
    }

    private void Reset()
    {
        player = GameObject.Find("Player");
        //offset = player.transform.position - transform.position;
		warpSpeed = 100;
        warpGuideSpeed = 20;
		dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
        inSpeedWarp = false;
        warpGuideToggleAvailable = false;
        debugText = GameObject.Find("DebugTextGuide").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        //offset = player.transform.position - transform.position;
		warpSpeed = 100;
        warpGuideSpeed = 20;
        dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
        inSpeedWarp = false;
        warpGuideToggleAvailable = false;
        debugText = GameObject.Find("DebugTextGuide").GetComponent<Text>();

        SetDebugText();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Left Bumper"))
        {

            //GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.blue;
            }
        }

        // red = superspeed warp
        if (Input.GetButtonDown("Right Bumper"))
        {
            
            //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.red;
            }
        }




        scroll = Input.GetAxis("Left Trigger"); //Input.GetAxis("Mouse ScrollWheel"); //
        scroll1 = Input.GetAxis("Right Trigger");
        //transform.position -= transform.forward * warpGuideSpeed * scroll;
        //dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
        if (scroll > 0f) // forward
        {
            transform.position += transform.forward * scroll;
            dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));

        }
        else if (scroll1 > 0f) // backwards
        {
            transform.position -= transform.forward * scroll1;
			dist = Mathf.Round(Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2)));
        }

        if (inSpeedWarp)
        {
            // send player flying towards this warp guide
            Vector3 dir = (transform.position - player.transform.position).normalized;

            player.GetComponent<Rigidbody>().velocity = dir * warpSpeed;
            inSpeedWarp = false;
            Destroy(gameObject);
            // note that we can't destroy this object until the player reaches it
            // but if it takes too long (ie player may have hit something in the way), make them stop and destroy this anyways
            // should probably also disable the warp guide's maintaining distance to player, otherwise it will move with the player
            // also make it so that lifting right click does not make warp guide disappear under this flag

            
        }

        
    }

    void LateUpdate()
    {

        if (!inSpeedWarp)
        {
            transform.position = player.transform.position + player.transform.forward * dist;
            transform.forward = player.transform.forward;
        }

        if (Input.GetButtonDown("Fire1") && !Input.GetButtonDown("Fire2"))
        {
            SetPlayerUpright();

            if (isSpeedWarpGuide)
            {
                // switch flag to begin speed warping
                inSpeedWarp = true;
            } else
            {   
                
                //Plays warp audio
                AudioSource audio = player.GetComponent<AudioSource>();
                audio.Play();

                player.transform.position = transform.position;
                player.GetComponent<Animator>().SetBool("isWarpGuideActive", false);
                Destroy(gameObject);
            }
        }

        if (Input.GetButtonDown("Fire2") && warpGuideToggleAvailable)
        {
            SetPlayerUpright();
            Destroy(gameObject);
        }

        warpGuideToggleAvailable = true;

        SetDebugText();

    }

    // makes player upright, but also maintaining player's turn direction ie y rotation)
    void SetPlayerUpright()
    {
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
    }

    void SetDebugText()
    {
        debugText.text = "Guide: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
        debugText.text += "\nRot: (" + transform.eulerAngles.x + ", " + transform.eulerAngles.y + ", " + transform.eulerAngles.z + ")";
        debugText.text += "\nDist: " + dist;
        debugText.text += " inWarpSpeed: " + inSpeedWarp;
    }
		
}