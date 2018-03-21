using UnityEngine;
using System.Collections;

public enum ControllerType { pc, xbox, ps };

public class TPSPlayerController : MonoBehaviour {

    public float speed = 8.0f;
    public float gravity = 3.0f;

    //This variable indicates the current state of character.

    private int state;

    // 0 for PC, 1 for xbox controller
    private int controlState = 0;

    //Define the turning speed.
    float turnSpeed = 7f;


    private float horizontal;

    //private Animator animacao;
    HumanBullet humanBullet;

    public ControllerType controllerType = ControllerType.pc;

    HPManager hPManager;


    public bool moveWithPhysics = true;
    Rigidbody rb;
    float maxVelocityChange;
    Vector3 targetVelocity;

    public bool grounded;
    private float mH, mV;

    void Start()
    {
        //animacao = GetComponentInChildren<Animator>();
        humanBullet = GetComponent<HumanBullet>();
        state = 0;
        horizontal = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        hPManager = gameObject.GetComponent<HPManager>();

        rb = GetComponent<Rigidbody>();
        maxVelocityChange = speed;
        targetVelocity = Vector3.zero;
    }

    void Update()
    {

        if (hPManager.isDead)
        {
            return;
        }
        CheckControllerType();
        MouseToggleInGame();

        if (moveWithPhysics)
        {
            MoveWithPhysics();
        }
        else
        {
            Control();
            Controller();
        }
        CheckForWarp();

 
       // MoveHorizontalCamera();
        AnimatePerson();

    }

    private void LateUpdate()
    {
        // Make sure we apply gravity after move with physics, otherwise it'll make y velocity 0
        if (!humanBullet.bulletMode)
        {
            Gravity();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9) // ground
        {
            grounded = true;
        }
    }

    // Collision exit doesn't work with warping, toggle grounded when player warps
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 9) // ground
        {
            grounded = false;
        }
    }


    private void CheckControllerType()
    {
        string[] names = Input.GetJoystickNames();
        foreach (string name in names)
        {
            //Debug.Log(name);
            if (name == "Controller (Xbox One For Windows)")
            {
                controllerType = ControllerType.xbox;
            } else
            {
                controllerType = ControllerType.ps;
            }
            
        }
    }


    /*
     * Use Esc Key to show cursor
     * Click game screen to hide cursor
     */
    private void MouseToggleInGame () 
    {
         if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // allows us to click back into game
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void AnimatePerson()
    {
        //animacao.SetInteger("Estado", state);
    }

    private void CheckForWarp()
    {
        if (Input.GetMouseButtonDown(0) ||
            Input.GetButtonDown("A Button") && controllerType == ControllerType.xbox ||
            Input.GetButtonDown("X Button") && controllerType == ControllerType.ps)
        {
            if (GetComponent<WarpLimiter>().canWarp)
            {
                humanBullet.ShootMe();
                GetComponent<WarpLimiter>().ConsumeCharge();
            }
        }
    }
    private void Control()
    {
        /*
        States:
        00 = Idle
        01 = Forward
        02 = Backward
        03 = Right
        04 = Left
        */
        mH = Input.GetAxis("Horizontal");
        mV = Input.GetAxis("Vertical");
        
        if (mH < 0) { state = 4; }
        else if (mH > 0) { state = 3; }
        else if (mV < 0) { state = 2; }
        else if (mV > 0) { state = 1; }
        else { state = 0; }

    }


    private void Controller () {
        
        //if (state == 0) { transform.Translate(0, 0, 0); }
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        //rb.AddForce(dir * speed * Time.deltaTime);
        if (!humanBullet.bulletMode)
        {
            targetVelocity = (dir != Vector3.zero) ? transform.forward : dir;
            targetVelocity *= speed;
            var velocity = rb.velocity;
            var velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;// Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);// 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

         // transform.Translate(dir * Time.deltaTime * speed, Space.World);
        /* 
        if (state == 1) 
        { 
            transform.Translate(0, 0, speed * Time.deltaTime); 
        }
        if (state == 2) 
        { 
            transform.Translate(0, 0, -speed * Time.deltaTime); 
        }
        if (state == 3) 
        { 
            //transform.Rotate(Vector3.up * Time.deltaTime * speed * speed, Space.World);
            transform.Translate(speed * Time.deltaTime, 0, 0); 
        }
        if (state == 4) 
        { 
            //transform.Rotate(Vector3.up * Time.deltaTime * -speed * speed, Space.World);
            transform.Translate(-speed * Time.deltaTime, 0, 0); 
        }*/
        


        //transform.Translate(mH * speed * Time.deltaTime, 0, mV * speed * Time.deltaTime);
        /*for (int i = 0; i < speed; i++)
        {
            //
            transform.Translate(mH * Time.deltaTime, 0, mV * Time.deltaTime);
        }*/



        /* 
        // keypad or numpad 1 for pc, 2 for xbox controller
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) 
        {
            controlState = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            controlState = 1;
        }

        if (controlState == 0) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                
            }
            transform.Translate(mH * speed * Time.deltaTime, 0, mV * speed * Time.deltaTime);
        } 
        else if (controlState == 1) 
        {
            if (Input.GetButtonDown("B Button")) 
            {
                humanBullet.ShootMe();
            }
            transform.Translate(mH * speed * Time.deltaTime, 0, mV * speed * Time.deltaTime);
        }*/
    }

    private void MoveWithPhysics()
    {
        
        if (!humanBullet.bulletMode)
        {
            /*float y;
            if (grounded)
            {
                y = 0;
            } else
            {
                y = -gravity;
            }*/
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;
            var velocity = rb.velocity;
            var velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;// Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);// 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

        private void MoveHorizontalCamera()
    {
        float mouseHorizontal = 0;

        // Prevent weird camera movement when locking on to target
        if (!GetComponent<LockOn>().targetLockedOn)
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                mouseHorizontal = Input.GetAxis("Mouse X");
            }

            // might need to speed up movement of right stick axis, they seem way slower than with mouse
            // or slow down mouse for precision when aiming
            if (Input.GetAxis("Right Stick X") != 0 && controllerType == ControllerType.xbox)
            {
                mouseHorizontal = Input.GetAxis("Right Stick X");
            }

            if (Input.GetAxis("Right Stick X (PS4)") != 0 && controllerType == ControllerType.ps)
            {
                mouseHorizontal = Input.GetAxis("Right Stick X (PS4)");
            }
        }

        horizontal = (horizontal + turnSpeed * mouseHorizontal) % 360f;
        transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);

        
    }

	/*void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Terrain")) {
			gameObject.transform.position = gameObject.transform.position + gameObject.transform.up;}
	}*/

	void Gravity () {
        //if (!humanBullet.bulletMode)
        transform.GetComponent<Rigidbody>().AddForce(gravity * Vector3.down);
	}
}
