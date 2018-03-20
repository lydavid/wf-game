using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarpType {original, projectBulletToCamRay, noBulletMode};

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

    private Vector3 forward;

    public GameObject target;

    [SerializeField]


    AudioSource warpAudio;


    AttackManager attackManager;
    TPSPlayerController TPSPlayerController;

    List<Vector3> lastVelocity;
    int lastVelocityEntry = 0;
    int lastVelocitySize = 20;

    Ray ray;
    RaycastHit hit;

    public WarpType warpType;

    void Start()
    {
        orb = GameObject.Find("CameraOrbitX");

        magnitude = 3000;

        bulletMode = false;

        playerSettings = GetComponent<PlayerSettings>();

        rb = GetComponent<Rigidbody>();

        warpAudio = GetComponent<PlayerAudio>().warpAudio;

        attackManager = GetComponent<AttackManager>();
        TPSPlayerController = GetComponent<TPSPlayerController>();

        lastVelocity = new List<Vector3>();


        if (warpType == WarpType.original)
        {
            //bullet.transform.localPosition = new Vector3(0.0f, 1.5f, 0.0f);
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(lastVelocity.Count);
        if (lastVelocity.Count < lastVelocitySize)
        {
            lastVelocity.Add(rb.velocity);
        }
        else
        {
            lastVelocity[lastVelocityEntry] = rb.velocity;
            lastVelocityEntry += 1;
            //Debug.Log(lastVelocityEntry);
            if (lastVelocityEntry >= lastVelocitySize)
            {
                lastVelocityEntry = 0;
            }
        }
    }

    /*public float GetAverageMagnitudeOfLastVelocity()
    {
        float magnitudes = 0;

        Debug.Log(lastVelocity.Count);
        for (int i = 0; i < lastVelocitySize; i++)
        {
            Debug.Log(lastVelocity[i].magnitude);
            if (lastVelocity[i].magnitude > magnitudes)
            {
                magnitudes += lastVelocity[i].magnitude;
            }
            
        }
        return magnitudes;

    }*/

    void Update()
    {

        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);

        if (playerSettings.humanBulletOn)
        {
            // somehow when this is removed, the player is able to glitch through the floor
            forward = Vector3.Normalize(orb.transform.forward) * magnitude;//transform.TransformDirection(Vector3.forward);
                                                        // forward is a mixture of x and z
            
            //Physics.Raycast(ray);

            
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            //ray.direction = Vector3.forward * 100;//Vector3.Normalize(new Vector3())
            Debug.DrawRay(ray.origin, ray.direction, Color.cyan);

            if (Physics.Raycast(ray, out hit))
            {
                //Transform objectHit = hit.transform;


                if (warpType == WarpType.original)
                {
                    // calculate from player obj to hit
                    forward = Vector3.Normalize(new Vector3(hit.point.x - transform.position.x, hit.point.y - transform.position.y, hit.point.z - transform.position.z)) * magnitude;
                    Debug.DrawRay(transform.position, forward, Color.green);
                }
                else
                {
                    // calculate from center of viewport to hit
                    forward = Vector3.Normalize(new Vector3(hit.point.x - ray.origin.x, hit.point.y - ray.origin.y, hit.point.z - ray.origin.z)) * magnitude;
                    Debug.DrawRay(ray.origin, forward, Color.green);

                }
                //Debug.DrawRay(ray);
                //Debug.Log("I'm looking at " + hit.transform.gameObject.name);

                // interactable object
                if (hit.transform.gameObject.layer == 10)
                {
                    //Debug.Log(hit.transform.gameObject.name);
                    target = hit.transform.gameObject;

                }
                // Do something with the object that was hit by the raycast.
            }
            // now we need to change y according to the angle of the orb (it's x rot seems to rep the up/down look angle
            //Debug.Log(transform.TransformDirection(Vector3.forward));
            //Debug.Log(orb.transform.localEulerAngles);
            //forward = new Vector3(forward.x, 330, forward.y);

            //Debug.Log(forward);

            //
            


            //Debug.DrawRay(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)));
            /*Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                print("I'm looking at " + hit.transform.name);
            else
                print("I'm looking at nothing!");
            */

            /* 
            if (Input.GetButtonDown("B Button") || Input.GetMouseButtonDown(1))
            {   
 
                rb.AddForce(forward);
                body.SetActive(false);
                bullet.SetActive(true);
                bulletMode = true;
            }*/

            /* Adjust to center of locked on target */
            LockOn lockOn = GetComponent<LockOn>();
            if (lockOn.targetLockedOn)
            {
                forward = Vector3.Normalize(new Vector3(lockOn.targetCenter.x - transform.position.x, lockOn.targetCenter.y - transform.position.y, lockOn.targetCenter.z - transform.position.z)) * magnitude;
            }

        }
    }

    public void ShootMe () 
    {
        warpAudio.Play();
        /* Adjust bullet position to be exactly where this parent transform was. This will give us proper accuracy as we had computed forward using this parent transform */

        if (warpType == WarpType.original)
        {
            bullet.transform.position = transform.position;
        }
        else if (warpType == WarpType.projectBulletToCamRay)
        {

            bullet.transform.position = hit.point +
                ((Vector3.Dot(bullet.transform.position - hit.point, ray.origin - hit.point) /
                (Vector3.Dot(ray.origin - hit.point, ray.origin - hit.point))) *
                (ray.origin - hit.point));
            //Debug.Break();
        } else if (warpType == WarpType.noBulletMode)
        {
            body.transform.position = hit.point +
                ((Vector3.Dot(body.transform.position - hit.point, ray.origin - hit.point) /
                (Vector3.Dot(ray.origin - hit.point, ray.origin - hit.point))) *
                (ray.origin - hit.point));
        }

        rb.useGravity = false;
        rb.velocity = Vector3.zero;//new Vector3(1,1,1);
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(forward);

        if (warpType == WarpType.noBulletMode)
        {

        }
        else
        {
            body.SetActive(false);
            bullet.SetActive(true);
        }
        
        bulletMode = true;
        TPSPlayerController.grounded = false;

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
            // Play sound as player collided with something whilst in bullet mode
            GetComponent<PlayerAudio>().impactAudio.Play();

            if (warpType == WarpType.original)
            {
                rb.velocity = new Vector3(3, 3, 3); //stops the player from flying everywhere
            }
            else
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            

            //body.transform.position = bullet.transform.position + Vector3.up;
            if (warpType == WarpType.noBulletMode)
            {

            }
            else
            {
                bullet.SetActive(false);
                body.SetActive(true);
            }

            // only need to do this if collision was from above
            //float yPos = transform.position.y - other.transform.position.y;
            // or from side
            //float xPos = Mathf.Abs(transform.position.x - other.transform.position.x);
            //if (yPos > 0 || xPos > 0)
            //{

                // Set the player to be above ground
                //float yPos = Mathf.Abs(other.transform.position.y - transform.position.y);
                // for now we use 2 which is about the heigh of this gameObject, to prevent it from spawning through the ground after warp
                //transform.position = new Vector3(transform.position.x, Mathf.Ceil(yPos + transform.position.y + 2), transform.position.z);

            //}
            

            bulletMode = false;
            rb.useGravity = true;
            attackManager.TurnOffAttackMode();
        }
        //}
    }


}
