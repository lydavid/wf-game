using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

	public GameObject player;
    public int arcSize;

	private Rigidbody rb;
	public bool enemySpotted = false;
	private bool playerThere = false;

    int count = 0;
    EnemyMovement enemyMovement;
    Vector3 knockBackForce;
    bool coolOff; // indicates that the enemy should cool off before hunting for player again

    // Use this for initialization
    void Start () {
		rb = player.GetComponent<Rigidbody>();
        arcSize = 30;
        enemyMovement = GetComponent<EnemyMovement>();
        coolOff = false;
        

    }
	
	// Update is called once per frame
	void Update () {

        /*if (coolOff)
        {
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            //enemyMovement.chasingPlayer = false;
            //coolOff = false;
        }*/

        if (!enemyMovement.coolingOff)
        {


            if (!enemyMovement.chasingPlayer)
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

                            enemyMovement.chasingPlayer = true; // indicate to EnemyMovement script to stop its default movement so that this enemy can give chase
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
    }


    void ChasePlayer()
    {
        float step = enemyMovement.speed * Time.deltaTime;
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
            enemyMovement.coolingOff = true;
            enemySpotted = false;
            enemyMovement.chasingPlayer = false;
        }
    }
}
