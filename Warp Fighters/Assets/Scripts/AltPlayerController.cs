using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AltPlayerController : MonoBehaviour {

    public GameObject player;
    public float speed;
    public int init_dist; // initial distance to spawn warp guide

    public int rotSpeed; // speed of rotating player with horizontal mouse axis

    public Text debugText;

    private GameObject warpGuide;
    

    private void Reset()
    {
        player = GameObject.Find("Player");
        speed = 15.0f;
        init_dist = 10;
        rotSpeed = 5;

        // loads prefab from Resources folder at runtime
        warpGuide = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));
    }

    // Use this for initialization
    void Start()
    {
        
        player = GameObject.Find("Player");
        speed = 15.0f;
        init_dist = 10;
        rotSpeed = 5;

        warpGuide = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));

        SetDebugText();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        /* Movement */
        float walk = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(straffe, 0, walk);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate(new Vector3(0, -1 * rotSpeed, 0));
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate(new Vector3(0, 1 * rotSpeed, 0));
        }


        /* Warp Guide */
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 pos = player.transform.position + transform.forward * init_dist;

            Instantiate(warpGuide, pos, transform.rotation);

        }

        /* Warp */
        // Warping is currently handled in the warp guide script

        SetDebugText();
    }

    void SetDebugText()
    {
        debugText.text = "Player: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
        debugText.text += "\nRot: (" + transform.eulerAngles.x + ", " + transform.eulerAngles.y + ", " + transform.eulerAngles.z + ")";
    }
}
