using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSguide : MonoBehaviour {

	[SerializeField]
	private Camera cam; // BTS cam attached to player
	public GameObject warpGuide;

	private bool warpToggle = false;

	// distance of warpguide from player/camera, should be min 4 or 5
	private float camDist = 4f;
	private const float minCamDist = 4f;
	private const float maxCamDist = 100f;
	

	void Start () {
	
	}


	void Update () {
		if (Input.GetButtonDown("Fire2")){
			if (warpToggle){
				warpToggle = false;
			}else{
				warpToggle = true;
			}
		}
		float mw = Input.GetAxis("Mouse ScrollWheel");
		WarpGuideAnimate(mw);
        //cam.nearClipPlane
	}

	void WarpGuideAnimate(float mouseWheel){

		// Making sure dist between warp guide and player always looks ok
		if (camDist <= minCamDist){
			camDist = minCamDist;
		}
		if (camDist >= maxCamDist){
			camDist = maxCamDist;
		}

		// If warp guide on
		if (warpToggle){
			camDist += mouseWheel;
			Vector3 mousePoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDist));
			Debug.Log(mousePoint);
			warpGuide.SetActive(true);
			warpGuide.transform.position = mousePoint;
		}else{
			warpGuide.SetActive(false);
		}
	}
}
