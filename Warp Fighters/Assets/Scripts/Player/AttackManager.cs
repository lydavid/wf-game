using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

    public bool initiatedAttack;

    bool turningOffAttackMode;
    float timeToTurnOff;
    float curTime;

	// Use this for initialization
	void Start () {
        initiatedAttack = false;
        timeToTurnOff = 0.5f;
        curTime = timeToTurnOff;
	}

    // Update is called once per frame
    void Update() {

        if (turningOffAttackMode)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0)
            {
                initiatedAttack = false;
                turningOffAttackMode = false;
                curTime = timeToTurnOff;
            }
            
            
        }

        // should match up with the velocity warp button
        if (Input.GetButtonDown("A Button") || Input.GetMouseButton(0) || Input.GetButtonDown("X Button"))
        {
            initiatedAttack = true;
            turningOffAttackMode = false; // interrupts turning off
            curTime = timeToTurnOff;
        } /*else if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            initiatedAttack = false;  // rather than using heuristics to determine when the player is no longer in attack mode, explicitly make a condition where they exit attack mode
            // it can't just been when they aren't in bulletMode cause on collision, they are already not in bulletMode
        }*/
    }

    public void TurnOffAttackMode()
    {
        turningOffAttackMode = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TurnOffAttackMode();
    }

}
