using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSguide : MonoBehaviour {

	[SerializeField]
	private Camera camera;
	private GameObject warpGuidePrefab;
	[SerializeField]
	private Material standardWarpGuide;

	// Use this for initialization
	void Start () {
		warpGuidePrefab = (GameObject)Resources.Load("Prefabs/Warp Guide", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire2"))
        {

			Vector3 mousePoint = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
			Debug.Log(mousePoint);
            GameObject warpGuide;

            warpGuide = Instantiate(warpGuidePrefab, mousePoint, transform.rotation);
            foreach (Renderer renderer in warpGuide.GetComponentsInChildren<Renderer>())
            {

				renderer.material = standardWarpGuide;
                //warpGuide.GetComponent<WarpGuideController>().SetAsSpeedWarpGuide(false);
                /*if (isSpeedWarp)
                {
                    renderer.material = altWarpGuide;
                    warpGuide.GetComponent<WarpGuideController>().SetAsSpeedWarpGuide(true);
                } else
                {
                    renderer.material = standardWarpGuide;
                    warpGuide.GetComponent<WarpGuideController>().SetAsSpeedWarpGuide(false);
                }*/
                
                    
            }    
        }
	}
}
