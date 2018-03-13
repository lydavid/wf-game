using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

	public GameObject line1;
	public GameObject line2;
	public GameObject line3;

	public GameObject player;

	private bool shown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (shown == false)
		{
			StartCoroutine(showInstructions());
			shown = true;
		}
		
	}

	void OnTriggerEnter (Collider hit) 
	{
		// move player under the terrain
		player.transform.Translate(new Vector3(0,-1,0));
	}

	IEnumerator showInstructions ()
	{
		line1.SetActive(true);
		yield return new WaitForSeconds(3f);
		line1.SetActive(false);
		line2.SetActive(true);
		yield return new WaitForSeconds(3f);
		line2.SetActive(false);
		line3.SetActive(true);
		yield return new WaitForSeconds(3f);
		line3.SetActive(false);

	}
}
