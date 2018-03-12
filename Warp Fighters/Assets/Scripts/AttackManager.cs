using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

    public bool initiatedAttack;

	// Use this for initialization
	void Start () {
        initiatedAttack = false;
	}

    // Update is called once per frame
    void Update() {

        // should match up with the velocity warp button
        if (Input.GetButtonDown("A Button") || Input.GetMouseButton(0))
        {
            initiatedAttack = true;
        } else if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            initiatedAttack = false;
        }
    }
}
