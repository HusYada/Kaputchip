// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public Image crosshair;
    public GameObject whatamilookinat;

    //SFX
    //public AudioClip aud_spraycan;

    // Componenets
    public GameObject
    spraycan,
    moviereel,
    fire_extinguisher;
    public inventory inv;
    public mouse ms;
    private Rigidbody rb;
    private GameObject cam;
    private LineRenderer lr;
    private AudioSource aud;

    // Ads Attack
    public GameObject[] ad_window;
    public Transform ad_spawn;
    public int ads_amount;

    // Solitaire Stuff
    public solitaire sol;

    // Health Bar
    public int current_hp;
    public Slider hp_bar;
    public TMP_Text hp_text;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        cam = GameObject.FindWithTag("MainCamera");
        plyr_charges = 3;
        // Lock cursor, unlock with Esc
        Cursor.lockState = CursorLockMode.Locked;
        current_hp = (int)hp_bar.value;
    }

    private void Update()
    {
        // --------------------------------------------------------------------------
        // Raycast Look Stuff (includes movie reel)

        Ray rayOrigin = new Ray(transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
        {
            var hitObject = hitInfo.collider.GetComponent<Transform>();

            if (hitObject)
            {
                UseMovieReel(hitInfo);
                if (hitObject.GetComponent<Collider>().tag == "Card")
                {
                    crosshair.color = Color.green;
                    whatamilookinat = hitObject.GetComponent<Collider>().gameObject;
                }
                else
                {
                    crosshair.color = Color.white;
                }
            }
        }
        // --------------------------------------------------------------------------

        CameraLook();
        Movement();
        Jump();
        UseSprayCan();
        UseFireExtinguisher();
        UseAdAttack();

        if(sol.raisefloor)
        {
            // lifting script -- see diy script
            //sd
        }

        if(current_hp <= 0)
        {
            SceneManager.LoadScene("homepage");
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
            ms.patrol_speed = ms.patrol_speed_after_powerup;
            ms.chasing_speed = ms.chasing_speed_after_powerup;
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

    private void UseMovieReel(RaycastHit hit)
    {
        if (Input.GetKeyDown(k_rarm) && inv.state != 2 && inv.equip_selc_pos[2].y == 500 && inv.inv_icons[14].enabled)
        {
            moviereel.transform.position = hit.point;
            reeling = true;
            ms.patrol_speed = ms.patrol_speed_after_powerup;
            ms.chasing_speed = ms.chasing_speed_after_powerup;
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

    private void UseAdAttack()
    {
        if (Input.GetKeyDown(k_rarm) && inv.state != 2 && inv.equip_selc_pos[2].y == 260 && inv.inv_icons[12].enabled && ads_amount == 0)
        {
            if(ms.behaviour != 7)
            {
                ms.prev_behav = ms.behaviour;
            }
            ads_amount = ad_window.Length;
            ms.behaviour = 7;

            // for the desktop section
            for(int i = 0; i < ad_window.Length; i++)
            {
                Instantiate(ad_window[i], ad_spawn.transform.position, Quaternion.identity);
            }
            ms.patrol_speed = ms.patrol_speed_after_powerup;
            ms.chasing_speed = ms.chasing_speed_after_powerup;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            current_hp -= 5;
            hp_bar.value -= 5;
            hp_text.text = "HP  " + current_hp + " /  75";
        }
    }
}