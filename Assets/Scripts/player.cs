// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    #region Script Variables

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
    public int
    plyr_shields,
    plyr_charges = 3;
    public bool
    has_key;
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

    //SFX
    //public AudioClip aud_spraycan;

    // Componenets
    public GameObject
    spraycan,
    moviereel,
    fire_extinguisher;
    public inventory inv;
    private Rigidbody rb;
    private GameObject cam;
    private LineRenderer lr;
    private AudioSource aud;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        cam = GameObject.FindWithTag("MainCamera");
        plyr_charges = 3;
        // Lock cursor, unlock with Esc
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraLook();
        Movement();
        Jump();
        UseMovieReel();
        UseSprayCan();
        UseFireExtinguisher();

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
    }

    void FixedUpdate()
    {
        // --------------------------------------------------------------------------
        // Moving method for Movie Reel while using it.
        MovieReelSwinging();

        // --------------------------------------------------------------------------
        // Butterfly Body Part - From a distance from the ground, you may float
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1f))
        {
            rb.drag = 0;
        }
        else
        {
            if (Input.GetKey(k_jump) && !grounded && inv.equip_selc_pos[0].y == 500 && inv.inv_icons[6].enabled)
            {
                rb.drag = plyr_butterfly;
            }
            else
            {
                rb.drag = 0;
            }
        }

        // --------------------------------------------------------------------------
        // Custom / Interaction Time

        // Ray bnrayOrigin = new Ray(transform.position, cam.transform.forward);
        // RaycastHit bnhitInfo;

        // if(Physics.Raycast(rayOrigin, out bnhitInfo, Mathf.Infinity)) 
        // {
        //     var hitObject = bnhitInfo.collider.GetComponent<Transform>();

        //     if(hitObject)
        //     {
        //         //hmm
        //     }
        // }
        // --------------------------------------------------------------------------
    }

    private void CameraLook()
    {
        // The camera's X and Y axis is set to the mouse's X and Y position
        cam_h += Input.GetAxis("Mouse X") * cam_spd;
        cam_v -= Input.GetAxis("Mouse Y") * cam_spd;
        cam.transform.eulerAngles = new Vector3(cam_v, cam_h, 0);

        // Lock cursor, unlock with Esc
        //Cursor.lockState = CursorLockMode.Locked;

        // Reset the camera
        if (Input.GetKey("f"))
        {
            cam_h = 0.0f;
            cam_v = 0.0f;
        }
    }

    private void Movement()
    {
        // Momo: Run button detection.
        plyr_spd = (Input.GetKey(k_run)) ? 4 : 2;

        if (Input.GetKey(k_left))
        {
            rb.MovePosition(rb.position - cam.transform.right * plyr_spd * Time.deltaTime);
        }
        if (Input.GetKey(k_right))
        {
            rb.MovePosition(rb.position + cam.transform.right * plyr_spd * Time.deltaTime);
        }
        if (Input.GetKey(k_up))
        {
            rb.MovePosition(rb.position + cam.transform.forward * plyr_spd * Time.deltaTime);
        }
        if (Input.GetKey(k_down))
        {
            rb.MovePosition(rb.position - cam.transform.forward * plyr_spd * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(k_jump) && grounded)
        {
            rb.AddForce(transform.up * plyr_jump);
        }
    }

    private void UseSprayCan()
    {
        if(Input.GetKeyDown(k_larm) && inv.state != 2 && inv.equip_selc_pos[1].y == 500 && inv.inv_icons[10].enabled)
        {
            rb.AddForce(-cam.transform.forward * plyr_jump);
            Quaternion cam_rotation = Quaternion.identity;
            cam_rotation.eulerAngles = new Vector3(cam_v, cam_h, 0);
            Instantiate(spraycan, rb.position + cam.transform.forward, cam_rotation);
        }
    }
    private void UseFireExtinguisher()
    {
        if(Input.GetKeyDown(k_larm) && inv.state != 2 && inv.equip_selc_pos[1].y == 380 && inv.inv_icons[9].enabled && plyr_charges > 0)
        {
            Quaternion cam_rotation = Quaternion.identity;
            cam_rotation.eulerAngles = new Vector3(cam_v, cam_h, 0);
            Instantiate(fire_extinguisher, rb.position + cam.transform.forward, cam_rotation);
            plyr_charges--;
            inv.UpdateAmount(1, plyr_charges);
        }
    }

    private void UseMovieReel()
    {
        if (Input.GetKeyDown(k_rarm) && inv.state != 2 && inv.equip_selc_pos[2].y == 500 && inv.inv_icons[14].enabled)
        {
            //int layerMask = 1 << 6;
            //layerMask = ~layerMask;

            Ray rayOrigin = new Ray(transform.position, cam.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))//, layerMask))
            {
                var hitObject = hitInfo.collider.GetComponent<Transform>();

                if (hitObject)
                {
                    //Instantiate(moviereel, hitInfo.point, Quaternion.identity);
                    moviereel.transform.position = hitInfo.point;
                    reeling = true;
                }
            }
        }
    }

    private void MovieReelSwinging()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);

        if (Input.GetKey(k_rarm) && reeling && inv.state != 2 && inv.equip_selc_pos[2].y == 500 && inv.inv_icons[14].enabled)
        {
            cam.transform.LookAt(moviereel.transform.position);
            rb.AddRelativeForce(cam.transform.forward * plyr_jump / 10, ForceMode.Force);
            lr.SetPosition(1, moviereel.transform.position);
        }

        if (Input.GetKeyUp(k_rarm))
        {
            reeling = false;
        }
    }
}