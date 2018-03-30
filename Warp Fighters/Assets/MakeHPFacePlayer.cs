using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHPFacePlayer : MonoBehaviour {

    GameObject mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(mainCamera.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
