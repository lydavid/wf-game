using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltCameraController : MonoBehaviour {

    public GameObject player;
    public float damping = 1;
    Vector3 offset;

    private void Reset()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        /*float currentAngle = transform.eulerAngles.y;
        float desiredAngle = player.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);*/
        //float desiredAngle = player.transform.eulerAngles.y;
        //Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = player.transform.position + offset;//(rotation * offset);
        transform.LookAt(player.transform);
    }
}
