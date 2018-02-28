using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {

    // movement
    public float speed = 5.0f;  // movement speed of enemy
    public Transform start, target;  // positions to move between

    private bool moveToA = false;
    private bool moveToB = true;
    private bool wait = false;


    public bool chasingPlayer; // will be contorlled from EnemyDetection
    public bool coolingOff;



    public GameObject player;
    public int arcSize;

    private Rigidbody rb;
    public bool enemySpotted = false;
    private bool playerThere = false;

    int count = 0;

    Vector3 knockBackForce;
    bool coolOff; // indicates that the enemy should cool off before hunting for player again
    float coolOffTime;

    // Use this for initialization
    void Start()
    {
        chasingPlayer = false;
        coolingOff = false;


        rb = player.GetComponent<Rigidbody>();
        arcSize = 30;

        coolOff = false;
        coolOffTime = 3.0f;


    }

    // Update is called once per frame
    void Update()
    {

        /*if (coolOff)
        {
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            //enemyMovement.chasingPlayer = false;
            //coolOff = false;
        }*/

        if (!coolOff)
        {


            if (!chasingPlayer)
            {


                RaycastHit hit;
                //Vector3 up = transform.TransformDirection(Vector3.up) * 100;
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
                Vector3 start = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

                /* 
                Vector3 right = Quaternion.Euler(0, 30, 0) * forward;
                Vector3 left = Quaternion.Euler(0, -30, 0) * forward;
                Debug.DrawRay(start, forward, Color.green);
                Debug.DrawRay(start, left, Color.green);
                Debug.DrawRay(start, right, Color.green);*/
                //Debug.Log("enemey spotted is: " + enemySpotted);

                for (float i = -arcSize; i <= arcSize; i += 0.5f)
                {
                    Vector3 dir = Quaternion.Euler(0, i, 0) * forward;
                    Debug.DrawRay(start, dir, Color.red);
                }


                for (float i = -arcSize; i <= arcSize; i += 0.5f)
                {

                    Vector3 dir = Quaternion.Euler(0, i, 0) * forward;

                    if (Physics.Raycast(start, dir, out hit))
                    {

                        if (hit.rigidbody == rb)
                        {

                            Debug.Log("Player hit! " + count);
                            count += 1;

                            enemySpotted = true;
                            playerThere = true;

                            chasingPlayer = true; // indicate to EnemyMovement script to stop its default movement so that this enemy can give chase
                                                                //transform.LookAt(player.transform);

                            break;
                        }
                    }

                    /*if (i == arcSize) {
                        enemySpotted = false;
                        //playerThere = false;
                    }*/
                }

                /*if (enemySpotted)
                {
                    transform.LookAt(player.transform);
                    enemySpotted = false;
                }*/





                if (!playerThere)
                {
                    enemySpotted = false;
                }

                /* 

                bool playerThere = enemySpotted;
                if (enemySpotted) {
                    for (float i = -30; i <= 30; i+=0.5f) {
                        Vector3 dir = Quaternion.Euler(0, i, 0) * forward;
                        if (Physics.Raycast(start, dir, out hit)){

                            if (hit.rigidbody == rb){
                                playerThere = true;
                                break;
                            }
                        }
                        if (i == 30) {
                            enemySpotted = false;
                            playerThere = false;
                        }
                    }


                }*/

            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            coolOffTime -= Time.deltaTime;
            if (coolOffTime <= 0)
            {
                coolOff = false; // finished cooling off after time expires
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        /*if (coolingOff)
        {
            //ReturnToStart();
            moveBetweenPoints();

        }
        else
        {

            

        }*/

        if (!enemySpotted)
        {
            moveBetweenPoints();
        }



    }

    /*
	Moves enemy between points A and B
	 */
    void moveBetweenPoints()
    {

        float step = speed * Time.deltaTime;
        if (wait)
        {
            StartCoroutine(Pause());
        }
        else
        {

            if (moveToB)
            {
                transform.LookAt(target.transform);
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }

            if (moveToA)
            {
                transform.LookAt(start.transform);
                transform.position = Vector3.MoveTowards(transform.position, start.position, step);

            }
            if (transform.position == target.position)
            {
                moveToA = true;
                moveToB = false;
                wait = true;
                if (transform.rotation.y != 270)
                {
                    // i could use a loop to slowly roate x angles at a time
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                }

            }
            if (transform.position == start.position)
            {
                moveToA = false;
                moveToB = true;
                wait = true;
                if (transform.rotation.y != 90)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(2);
        wait = false;
    }


    void ChasePlayer()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
        transform.LookAt(player.transform);




        // some condition to revert away from chasing player
        //enemyMovement.chasingPlayer = false;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {

            Debug.Log("Attacked!");

            // knockback the player and damage them
            knockBackForce = transform.forward * 20;
            player.GetComponent<Rigidbody>().AddForce(knockBackForce, ForceMode.Impulse);

            Debug.Log(knockBackForce);

            // bounce off player to give them a breather

            // or simply revert back to its original movement set
            //enemyMovement.chasingPlayer = false; // nope, this way, it will automatically redetect the player
            // instead move it into another state where it does a full turnaround and return to StartPoint A

            // consider a state machine for all of this code
            //coolOff = true;
            //enemyMovement.coolingOff = true;
            enemySpotted = false;
            chasingPlayer = false;
            coolOff = true;
            coolOffTime = 3.0f;
        }
    }
}
