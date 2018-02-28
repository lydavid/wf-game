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

    public enum EnemyMoveState { patroling, chasingPlayer, coolingOff };
    public EnemyMoveState enemyMoveState;


    // Use this for initialization
    void Start()
    {
        //chasingPlayer = false;
        //coolingOff = false;


        rb = player.GetComponent<Rigidbody>();
        arcSize = 30;

        //coolOff = false;
        coolOffTime = 3.0f;

        enemyMoveState = EnemyMoveState.patroling;
    }

    // Update is called once per frame
    void Update()
    {

        switch (enemyMoveState)
        {
            case EnemyMoveState.patroling:
                MoveBetweenPoints();
                LookForPlayer();
                break;

            case EnemyMoveState.chasingPlayer:
                ChasePlayer();
                break;

            case EnemyMoveState.coolingOff:
                CoolOff();
                break;

            default: break;
        }
        // put everything in lateupdate and a statemachine in Update



        
    }



    void LookForPlayer()
    {

        RaycastHit hit;
        //Vector3 up = transform.TransformDirection(Vector3.up) * 100;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Vector3 start = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

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

                    //enemySpotted = true;
                    //playerThere = true;

                    enemyMoveState = EnemyMoveState.chasingPlayer;

                    break;
                }
            }


        }


        /*if (!playerThere)
        {
            enemySpotted = false;
        }*/


    }

    void CoolOff()
    {
        coolOffTime -= Time.deltaTime;
        if (coolOffTime <= 0)
        {
            enemyMoveState = EnemyMoveState.patroling; // finished cooling off after time expires
        }
    }

    /*
	Moves enemy between points A and B
	 */
    void MoveBetweenPoints()
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
            //enemySpotted = false;
            //chasingPlayer = false;
            //coolOff = true;
            enemyMoveState = EnemyMoveState.coolingOff;
            coolOffTime = 3.0f;
        }
    }
}
