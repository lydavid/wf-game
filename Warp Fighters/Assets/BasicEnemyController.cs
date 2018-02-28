using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyMoveState { patroling, chasingPlayer, coolingOff };


public class BasicEnemyController : MonoBehaviour {


    [Header("Movement")]
    public float speed = 5.0f;  // movement speed of enemy
    public Transform start, target;  // positions to move between

    private bool moveToA = false;
    private bool moveToB = true;
    private bool wait = false;


    [Header("Detection")]
    public GameObject player;
    public int arcSize;

    private Rigidbody rb;


    [Header("Combat")]
    public Vector3 knockBackForce;
    public float coolOffTime;

    [Header("State Machine")]
    public EnemyMoveState enemyMoveState;

    [Header("Debug")]
    int count = 0;


    // Use this for initialization
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        arcSize = 30;
        coolOffTime = 3.0f;
        enemyMoveState = EnemyMoveState.patroling;
    }


    // Update is called once per frame
    void Update()
    {
        // State machine to control enemy actions
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
    }


	/* Moves enemy between start and target */
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


    /* Detect player with a series of raycast in an arc shape */
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
                    enemyMoveState = EnemyMoveState.chasingPlayer;

                    Debug.Log("Player hit! " + count);
                    count += 1;

                    break;
                }
            }
        }
    }


    /* Persue the player until it hits the player or the player hits it 
       perhaps there should be another condition to stop persuit like if player moves out of its zone
     */
    void ChasePlayer()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
        transform.LookAt(player.transform);
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

            enemyMoveState = EnemyMoveState.coolingOff;
            coolOffTime = 3.0f;
        }
    }


    /* Does nothing for a period of time to give player a breather */
    void CoolOff()
    {
        coolOffTime -= Time.deltaTime;
        if (coolOffTime <= 0)
        {
            enemyMoveState = EnemyMoveState.patroling; // finished cooling off after time expires
        }
    }
}
