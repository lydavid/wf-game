using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AltPlayerController : MonoBehaviour {

    public GameObject player;
    public float speed;
    public int init_dist; // initial distance to spawn warp guide

    public int rotSpeedX; // speed of rotating player with horizontal mouse axis
    public int rotSpeedY;

    public bool usePC; // flag to determine whether to use PC setup or Controller

    /*[Header("Jump")]
    [Range(1, 10)]
    public float jumpVelocity;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;*/


    public Material standardWarpGuide;
    public Material altWarpGuide;

    Rigidbody rb;

    private bool warpGuideToggleAvailable;

    [Header("Debug")]
    public Text debugText;

    private GameObject warpGuidePrefab; // stores location of prefab
    private GameObject warpGuideRedPrefab; // stores prefab of red version
    private bool isSpeedWarp;

    Vector3 angle;

    private void Reset()
    {
        player = GameObject.Find("Player");
        speed = 1f;// 5.0f;
        init_dist = 10;
        rotSpeedX = 5;
        rotSpeedY = 1;
        usePC = false;
        warpGuideToggleAvailable = true;
        debugText = GameObject.Find("DebugTextPlayer").GetComponent<Text>();


        // loads prefab from Resources folder at runtime
        warpGuidePrefab = (GameObject)Resources.Load("Resources/prefabs/Warp Guide", typeof(GameObject));
        warpGuideRedPrefab = (GameObject)Resources.Load("Prefabs/Warp Guide Red", typeof(GameObject));
        isSpeedWarp = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        angle = transform.eulerAngles;

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = Color.blue;
        }


    }

    // Use this for initialization
    void Start()
    {
        
        player = GameObject.Find("Player");
        speed = 1f;// 5.0f;
        init_dist = 10;
        rotSpeedX = 5;
        rotSpeedY = 1;
        //usePC = false;
        warpGuideToggleAvailable = true;
		//debugText = GameObject.Find("DebugTextPlayer").GetComponent<Text>();

        warpGuidePrefab = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));
        warpGuideRedPrefab = (GameObject)Resources.Load("Prefabs/Warp Guide Red", typeof(GameObject));
        isSpeedWarp = false;

        SetDebugText();

        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<Animator>().SetBool("isWarpGuideActive", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // allows us to click back into game
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }



        /* Change warp type/color */
        // blue = instantaneous warp
        if (Input.GetButtonDown("Left Bumper"))
        {
            isSpeedWarp = false;
            //GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = Color.blue;
            }

        }

        // red = superspeed warp
        if (Input.GetButtonDown("Right Bumper"))
        {
            isSpeedWarp = true;
            //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = Color.red;
            }
        }

        /* Movement */
        float walk = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe, 0, walk);

        //PlayerRotation();
        
        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        //Debug.DrawRay(transform.position, forward, Color.green);


        /* Warp Guide */
        if (Input.GetButtonDown("Fire2") && warpGuideToggleAvailable)
        {
            Vector3 pos = player.transform.position + transform.forward * init_dist;

            GameObject warpGuide;

            warpGuide = Instantiate(warpGuidePrefab, pos, transform.rotation);
            foreach (Renderer renderer in warpGuide.GetComponentsInChildren<Renderer>())
            {
                if (isSpeedWarp)
                {
                    renderer.material = altWarpGuide;
                    warpGuide.GetComponent<WarpGuideController>().SetAsSpeedWarpGuide(true);
                } else
                {
                    renderer.material = standardWarpGuide;
                    warpGuide.GetComponent<WarpGuideController>().SetAsSpeedWarpGuide(false);
                }
                
                    
            }

            warpGuideToggleAvailable = false;
            
        } else if (Input.GetButtonDown("Fire1") || (Input.GetButtonDown("Fire2") && !warpGuideToggleAvailable))
        {
            // allows warp guide to toggle on after warping or after disabling warp guide in a previous frame
            warpGuideToggleAvailable = true;
            player.GetComponent<Animator>().SetBool("isWarpGuideActive", true);
        }

        /* Warp */
        // Warping is currently handled in the warp guide script


        //SetDebugText();
    }

    void PlayerRotation()
    {
        /* === PC === */

        if (usePC)
        {
            /* Turning along x-axis */
            if (Input.GetAxis("Mouse X") < 0)
            {
                //transform.Rotate(new Vector3(0, -1 * rotSpeedX, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 1 * rotSpeedX, transform.eulerAngles.z);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                //transform.Rotate(new Vector3(0, 1 * rotSpeedX, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1 * rotSpeedX, transform.eulerAngles.z);
            }

            /* Turning along y-axis */
            if (Input.GetAxis("Mouse Y") < 0)
            {
                //transform.Rotate(new Vector3(1 * rotSpeedX, 0, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 1 * rotSpeedY, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                //transform.Rotate(new Vector3(-1 * rotSpeedX, 0, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - 1 * rotSpeedY, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
        else
        {
            /* === Controller === */

            /* Turning along x-axis */
            if (Input.GetAxis("Right Stick X") < 0)
            {
                //transform.Rotate(new Vector3(0, -1 * rotSpeedX, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 1 * rotSpeedX, transform.eulerAngles.z);
            }
            if (Input.GetAxis("Right Stick X") > 0)
            {
                //transform.Rotate(new Vector3(0, 1 * rotSpeedX, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1 * rotSpeedX, transform.eulerAngles.z);
            }

            /* Turning along y-axis */
            if (Input.GetAxis("Right Stick Y") < 0)
            {
                //transform.Rotate(new Vector3(1 * rotSpeedX, 0, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - 1 * rotSpeedY, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            if (Input.GetAxis("Right Stick Y") > 0)
            {
                //transform.Rotate(new Vector3(-1 * rotSpeedX, 0, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 1 * rotSpeedY, transform.eulerAngles.y, transform.eulerAngles.z);
            }

        }
    }

    private void FixedUpdate()
    {
        //transform.position -= Vector3.up * 9.8f;
    }

    void SetDebugText()
    {
        debugText.text = "Player: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
        debugText.text += "\nRot: (" + transform.eulerAngles.x + ", " + transform.eulerAngles.y + ", " + transform.eulerAngles.z + ")";
    }
}
