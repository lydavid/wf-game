using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour {

    //public bool buttonPressed;

    public GameObject door; // make sure that a corresponding door is the child of this gameobject

    //Animator animator;
    Animator anim;
    public Animator doorAnim;

    AudioSource buttonPressAudio;
    AudioSource doorOpenCloseAudio;

	// Use this for initialization
	void Start () {
        //buttonPressed = false;
        //animator = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        doorAnim = doorAnim.GetComponent<Animator>();

        //door = gameObject.transform.GetChild(0).gameObject; // we assume that door is the one and only child of this button object
        buttonPressAudio = GetComponent<AudioSource>();
        doorOpenCloseAudio = door.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //gameObject.GetComponent<Animator>().Play("ButtonPressAnimation");
            //anim.SetBool("ButtonPressed", true);
            //doorAnim.SetBool("DoorOpen", true);

            // Prevent audio from playing again before animation finishes
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("ButtonDownUp")) {
                anim.Play("ButtonDownUp"); // Button goes down then back up after some time (3s total)
                buttonPressAudio.Play();
            }

            if (!doorAnim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenClose"))
            {
                doorAnim.Play("DoorOpenClose"); // Door swings open then closes after some time (3s total)
                doorOpenCloseAudio.Play();
            }

            //buttonPressed = true;
            //animator.SetBool("buttonPressed", true);
            //door.SetActive(false);
        }
    }

    /*void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("ButtonPressed", false);
            doorAnim.SetBool("DoorOpen", false);
            //gameObject.GetComponent<Animator>().Play("ButtonPressAnimation");
            Debug.Log("exit");
            //buttonPressed = false;
            //animator.SetBool("buttonPressed", false);
            //door.SetActive(true);
        }
    }*/
}
