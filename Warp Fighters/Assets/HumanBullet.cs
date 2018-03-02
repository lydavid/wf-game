using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBullet : MonoBehaviour {

    [SerializeField]
    private Camera cam; // BTS cam attached to player

    public int magnitude;

    GameObject orb;

    public GameObject body;
    public GameObject bullet;

    void Start()
    {
        orb = GameObject.Find("Orb");
        magnitude = 5000;
    }


    void Update()
    {
        Vector3 forward = orb.transform.forward * magnitude;//transform.TransformDirection(Vector3.forward);
        // forward is a mixture of x and z

        // now we need to change y according to the angle of the orb (it's x rot seems to rep the up/down look angle
        //Debug.Log(transform.TransformDirection(Vector3.forward));
        //Debug.Log(orb.transform.localEulerAngles);
        //forward = new Vector3(forward.x, 330, forward.y);

        Debug.Log(forward);
        Debug.DrawRay(transform.position, forward, Color.green);
        //Debug.DrawRay(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)));
        /*Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            print("I'm looking at " + hit.transform.name);
        else
            print("I'm looking at nothing!");
        */
        if (Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().AddForce(forward);
            body.SetActive(false);
            bullet.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 9 && other.gameObject.tag != "Player")
        {
            bullet.SetActive(false);
            body.SetActive(true);
        }
    }


}
