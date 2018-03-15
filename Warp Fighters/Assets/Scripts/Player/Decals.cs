using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decals : MonoBehaviour {

	[SerializeField]
	private GameObject WallCrackPrefab;
	private GameObject impactDecals;

	HumanBullet humanBullet;

	// Use this for initialization
	void Start () {
		impactDecals = new GameObject();
		humanBullet = GetComponent<HumanBullet>(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void drawDecal (ContactPoint point)
    {   
		int numDecals = impactDecals.transform.childCount;
		if (numDecals >= 1)
		{
			Destroy(impactDecals.transform.GetChild(0).gameObject);
			
		}
        GameObject decal = Instantiate(WallCrackPrefab, impactDecals.transform);
        decal.transform.position = point.point + (point.normal * 0.01f);
        decal.transform.forward = point.normal * -1f;
    }

	void OnCollisionEnter (Collision hit)
	{	
		if (humanBullet.bulletMode)
		{
			ContactPoint pointOfContact = hit.contacts[0];
    		drawDecal(pointOfContact);
		}
		
	}
}
