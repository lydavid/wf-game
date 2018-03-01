using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyMoveState { patroling, chasingPlayer, coolingOff, warpAtPlayer, waiting, warpBackToGround };


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
    public int sightRange;

    private Rigidbody rb;


    [Header("Combat")]
    public Vector3 knockBackForce;
    public float coolOffTime;

    Rigidbody selfRigidbody;

    [Header("State Machine")]
    public EnemyMoveState enemyMoveState;

    [Header("Debug")]
    int count = 0;

    public float waitTime;
    Vector3 originalPos;

    Material origMaterial;
    public Material alertedColor;
    public Material attackColor;
    public Material coolingColor;

    public bool ableToDamagePlayer; // use this to restrict enemy from hitting player multiple times in its single warp

    // Use this for initialization
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        arcSize = 30;
        sightRange = 30;
        coolOffTime = 3.0f;
        enemyMoveState = EnemyMoveState.patroling;

        selfRigidbody = GetComponent<Rigidbody>();

        origMaterial = GetComponent<Renderer>().material;

        ableToDamagePlayer = true;
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

            case EnemyMoveState.warpAtPlayer:
                WarpAtPlayer();
                break;

            case EnemyMoveState.waiting:
                Waiting();
                break;

            case EnemyMoveState.warpBackToGround:
                WarpBackToGround();
                break;

            default: break;
        }        
    }


	/* Moves enemy between start and target */
    void MoveBetweenPoints()
    {

        //transform.rotation = Quaternion.identity;

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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * sightRange;
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

                    //Debug.Log("Player hit! " + count);
                    //count += 1;

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
        GetComponent<Renderer>().material = alertedColor;

        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
        transform.LookAt(player.transform);

        if (Vector3.Distance(player.transform.position, transform.position) < sightRange)
        {
            enemyMoveState = EnemyMoveState.warpAtPlayer;
        }
    }


    /* Enemy speed warps towards player */
    void WarpAtPlayer()
    {

        originalPos = transform.position;

        float magnitude = 30.0f; // should be same as sightRange and 
        transform.LookAt(player.transform);
        Debug.Log(transform.forward * magnitude);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * magnitude, ForceMode.Impulse);
        //enemyMoveState = EnemyMoveState.coolingOff;

        // change to a waiting state while waiting for enemy to collide with player or not
        enemyMoveState = EnemyMoveState.waiting;
        waitTime = 1.5f;
    }


    void WarpBackToGround()
    {

        selfRigidbody.velocity = Vector3.zero;
        selfRigidbody.angularVelocity = Vector3.zero;
        //float magnitude = 20.0f;
        //transform.LookAt(originalPos);
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * magnitude, ForceMode.Impulse);

        if (moveToA)
        {
            transform.position = start.transform.position;

        } else
        {
            transform.position = target.transform.position;
        }
        enemyMoveState = EnemyMoveState.patroling;
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {

            if (ableToDamagePlayer)
            {

                // TODO: Actual attack animation
                // Change color of enemy to indicate enemy has attacked player
                GetComponent<Renderer>().material = attackColor;


                Debug.Log("Attacked!");

                // knockback the player and damage them
                knockBackForce = transform.forward * 20;
                player.GetComponent<Rigidbody>().AddForce(knockBackForce, ForceMode.Impulse);

                // knockback self a bit
                Vector3 selfKnockBackForce = -knockBackForce / 2;
                GetComponent<Rigidbody>().AddForce(selfKnockBackForce, ForceMode.Impulse);

                // dmg the player
                player.GetComponent<HPManager>().Damage(1);

                enemyMoveState = EnemyMoveState.coolingOff;
                coolOffTime = 3.0f;

                ableToDamagePlayer = false;

            }
        }
    }


    void Waiting()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            enemyMoveState = EnemyMoveState.patroling;
        }
    }


    /* Does nothing for a period of time to give player a breather */
    void CoolOff()
    {
        GetComponent<Renderer>().material = coolingColor;
        //player.GetComponent<Rigidbody>().AddForce();
        coolOffTime -= Time.deltaTime;
        if (coolOffTime <= 0)
        {
            enemyMoveState = EnemyMoveState.warpBackToGround;
            //enemyMoveState = EnemyMoveState.patroling; // finished cooling off after time expires
            GetComponent<Renderer>().material = origMaterial;
            ableToDamagePlayer = true;
        }
    }
}
