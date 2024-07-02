// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
using UnityEngine;

public class player : MonoBehaviour
{
	// Inputs
	private KeyCode 
	k_left = KeyCode.A, 
	k_right = KeyCode.D, 
	k_up = KeyCode.W, 
	k_down = KeyCode.S,
	k_run = KeyCode.LeftShift,
	k_jump = KeyCode.Space,
	k_larm = KeyCode.Mouse0,
	k_rarm = KeyCode.Mouse1;

	// Player Variables
	private int 
	plyr_spd = 2,
	plyr_jump = 250,
	plyr_butterfly = 20;
	private bool
	grounded,
    reeling;

	// Camera Variables
	private int
	cam_spd = 8;
	private float 
	cam_v, 
	cam_h;

	// Componenets
	public GameObject 
	spraycan,
	moviereel;
    private Rigidbody rb;
    private GameObject cam;
    private LineRenderer lr;

    void Start()
    {
    	rb = GetComponent<Rigidbody>();
    	lr = GetComponent<LineRenderer>();
    	cam = GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate()
    {
    	// --------------------------------------------------------------------------
    	// Camera Movement

    	// The camera's X and Y axis is set to the mouse's X and Y position
        cam_h += Input.GetAxis("Mouse X") * cam_spd;
        cam_v -= Input.GetAxis("Mouse Y") * cam_spd;
        cam.transform.eulerAngles = new Vector3(cam_v, cam_h, 0);

        // Lock cursor, unlock with Esc
        Cursor.lockState = CursorLockMode.Locked;

        // Reset the camera
        if(Input.GetKey("f")) 
        {
            cam_h = 0.0f;
            cam_v = 0.0f;
        }

        // --------------------------------------------------------------------------
    	// Player Movement

		if(Input.GetKey(k_left)) 
		{
			rb.MovePosition(rb.position - cam.transform.right * plyr_spd * Time.fixedDeltaTime);
		}
		if(Input.GetKey(k_right)) 
		{
			rb.MovePosition(rb.position + cam.transform.right * plyr_spd * Time.fixedDeltaTime);
		}
		if(Input.GetKey(k_up)) 
		{
			rb.MovePosition(rb.position + cam.transform.forward * plyr_spd * Time.fixedDeltaTime);
		}
		if(Input.GetKey(k_down)) 
		{
			rb.MovePosition(rb.position - cam.transform.forward * plyr_spd * Time.fixedDeltaTime);
		}

		// --------------------------------------------------------------------------
		// Ground Check
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.5f))
        {
            grounded = true;
        }
        else
        {
        	grounded = false;
        }

        // --------------------------------------------------------------------------
		// Run Button
		if(Input.GetKey(k_run))
		{
			plyr_spd = 4;
		} 
		else 
		{
			plyr_spd = 2;
		}

		// --------------------------------------------------------------------------
		// Jump Button
		if(Input.GetKey(k_jump) && grounded)
		{
			rb.AddForce(transform.up * plyr_jump);
		}

		// --------------------------------------------------------------------------
		// Butterfly Body Part - From a distance from the ground, you may float
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1f))
        {
        	rb.drag = 0;
        }
        else
        {
        	if(Input.GetKey(k_jump) && !grounded)
			{
				rb.drag = plyr_butterfly;
			}
			else 
			{
				rb.drag = 0;
			}
        }

        // --------------------------------------------------------------------------
    	// Spray Can Arm Part
        if(Input.GetKeyDown(k_larm))
        {
        	rb.AddForce(-cam.transform.forward * plyr_jump);
        	Quaternion cam_rotation = Quaternion.identity;
        	cam_rotation.eulerAngles = new Vector3(cam_v, cam_h, 0);
			Instantiate(spraycan, rb.position + cam.transform.forward, cam_rotation);
        }

        // --------------------------------------------------------------------------
    	// Movie Reel Arm Part
    	        
    	// make it so its turned off when not using
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);

    	if(Input.GetKeyDown(k_rarm))
    	{
            int layerMask = 1 << 6;
            layerMask = ~layerMask;

    		Ray rayOrigin = new Ray(transform.position, cam.transform.forward);
    		RaycastHit hitInfo;

    		if(Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, layerMask)) 
    		{
    			var hitObject = hitInfo.collider.GetComponent<Transform>();

    			if(hitObject)
    			{
    				//Instantiate(moviereel, hitInfo.point, Quaternion.identity);
    				moviereel.transform.position = hitInfo.point;
                    reeling = true;
    			}
    		}
    	}

        if(Input.GetKey(k_rarm) && reeling)
        {
        	cam.transform.LookAt(moviereel.transform.position);
			rb.AddRelativeForce(cam.transform.forward * plyr_jump/10, ForceMode.Force);
			lr.SetPosition(1, moviereel.transform.position);
        }

        if(Input.GetKeyUp(k_rarm))
        {
        	reeling = false;
        }

		// --------------------------------------------------------------------------
    }
}