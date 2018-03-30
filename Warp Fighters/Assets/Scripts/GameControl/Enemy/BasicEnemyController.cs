using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyMoveState { patroling, chasingPlayer, coolingOff, warpAtPlayer, waiting, warpBackToGround, flyingToDeath, waitToDestroy };

/*
 * Guard: Stands still, if player passes through its sight, it will warp attack them, then return. Take them out from behind or beside them.
 * Patrol: Moves back and forth between two points, engaging player if they enter its sight. Dodge their attack and counter attack them while they cool off.
 * Boss: ???
 * 
 */
public enum EnemyType { a_guard, b_patrol, c_boss };


public class BasicEnemyController : MonoBehaviour {

    [Header("Type")]
    public EnemyType enemyType;


    [Header("Movement")]
    [Tooltip("Movement speed of the enemy.")]
    public float speed = 4.0f;
    [Tooltip("Distance enemy will move up to before turning back.")]
    public float patrolDistance;
    [Tooltip("Time in seconds of how long to wait before turning and moving in the other direction.")]
    public float pauseTime = 0.5f;

    Transform start, target;  // positions to move between
    bool moveToA = false;
    bool moveToB = true;
    bool wait = false;


    [Header("Detection")]
    public int arcSize = 30;
    public int sightRange = 30;

    GameObject player;


    [Header("Combat")]
    public float coolOffTime = 3.0f;
    public int healthPoints = 1;

    public Vector3 knockBackForce;
    Rigidbody selfRigidbody;


    [Header("State Machine")]
    public EnemyMoveState enemyMoveState;


    [Header("Debug")]
    //int count = 0;
    float waitTime;
    Vector3 originalPos;


    /*[Header("Materials")]
    //Material origMaterial;
    public Material alertedColor;  // orange when it's persuing the player
    public Material attackColor;  // blue when it's dealing dmg to the player
    public Material damagedColor; // red when it's receiving dmg from the player
    */

    [Header("Flags")]
    public bool ableToDamagePlayer; // use this to restrict enemy from hitting player multiple times in its single warp
    public bool cannotMove;
    public bool initiatedAttack;
    public bool ableToBeDamaged;


    // for guard type only
    Vector3 originalPosition;
    Quaternion originalRotation;

    [Header("Animations")]
    Animator animator; // enemy movement animations

    EnemyHPDisplay enemyHPDisplay;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Create transform where it is standing, this will the the start
        start = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        // create another transform some distance away, this will be the target
        Vector3 newPosition = transform.TransformPoint(Vector3.forward * patrolDistance);
        target = Instantiate(new GameObject(), newPosition, transform.rotation).transform;

        enemyMoveState = EnemyMoveState.patroling;

        selfRigidbody = GetComponent<Rigidbody>();

        //origMaterial = GetComponent<Renderer>().material;

        ableToDamagePlayer = true;
        initiatedAttack = false;
        ableToBeDamaged = true;

        // for guard type only
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        animator = GetComponent<Animator>();


        // for boss only
        if (enemyType == EnemyType.c_boss)
        {
            healthPoints = 5;
        }

        enemyHPDisplay = GetComponent<EnemyHPDisplay>();
        enemyHPDisplay.SetUpHPSprites();
    }


    // Update is called once per frame
    void Update()
    {
        // State machine to control enemy actions
        switch (enemyMoveState)
        {
            case EnemyMoveState.patroling:

                if (enemyType != EnemyType.a_guard)
                {
                    if (wait)
                    {
                        animator.SetBool("Patrol", false);
                    }
                    else
                    {
                        animator.SetBool("Patrol", true);
                    }
                    MoveBetweenPoints();
                    LookForPlayer();

                } else
                {
                    
                    LookForPlayer();
                }
                break;

            case EnemyMoveState.chasingPlayer:
                animator.SetBool("Warp", true);
                ChasePlayer();
                break;

            case EnemyMoveState.coolingOff:
                animator.SetBool("Warp", false);
                CoolOff();
                break;

            case EnemyMoveState.warpAtPlayer:
                animator.SetBool("Warp", true);
                WarpAtPlayer();
                break;

            case EnemyMoveState.waiting:
                Waiting();
                break;

            case EnemyMoveState.warpBackToGround:
                WarpBackToGround();
                break;

            case EnemyMoveState.waitToDestroy:
                WaitToDestroy();
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
            //animator.SetBool("Patrol", false);
            StartCoroutine(Pause());
        }
        else
        {

            if (moveToB)
            {
                //transform.LookAt(target.transform);
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            } else if (moveToA)
            {
                //transform.LookAt(start.transform);
                transform.position = Vector3.MoveTowards(transform.position, start.position, step);

            }

            if (Vector3.Distance(transform.position, target.position) < 1)//(transform.position == target.position) // careful: computing Distance is much slower than using squares, cause it uses sqrt
            {
                moveToA = true;
                moveToB = false;
                wait = true;
                if (transform.rotation.y != 270)
                {
                    // i could use a loop to slowly roate x angles at a time
                    //transform.rotation = Quaternion.Euler(0, 270, 0);
                    //transform.LookAt(start.transform);
                }

            } else if (Vector3.Distance(transform.position, start.position) < 1)//(transform.position == start.position)
            {
                moveToA = false;
                moveToB = true;
                wait = true;
                if (transform.rotation.y != 90)
                {
                    //transform.rotation = Quaternion.Euler(0, 90, 0);
                    //transform.LookAt(target.transform);
                }
                
            }
        }
    }


    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        wait = false;

        if (moveToA)
        {
            transform.LookAt(start.transform);
        } else
        {
            transform.LookAt(target.transform);
        }
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

                if (hit.transform.gameObject.tag == "Player")
                {

                    if (enemyType != EnemyType.a_guard)
                    {
                        enemyMoveState = EnemyMoveState.chasingPlayer;
                    } else
                    {
                        enemyMoveState = EnemyMoveState.warpAtPlayer;
                    }

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
        //GetComponent<Renderer>().material = alertedColor;
        //ChangeColorOfChildren(alertedColor);

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
        initiatedAttack = true;

        //originalPos = transform.position;

        float magnitude = 30.0f; // should be same as sightRange
        transform.LookAt(player.transform);
        Debug.Log(transform.forward * magnitude);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * magnitude, ForceMode.Impulse);
        //Debug.Break();

        // change to a waiting state while waiting for enemy to collide with player or not
        enemyMoveState = EnemyMoveState.waiting;
        waitTime = 1.5f;
    }


    // Teleports back to either start or target position depending on which one it reached last
    void WarpBackToGround()
    {
        //ChangeColorOfChildren(origMaterial);
        // Make it stop flying around
        selfRigidbody.velocity = Vector3.zero;
        selfRigidbody.angularVelocity = Vector3.zero;
        //float magnitude = 20.0f;
        //transform.LookAt(originalPos);
        //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * magnitude, ForceMode.Impulse);

        if (enemyType != EnemyType.a_guard)
        {
            // Non-guard enemies will head back towards their patrol route
            if (moveToA)
            {
                //transform.position = target.transform.position;
                transform.LookAt(start);

            }
            else
            {
                //transform.position = start.transform.position;
                transform.LookAt(target);
            }

        } else
        {
            // Guard enemies warps at you, then teleports back to their starting position
            transform.position = originalPosition; 
            transform.rotation = originalRotation;
        }

        /*float step = speed * Time.deltaTime;
        transform.LookAt(originalPos);
        transform.position = Vector3.MoveTowards(transform.position, originalPos, step);

        if (transform.position == originalPos)
        {

            enemyMoveState = EnemyMoveState.patroling;
        }*/
        enemyMoveState = EnemyMoveState.patroling;
    }


    // differentiate whether this enemy is attacking the player or being attacked by the player
    // whoever initiates attack with deal dmg to the other
    // if both initiates attack, they should bounce off
    private void OnCollisionEnter(Collision other)
    {

        

        if (other.gameObject.tag == "Player")
        {

            if (initiatedAttack && !player.GetComponent<AttackManager>().initiatedAttack) // and player did not
            {



                if (ableToDamagePlayer)
                {

                    // TODO: Actual attack animation
                    // Change color of enemy to indicate enemy has attacked player
                    //GetComponent<Renderer>().material = attackColor;
                    //ChangeColorOfChildren(attackColor);


                    Debug.Log("Attacked!");

                    // knockback the player and damage them
                    knockBackForce = GetComponent<Rigidbody>().velocity;//transform.forward * 20;
                    player.GetComponent<Rigidbody>().AddForce(knockBackForce, ForceMode.Impulse);

                    // knockback self a bit
                    Vector3 selfKnockBackForce = -knockBackForce / 2;
                    GetComponent<Rigidbody>().AddForce(selfKnockBackForce, ForceMode.Impulse);

                    // dmg the player
                    player.GetComponent<HPManager>().Damage(1);

                    // change state
                    enemyMoveState = EnemyMoveState.coolingOff;
                    coolOffTime = 3.0f;

                    ableToDamagePlayer = false;
                    initiatedAttack = false;

                }


            }
            else if (player.GetComponent<AttackManager>().initiatedAttack && !initiatedAttack)
            {
                if (ableToBeDamaged)
                {
                    // knockback self
                    Vector3 selfKnockBackForce = player.GetComponent<Rigidbody>().velocity;
                    GetComponent<Rigidbody>().AddForce(selfKnockBackForce, ForceMode.Impulse);

                    // knockback player a bit
                    Vector3 playerKnockBackForce = -selfKnockBackForce / 2;
                    player.GetComponent<Rigidbody>().AddForce(playerKnockBackForce, ForceMode.Impulse);

                    enemyMoveState = EnemyMoveState.coolingOff;
                    coolOffTime = 3.0f;
                    Damage();
                    ableToBeDamaged = false;
                }
            }
            else
            {
                // otherwise both bounce back without damage

                // Knockback self
                Vector3 selfKnockBackForce = player.GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().AddForce(selfKnockBackForce, ForceMode.Impulse);

                // knockback player
                Vector3 playerKnockBackForce = GetComponent<Rigidbody>().velocity;
                player.GetComponent<Rigidbody>().AddForce(playerKnockBackForce, ForceMode.Impulse);

                enemyMoveState = EnemyMoveState.coolingOff;
                coolOffTime = 3.0f;

                initiatedAttack = false;
                player.GetComponent<AttackManager>().initiatedAttack = false;

            }

        } else
        {
            if (enemyMoveState == EnemyMoveState.flyingToDeath)// && other.gameObject.layer != 9)
            {
                ExplodeOnImpact();
            }
        }
    }


    void Waiting()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            //GetComponent<Renderer>().material = origMaterial;
            //ChangeColorOfChildren(origMaterial);


            //enemyMoveState = EnemyMoveState.patroling;
            enemyMoveState = EnemyMoveState.coolingOff;
        }
    }


    /* Does nothing for a period of time to give player a breather */
    void CoolOff()
    {
        //GetComponent<Renderer>().material = origMaterial;



        //ChangeColorOfChildren(origMaterial);


        //player.GetComponent<Rigidbody>().AddForce();
        coolOffTime -= Time.deltaTime;
        if (coolOffTime <= 0)
        {
            enemyMoveState = EnemyMoveState.warpBackToGround;
            //enemyMoveState = EnemyMoveState.patroling; // finished cooling off after time expires
            //GetComponent<Renderer>().material = origMaterial;
            ableToDamagePlayer = true;
            ableToBeDamaged = true;
        }
    }


    void Damage()
    {
        healthPoints -= 1;
        //ChangeColorOfChildren(damagedColor);

        // Remove an HP sprite above head
        enemyHPDisplay.ChangeHPSprites(healthPoints);

        // let's delay it from dying until it bounces into something
        // then make it explode into triangles
        if (healthPoints <= 0)
        {
            //Destroy(gameObject);
            enemyMoveState = EnemyMoveState.flyingToDeath;
        }
    }


    void ExplodeOnImpact()
    {

        // Give player an additional charge
        player.GetComponent<WarpLimiter>().GainMaxChargeAndRefill();

        waitTime = 3.0f;
        GetComponent<MeshExplosion>().SplitMesh(waitTime, 50);
        
        enemyMoveState = EnemyMoveState.waitToDestroy;
    }


    void WaitToDestroy()
    {
        // Does nothing but await its fate.
    }
    

    /*void ChangeColorOfChildren(Material newMat)
    {
        foreach (Renderer ren in GetComponentsInChildren<Renderer>())
        {
            ren.material = newMat;
        }

    }*/

}
