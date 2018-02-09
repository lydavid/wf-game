using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public float speed;
	public Transform target, start;
	private bool moveToA = false;
	private bool moveToB = true;
	private bool wait = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		moveBetweenPoints();
	}

	void moveBetweenPoints () {

		float step = speed * Time.deltaTime;
		if (wait){
			StartCoroutine(Pause());   
		}else{

			if (moveToB){
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}

			if (moveToA) {
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
