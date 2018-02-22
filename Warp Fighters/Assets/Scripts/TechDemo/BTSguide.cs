using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSguide : MonoBehaviour {

	[SerializeField]
	private Camera cam; // BTS cam attached to player
	public GameObject warpGuide;

	private bool warpToggle = false;

	// distance of warpguide from player/camera, should be min 4 or 5
	private float curCamDist = 4f;
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

		// Making sure dist between warp guide and player always between min and max
		//Mathf.Clamp(camDist, minCamDist, maxCamDist);
		if (curCamDist <= minCamDist){
			curCamDist = minCamDist;
		}
		if (curCamDist >= maxCamDist){
			curCamDist = maxCamDist;
		}



		// If warp guide on
		if (warpToggle){
			curCamDist += mouseWheel;
			Vector3 mousePoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, curCamDist));
			//Debug.Log(mousePoint);
			warpGuide.SetActive(true);
			warpGuide.transform.position = mousePoint;
			RotateWarpGuide();
		}else{
			warpGuide.SetActive(false);
		}
	}

	private void RotateWarpGuide(){
		Quaternion rotation = Quaternion.Euler(new Vector3(warpGuide.transform.rotation.eulerAngles.x, 
															transform.rotation.eulerAngles.y, 
															warpGuide.transform.rotation.eulerAngles.z));
		warpGuide.transform.rotation = rotation;
	}


}
