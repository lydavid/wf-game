

using UnityEngine;
using System.Collections;

public class TPSPlayerController : MonoBehaviour {

    public float speed = 3.0f;
	public float gravity = 1.0f;

    //This variable indicates the current state of character.
    
    private int state;

    //Define the turning speed.
    private float turnSpeed = 4.0f;
    

    private float horizontal;

    private Animator animacao;


    void Start ()
    {
        animacao = GetComponentInChildren<Animator>();
        state = 0;
        horizontal = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // allows us to click back into game
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Control();
        MovePerson();
        AnimatePerson();
		Gravity ();
    }

    private void AnimatePerson()
    {
        //animacao.SetInteger("Estado", state);
    }

    private void Control()
    {
        /*
        States:
        01 = Walking
        02 = Running
        03 = Walking Back
        04 = Walking Right
        05 = Walking Left
        */
        
        float xAxis = Input.GetAxis("Left Stick X");
        float yAxis = Input.GetAxis("Left Stick Y");

        if (xAxis == 0 && yAxis == 0) {
            state = 0;
        }

        if (xAxis < 0 && Mathf.Abs(xAxis) > Mathf.Abs(yAxis)) {
            state = 5;
        }

        if (yAxis < 0 && Mathf.Abs(yAxis) > Mathf.Abs(xAxis)) {
            state = 1;
        }

        if (xAxis > 0 && Mathf.Abs(xAxis) > Mathf.Abs(yAxis)) {
            state = 4;
        }

        if (yAxis > 0 && Mathf.Abs(yAxis) > Mathf.Abs(xAxis)) {
            state = 3;
        }


        if (Input.GetKeyDown("w"))
        {
            state = 1;
        }
        if (Input.GetKeyUp("w") && state == 1)
        {
            state = 0;
            if (Input.GetKey("s")) { state = 3; }
            if (Input.GetKey("a")) { state = 5; }
            if (Input.GetKey("d")) { state = 4; }
        }
        if (Input.GetKeyUp("w") && state == 2)
        {
            state = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && state == 1)
        {
            state = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && state == 2) { state = 1; }
                
        if (Input.GetKeyDown("s"))
        {
            state = 3;
        }
        if (Input.GetKeyUp("s") && state == 3)
        {
            state = 0;
            if (Input.GetKey("a")) { state = 5; }
            if (Input.GetKey("d")) { state = 4; }
            if (Input.GetKey("w")) { state = 1; }
        }

        if (Input.GetKeyDown("d"))
        {
            state = 4;
        }
        if (Input.GetKeyUp("d") && state == 4)
        {
            state = 0;
            if (Input.GetKey("s")) { state = 3; }
            if (Input.GetKey("a")) { state = 5; }
            if (Input.GetKey("w")) { state = 1; }

        }

        if (Input.GetKeyDown("a"))
        {
            state = 5;
        }
        if (Input.GetKeyUp("a") && state == 5)
        {
            state = 0;
            if (Input.GetKey("s")) { state = 3; }
            if (Input.GetKey("d")) { state = 4; }
            if (Input.GetKey("w")) { state = 1; }
        }

    }

    private void MovePerson()
    {
        //var mouseHorizontal = Input.GetAxis("Mouse X");
        var mouseHorizontal = Input.GetAxis("Right Stick X");
        horizontal = (horizontal + turnSpeed * mouseHorizontal) % 360f;
        transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);

        if (state == 0) { transform.Translate(0, 0, 0); }
        if (state == 1) { transform.Translate(0, 0, speed * Time.deltaTime); }
        if (state == 2) { transform.Translate(0, 0, (speed + 5.0f) * Time.deltaTime); }
        if (state == 3) { transform.Translate(0, 0, -speed * Time.deltaTime); }
        if (state == 4) { transform.Translate(speed * Time.deltaTime, 0, 0); }
        if (state == 5) { transform.Translate(-speed * Time.deltaTime, 0, 0); }
    }

	/*void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Terrain")) {
			gameObject.transform.position = gameObject.transform.position + gameObject.transform.up;}
	}*/

	void Gravity () {
		transform.GetComponent<Rigidbody>().AddForce(gravity * Vector3.down);
	}
}
