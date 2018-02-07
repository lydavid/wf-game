using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AltPlayerController : MonoBehaviour {

    public GameObject player;
    public float speed;
    public int init_dist; // initial distance to spawn warp guide

    public Text debugText;

    private GameObject warpGuide;
    

    private void Reset()
    {
        player = GameObject.Find("Player");
        speed = 15.0f;
        init_dist = 10;

        warpGuide = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));
    }

    // Use this for initialization
    void Start()
    {
        
        player = GameObject.Find("Player");
        speed = 15.0f;
        init_dist = 10;

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


        /* Warp Guide */
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 pos = player.transform.position + new Vector3(0, 0, init_dist);

            Instantiate(warpGuide, pos, Quaternion.identity);

        }




        /* Warp */


        SetDebugText();
    }

    void SetDebugText()
    {
        debugText.text = "Player: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
        debugText.text += "\nRot: (" + transform.eulerAngles.x + ", " + transform.eulerAngles.y + ", " + transform.eulerAngles.z + ")";
    }
}
