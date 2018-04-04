using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

	GameObject player;
	Rigidbody playerRB;

	//public int distance;

    public int strength = 1;

    Animator animator;
    AudioSource bounceAudio;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerRB = player.GetComponent<Rigidbody>();
		//distance = 700;

        animator = transform.GetComponent<Animator>();
        bounceAudio = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(playerRB.velocity.magnitude);
	}

	void OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.tag == "Player" && playerRB.velocity.magnitude > 6)
		{
            //Vector3 curVelocity = playerRB.velocity;
            //Debug.Log(playerRB.velocity.magnitude);

            //Debug.Log(playerRB.velocity);
            // float magnitude = playerRB.velocity.magnitude;

            //playerRB.velocity = Vector3.zero;
            //playerRB.angularVelocity = Vector3.zero;
            //playerRB.AddForce((transform.up + (0.03f * curVelocity)) * distance );

            //playerRB.velocity = new Vector3(0, Mathf.Sqrt(Mathf.Pow(playerRB.velocity.x, 2) + Mathf.Pow(playerRB.velocity.z, 2)) * strength, 0);
            //animator.SetBool("PlayerTouched", true);
            float forceOfLaunch = Mathf.Clamp(playerRB.velocity.magnitude * strength, 0f, 60f);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("LaunchPadLift"))
            {
                bounceAudio.Play();
                animator.Play("LaunchPadLift");
                //playerRB.velocity = new Vector3(0, playerRB.velocity.magnitude * strength, 0);
                playerRB.AddForce(new Vector3(0, forceOfLaunch, 0), ForceMode.Impulse);
            }
        }
	}
}
