using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour {

	[Header("Properties")][SerializeField]
	private float speed = 10f;
	public GameObject body;
	private Vector3 centroDaTela;
	public Camera camera;

	/*
        States:
		00 = Idle
        01 = Walking
        02 = Walking Back
        03 = Walking Right
        04 = Walking Left
        */
	private int state = 0;
	//private Rigidbody rb;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//RaycastHit hit;
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Vector3 pos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(20);

		//transform.LookAt(pos);

		//float dist = 10.0f;
		/* 
		if (Physics.Raycast(ray, out hit)){
			transform.LookAt(hit.point);
		}else {
			transform.LookAt(ray.origin + ray.direction * dist);
		}*/
		//updateState();
		//movePlayer();
		/* 
		float mH = Input.GetAxis("Horizontal");
		float mV = Input.GetAxis("Vertical");
		Vector3 dir = new Vector3(mH, 0.0f, mV);
		//transform.forward = dir;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.15f);
		transform.Translate(dir * speed * Time.deltaTime);*/
		//Vector3 mousePositionVector3 = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);

		//mousePositionVector3 = Camera.main.ScreenToWorldPoint(mousePositionVector3);
		//Vector3 targetdir = mousePositionVector3 - body.transform.position;
		//body.transform.rotation= Quaternion.LookRotation(Vector3.forward,targetdir);

	}

	void updateState(){
		float mH = Input.GetAxis("Horizontal");
		float mV = Input.GetAxis("Vertical");
		if (mH < 0){ state = 4; }
		else if (mH > 0){ state = 3; }
		else if (mV < 0){ state = 2; }
		else if (mV > 0){ state = 1; }
		else { state=0; }
	}

	void movePlayer(){
		if (state == 0) { transform.Translate(0, 0, 0); }
        if (state == 1) { transform.Translate(0, 0, speed * Time.deltaTime); }
        if (state == 2) { transform.Translate(0, 0, -speed * Time.deltaTime); }
        if (state == 3) { transform.Translate(speed * Time.deltaTime, 0, 0); }
        if (state == 4) { transform.Translate(-speed * Time.deltaTime, 0, 0); }
	}


	void FixedUpdate(){


		var mousePos = Input.mousePosition;
   		mousePos.z = 10; // select distance = 10 units from the camera
		Vector3 dir = camera.ScreenToWorldPoint(mousePos);
   		Debug.Log(dir);

		body.transform.LookAt(dir);



	}
}
