﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSguide : MonoBehaviour {

	[SerializeField]
	private Camera cam; // BTS cam attached to player
	public GameObject warpGuide;
	private bool warpToggle = false;
	private int warpState = 0; // 0 = instant warp, 1 = velocity warp

	// distance of warpguide from player/camera, should be min 4 or 5
	private float curCamDist = 4f;
	private const float minCamDist = 3f;
	private const float maxCamDist = 100f;
	

	void Start () {
	
	}


	void Update () {
		UpdateStates();
		//float mw = Input.GetAxis("Mouse ScrollWheel");
		float mw = Input.GetAxis("D-Pad Y Axis");
		//Debug.Log("dpad: " + Input.GetAxis("D-Pad Y Axis"));
		WarpGuideAnimate(mw);
		
	}

	void FixedUpdate () {
		Warp();
	}

	void UpdateStates () {
		
		if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("Left Stick Click")) {
			if (warpToggle) {
				warpToggle = false;
			} else {
				warpToggle = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			warpState = 0;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			warpState = 1;
		}
	}

	void Warp () { // Handles which warp when warp key pressed

		if (warpToggle == true) {

			if (Input.GetButtonDown("A Button")) {
				InstantWarp();
			}
			if (Input.GetButtonDown("B Button")) {
				VelocityWarp();
			}
		}


		if (Input.GetButtonDown("Fire1") ) {

			if (warpToggle == true) {

				if (warpState == 0) {
					InstantWarp();
				} else {
					VelocityWarp();
				}				
			}
			
		}
	}

	void InstantWarp () {
		transform.position = warpGuide.transform.position;
		warpGuide.SetActive(false);
		warpToggle = false;
	}

	void VelocityWarp () {
		Vector3 dir = (warpGuide.transform.position - transform.position).normalized;
		//GetComponent<Rigidbody>().AddForce(dir * 10f);
        GetComponent<Rigidbody>().velocity = dir * 50f;
		//GetComponent<Rigidbody>().velocity = 10*transform.forward;
		warpGuide.SetActive(false);
		warpToggle = false;
	}

	void WarpGuideAnimate(float mouseWheel) {

		// Making sure dist between warp guide and player always between min and max
		//Mathf.Clamp(camDist, minCamDist, maxCamDist);
		if (curCamDist <= minCamDist) {
			curCamDist = minCamDist;
		}
		if (curCamDist >= maxCamDist) {
			curCamDist = maxCamDist;
		}

		// If warp guide on
		if (warpToggle) {
			curCamDist += mouseWheel;
			Vector3 mousePoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
																	Input.mousePosition.y, 
																	curCamDist));

			
									
			//Debug.Log(mousePoint);
			warpGuide.SetActive(true);
			bool guideHit = warpGuide.GetComponentInChildren<WarpGuideCollide>().guideHit;
			// edit with collision contact point
			if (!guideHit){
				warpGuide.transform.position = mousePoint;
			} else {
				warpGuide.transform.position = warpGuide.transform.position;
				if (mouseWheel < 0) {
					warpGuide.transform.position = mousePoint;
				}
			}
			RotateWarpGuide();
		} else {
			warpGuide.SetActive(false);
		}
	}

	private void RotateWarpGuide() {
		Quaternion rotation = Quaternion.Euler(new Vector3(warpGuide.transform.rotation.eulerAngles.x, 
															transform.rotation.eulerAngles.y, 
															warpGuide.transform.rotation.eulerAngles.z));
		warpGuide.transform.rotation = rotation;
	}

}
