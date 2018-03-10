using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A substitute for the velocity warp
public class HumanBullet : MonoBehaviour {

    [SerializeField]
    private Camera cam; // BTS cam attached to player

    public int magnitude;

    GameObject orb;

    public GameObject body;
    public GameObject bullet;

    public bool bulletMode;

    PlayerSettings playerSettings;

    Rigidbody rb;

    void Start()
    {
        orb = GameObject.Find("Orb");
        magnitude = 5000;

        bulletMode = false;

        playerSettings = GetComponent<PlayerSettings>();

        rb = GetComponent<Rigidbody>();
        
    }


    void Update()
    {
        if (playerSettings.humanBulletOn)
        {
            Vector3 forward = orb.transform.forward * magnitude;//transform.TransformDirection(Vector3.forward);
                                                                // forward is a mixture of x and z

            // now we need to change y according to the angle of the orb (it's x rot seems to rep the up/down look angle
            //Debug.Log(transform.TransformDirection(Vector3.forward));
            //Debug.Log(orb.transform.localEulerAngles);
            //forward = new Vector3(forward.x, 330, forward.y);

            //Debug.Log(forward);
            Debug.DrawRay(transform.position, forward, Color.green);
            //Debug.DrawRay(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)));
            /*Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                print("I'm looking at " + hit.transform.name);
            else
                print("I'm looking at nothing!");
            */
            if (Input.GetButtonDown("B Button") || Input.GetMouseButtonDown(1))
            {   
 
                rb.AddForce(forward);
                body.SetActive(false);
                bullet.SetActive(true);
                bulletMode = true;
            }

        }
    }

    /*private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity);
    }*/

    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.layer != 9 && other.gameObject.tag != "Player")
        //{
        if (bulletMode)
        {
            rb.velocity = new Vector3(3, 3, 3); //stops the player from flying everywhere
            
            bullet.SetActive(false);
            body.SetActive(true);

            // only need to do this if collision was from above
            float yPos = transform.position.y - other.transform.position.y;
            // or from side
            float xPos = Mathf.Abs(transform.position.x - other.transform.position.x);
            if (yPos > 0 || xPos > 0)
            {

                // Set the player to be above ground
                //float yPos = Mathf.Abs(other.transform.position.y - transform.position.y);
                // for now we use 2 which is about the heigh of this gameObject, to prevent it from spawning through the ground after warp
                transform.position = new Vector3(transform.position.x, Mathf.Ceil(yPos + transform.position.y + 2), transform.position.z);

            }
            
            bulletMode = false;
        }
        //}
    }


}
