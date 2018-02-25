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
    float bleh = 0.00f;
    bool flag = false;

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

        EasingFunction.Ease ease = EasingFunction.Ease.EaseInCubic;
        EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);
        EasingFunction.Function dFunc = EasingFunction.GetEasingFunctionDerivative(ease);

        float value = func(0, 1, bleh);
        //Debug.Log(value);
        
        value = dFunc(0, 1, bleh);
        //Debug.Log(value);
        bleh += 0.02f;

        if (reached)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, originalPos, value);
            if (gameObject.transform.position == originalPos)
            {
                reached = false;
                bleh = 0.00f;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, value);
            if (gameObject.transform.position == target.transform.position)
            {
                reached = true;
                bleh = 0.00f;
            }
        }
    }

   /*private void OnCollisionEnter(Collision other)
    {



        if (other.gameObject.tag == "Player")
        {
            // how much the character should be knocked back
            var magnitude = 5;
            // calculate force vector
            var force = transform.position - other.transform.position;
            // normalize force vector to get direction only and trim magnitude
            force.Normalize();
            Debug.Log(force);
            Vector3 e = force * magnitude;
            Debug.Log(e);
            other.gameObject.GetComponent<Rigidbody>().AddForce(e, ForceMode.Impulse);

            Debug.Log("Yes");

        }
            
        
    }*/
}
