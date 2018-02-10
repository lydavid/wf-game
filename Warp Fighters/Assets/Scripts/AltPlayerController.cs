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

    [Range(1, 10)]
    public float jumpVelocity;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody rb;

    public Text debugText;

    private GameObject warpGuidePrefab; // stores location of prefab
    private GameObject warpGuideRedPrefab; // stores prefab of red version
    private bool isSpeedWarp;

    Vector3 angle;

    private void Reset()
    {
        player = GameObject.Find("Player");
        speed = 15.0f;
        init_dist = 10;
        rotSpeedX = 5;
        rotSpeedY = 1;

		debugText = GameObject.Find("DebugTextPlayer").GetComponent<Text>();


        // loads prefab from Resources folder at runtime
        warpGuidePrefab = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));
        warpGuideRedPrefab = (GameObject)Resources.Load("Prefabs/Warp Guide Red", typeof(GameObject));
        isSpeedWarp = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        angle = transform.eulerAngles;
    }

    // Use this for initialization
    void Start()
    {
        
        player = GameObject.Find("Player");
        speed = 15.0f;
        init_dist = 10;
        rotSpeedX = 5;
        rotSpeedY = 1;

		debugText = GameObject.Find("DebugTextPlayer").GetComponent<Text>();

        warpGuidePrefab = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));
        warpGuideRedPrefab = (GameObject)Resources.Load("Prefabs/Warp Guide Red", typeof(GameObject));
        isSpeedWarp = false;

        SetDebugText();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        /* Change warp type/color */
        // blue = instantaneous warp
        if (Input.GetKeyDown("1"))
        {
            isSpeedWarp = false;
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            
        }

        // red = superspeed warp
        if (Input.GetKeyDown("2"))
        {
            isSpeedWarp = true;
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }

        /* Movement */
        float walk = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(straffe, 0, walk);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }


        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        /* Turning along x-axis */
        if (Input.GetKey("3"))
        {
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
        }


        /* Turning along y-axis */
        if (Input.GetKey("4"))
        {
            
            

            if (Input.GetAxis("Mouse Y") < 0)
            {
                //transform.Rotate(new Vector3(1 * rotSpeedX, 0, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 1 * rotSpeedY, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            { 
                //transform.Rotate(new Vector3(-1 * rotSpeedX, 0, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x -1 * rotSpeedY, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);


        /* Warp Guide */
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 pos = player.transform.position + transform.forward * init_dist;

            GameObject warpGuide;

            if (isSpeedWarp)
            {
                warpGuide = Instantiate(warpGuideRedPrefab, pos, transform.rotation);
            } else
            {
                warpGuide = Instantiate(warpGuidePrefab, pos, transform.rotation);
            }

            
        }

        /* Warp */
        // Warping is currently handled in the warp guide script

        /* Implement Player gravity */
        UseGravity();

        SetDebugText();
    }

    // Gravity script here
    void UseGravity()
    {
        
    }

    void SetDebugText()
    {
        debugText.text = "Player: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
        debugText.text += "\nRot: (" + transform.eulerAngles.x + ", " + transform.eulerAngles.y + ", " + transform.eulerAngles.z + ")";
    }
}
