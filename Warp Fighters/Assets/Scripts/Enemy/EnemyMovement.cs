using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 5.0f;  // movement speed of enemy
    public Transform start, target;  // positions to move between

    private bool moveToA = false;
    private bool moveToB = true;
    private bool wait = false;


    public bool chasingPlayer; // will be contorlled from EnemyDetection
    public bool coolingOff;

    //public enum EnemyMoveState { patroling, chasingPlayer, coolingOff };
    //public EnemyMoveState enemyMoveState;
	
	// Use this for initialization
	void Start () {
        chasingPlayer = false;
        coolingOff = false;

        //enemyMoveState = EnemyMoveState.patroling;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        /*if (coolingOff)
        {
            //ReturnToStart();
            moveBetweenPoints();

        }
        else
        {

            

        }*/
        bool enemySpotted = gameObject.GetComponent<EnemyDetection>().enemySpotted;
        if (!enemySpotted)
        {
            moveBetweenPoints();
        }



    }

    /*void ReturnToStart()
    {
        float step = speed * Time.deltaTime;
        transform.LookAt(start.transform);
        transform.position = Vector3.MoveTowards(transform.position, start.position, step);

        if (transform.position == start.position)
        {
            coolingOff = false;
        }
    }*/


	/*
	Moves enemy between points A and B
	 */
	void moveBetweenPoints () {

		float step = speed * Time.deltaTime;
		if (wait){
			StartCoroutine(Pause());   
		}else{

			if (moveToB){
				transform.LookAt(target.transform);
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}

			if (moveToA) {
				transform.LookAt(start.transform);
				transform.position = Vector3.MoveTowards(transform.position, start.position, step);
				
			}
			if (transform.position == target.position){
				moveToA = true;
				moveToB = false;
				wait = true;
				if (transform.rotation.y != 270){
					// i could use a loop to slowly roate x angles at a time
					transform.rotation = Quaternion.Euler(0, 270, 0);
				}
				
			}
			if (transform.position == start.position){
				moveToA = false;
				moveToB = true;
				wait = true;
				if (transform.rotation.y != 90){
					transform.rotation = Quaternion.Euler(0, 90, 0);
				}
			}   
		}
	}

	IEnumerator Pause(){
     	yield return new WaitForSecondsRealtime(2);
		wait = false;
	}
}
