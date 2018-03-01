using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHearts : MonoBehaviour {

	public Texture heart;
	private int init_hearts;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		init_hearts = GetComponent<HPManager>().healthPoints;
	}

	void OnGUI () {
		for (int i = 0; i < init_hearts; i++) {
			GUI.DrawTexture(new Rect(i*50, 0, 50, 50), heart);
		}
	}
}
