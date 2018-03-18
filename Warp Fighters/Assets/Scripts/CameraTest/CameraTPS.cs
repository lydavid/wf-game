using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTPS : MonoBehaviour {


	public GameObject player;
	public Transform camTransform;
	public float distance = 6.0f;
	public float sensitivity = 200.0f;
	private float h, v = 0.0f;
	private bool clipped = false;

	private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		camTransform = transform;
    }

    private void Update()
    {
		h += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        v += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

		h += Input.GetAxis("Right Stick X") * sensitivity * Time.deltaTime;
        v += Input.GetAxis("Right Stick Y") * sensitivity * Time.deltaTime;

		v = Mathf.Clamp(v, -70f, 70f);
		Debug.Log("From cam: "+RayCamToPlayer());
		Debug.Log("From player: "+RayPlayerToCam());
    }

    private void LateUpdate()
    {
		Vector3 dir = new Vector3(0, 0, -distance);
		Quaternion rotation = Quaternion.Euler(v, h, 0);
		camTransform.position = player.transform.position + rotation * dir;
		camTransform.LookAt(player.transform.position);
    }


	/*Raycast from camera to player
		returns true iff ray hits player obj */
	private bool RayCamToPlayer ()
	{
		RaycastHit hit;
		Vector3 start = transform.position;
		Vector3 dir = (player.transform.position - camTransform.position);

		Debug.DrawRay(start, dir, Color.green);

		if (Physics.Raycast(start, dir, out hit))
		{
			if (hit.transform.gameObject.tag == "Player")
			{
				return true;
			} 
		}
		return false;
	}

	/*Raycast from player to camera
		returns true iff ray hits camera obj */
	private bool RayPlayerToCam ()
	{
		RaycastHit hit;
		Vector3 start = player.transform.position;
		Vector3 dir = (camTransform.position - player.transform.position);

		Debug.DrawRay(start, dir, Color.red);

		if (Physics.Raycast(start, dir, out hit))
		{
			if (hit.transform.gameObject.tag == "MainCamera")
			{
				return true;
			} 
		}
		return false;
	}
}