using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class MovingTransparentWall : MonoBehaviour {


    public GameObject target; // end point of transition
    public float speed;

    //Rigidbody rb;
    Vector3 originalPos;
    bool reached;  // indicates whether it has reached its target, if true, should go back

	// Use this for initialization
	void Start () {
        originalPos = gameObject.transform.position;
        reached = false;
        speed = 0.5f;
	}

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void Update () {
        
        if (reached)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, originalPos, speed);
            if (gameObject.transform.position == originalPos)
            {
                reached = false;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed);
            if (gameObject.transform.position == target.transform.position)
            {
                reached = true;
            }
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.tag == "Player")
        {
            // how much the character should be knocked back
            var magnitude = 5000;
            // calculate force vector
            var force = transform.position - other.transform.position;
            // normalize force vector to get direction only and trim magnitude
            force.Normalize();
            other.gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);

            Debug.Log("Yes");
        }
    }*/
}
