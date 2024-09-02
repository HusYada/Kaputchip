// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using URPGlitch.Runtime.DigitalGlitch;
using Cinemachine;

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
    public bool CanCtrl = true;

    public int
    plyr_shields,
    plyr_charges = 3;
    public bool
    has_key;
    private int
    plyr_spd = 2,
    plyr_jump = 375,
    plyr_butterfly = 20;
    private bool
    grounded,
    reeling;

    // Camera Variables
    private int
    cam_spd = 8;
    private float
    cam_v = 0f,
    cam_h = 0f;
    public Image crosshair;
    public GameObject whatamilookinat;
    float xRotation = 0f, yRotation = 0f;
    public Transform playerBody;

    //SFX
    public AudioClip aud_jump;
    public AudioClip aud_spraycan;
    public AudioClip aud_damage;
    public AudioClip aud_antiviruswarning;

    // Componenets
    public GameObject
    spraycan,
    moviereel,
    fire_extinguisher;
    public inventory inv;
    public mouse ms;
    private Rigidbody rb;
    [HideInInspector] public GameObject cam;
    private LineRenderer lr;
    private AudioSource aud;

    // Ads Attack
    public GameObject[] ad_window;
    public Transform ad_spawn;
    public int ads_amount;

    // Health Bar
    public int current_hp;
    public Slider hp_bar;
    public TMP_Text hp_text;

    // Glitch
    [SerializeField] Volume vol;
    DigitalGlitchVolume digi;
    public float glitchspd;

    // CineMachine
    public CinemachineVirtualCamera shakeCamera;
    private float ShakeTimer;

    // Anti Virus Active
    public GameObject antiwanti;
    public bool isInSecretRoom;
    public bool enteredfinaldesktop;
    public Animator CPUlidAnim;
    public Animator AntiAnim;

    // Inventory Y Positions
    private int posy0 = 500, posy1 = 380, posy2 = 260;//, posy3 = 140;

    #endregion

    private void Awake()
    {
        //cam = GameObject.Find("Virtual Camera (Inside)");
        //mainCamera = cam.GetComponent<CinemachineVirtualCamera>();

        cam = transform.Find("Virtual Camera (Inside)").gameObject;
    }

    void Start()
    {
        CanCtrl = true;

        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        plyr_charges = 3;
        // Lock cursor, unlock with Esc
        Cursor.lockState = CursorLockMode.Locked;
        shakeCamera.transform.localRotation = Quaternion.identity;
        current_hp = (int)hp_bar.value;

        vol.profile.TryGet<DigitalGlitchVolume>(out digi);

        antiwanti = GameObject.Find("Anti_Virus_Active_Warning");

        // Resolution Check
        if(Screen.currentResolution.width == 800)
        {
            posy0 = 270; posy1 = 220; posy2 = 170; //posy3 = 120;
        }
    }

    private void Update()
    {

        if(digi.intensity.value > 0)
        {
            digi.intensity.value -= glitchspd;
        }

        //if(antivirus_overlay_flashing)
        //{
            //play flashing animation here
        //}

        // Leon: I moved Raycast Look Stuff into FixedUpdate as RaycastLookStuff().
        //      You should keep everything that detects stuffs in FixedUpdate.
        //      Update: For PlayerInputs like GetKeyDown and precise math calculations.
        //      FixedUpdate: For detections like ground check, collisions.

        if (current_hp <= 0)
        {
            // Leon: This is really a bad approach, you should check if player is dead on being attacked, not on every frame.
            SceneManager.LoadScene("game_over");
        }

        if (ShakeTimer > 0f)
        {
            ShakeTimer -= (Time.deltaTime * 1f);
            if(ShakeTimer<=0f)
                shakeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        }

        if (!CanCtrl)
            return;

        CameraLook();
        Movement();
        Jump();
        UseSprayCan();
        UseFireExtinguisher();
        UseAdAttack();
    }

    void FixedUpdate()
    {

        RaycastLookStuff();

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
            if (Input.GetKey(k_jump) && !grounded && inv.equip_selc_pos[0].y == posy0 && inv.inv_icons[6].enabled)
            {
                rb.drag = plyr_butterfly;
            }
            else
            {
                rb.drag = 0;
            }
        }
        
        // --------------------------------------------------------------------------
        // Ground Check
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.55f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void CameraLook()
    {
        
        // The camera's X and Y axis is set to the mouse's X and Y position
        cam_h = Input.GetAxis("Mouse X") * cam_spd;
        cam_v = Input.GetAxis("Mouse Y") * cam_spd;
        //cam.transform.eulerAngles = new Vector3(cam_v, cam_h, 0);
        
        //camera rotation limit
        xRotation -= cam_v;
        yRotation += cam_h;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        shakeCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        rb.MoveRotation(Quaternion.Euler(0f, yRotation, 0f));
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
            aud.clip = aud_jump;
            aud.Play();
        }
    }

    private void UseSprayCan()
    {
        if(Input.GetKeyDown(k_larm) && inv.state != 2 && inv.equip_selc_pos[1].y == posy0 && inv.inv_icons[10].enabled)
        {
            rb.AddForce(-cam.transform.forward * plyr_jump);
            Quaternion cam_rotation = Quaternion.identity;
            cam_rotation.eulerAngles = new Vector3(xRotation, yRotation, 0);
            Instantiate(spraycan, rb.position + cam.transform.forward, cam_rotation);
            ms.patrol_speed = ms.patrol_speed_after_powerup;
            ms.chasing_speed = ms.chasing_speed_after_powerup;
            aud.clip = aud_spraycan;
            aud.Play();
        }
    }
    private void UseFireExtinguisher()
    {
        if(Input.GetKeyDown(k_larm) && inv.state != 2 && inv.equip_selc_pos[1].y == posy1 && inv.inv_icons[9].enabled && plyr_charges > 0)
        {
            Quaternion cam_rotation = Quaternion.identity;
            cam_rotation.eulerAngles = new Vector3(xRotation, yRotation, 0);
            Instantiate(fire_extinguisher, rb.position + cam.transform.forward, cam_rotation);
        }
    }

    private void UseMovieReel(RaycastHit hit)
    {
        if (Input.GetKeyDown(k_rarm) && inv.state != 2 && inv.equip_selc_pos[2].y == posy0 && inv.inv_icons[14].enabled)
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

        if (Input.GetKey(k_rarm) && reeling && inv.state != 2 && inv.equip_selc_pos[2].y == posy0 && inv.inv_icons[14].enabled)
        {
            cam.transform.LookAt(moviereel.transform.position);
            //rb.AddRelativeForce(cam.transform.forward * plyr_jump / 10, ForceMode.Force);
            rb.AddForce(cam.transform.forward * plyr_jump / 10);
            lr.SetPosition(1, moviereel.transform.position);
        }

        if (Input.GetKeyUp(k_rarm))
        {
            reeling = false;
            lr.SetPosition(1, transform.position);
        }
    }

    void RaycastLookStuff()
    {
        Ray rayOrigin = new Ray(transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
        {
            var hitObject = hitInfo.collider.GetComponent<Transform>();

            if (hitObject)
            {
                UseMovieReel(hitInfo);
                if (hitObject.GetComponent<Collider>().tag == "Driver")
                {
                    crosshair.color = Color.green;
                    whatamilookinat = hitObject.GetComponent<Collider>().gameObject;
                }
                else if (hitObject.GetComponent<Collider>().tag == "Card")
                {
                    crosshair.color = Color.green;
                    whatamilookinat = hitObject.GetComponent<Collider>().gameObject;
                }
                else if (hitObject.GetComponent<Collider>().tag == "Enem")
                {
                    crosshair.color = Color.red;
                    whatamilookinat = hitObject.GetComponent<Collider>().gameObject;
                    if (Input.GetKeyDown(k_larm) && inv.state != 2 && inv.equip_selc_pos[1].y == posy0 && inv.inv_icons[10].enabled)
                    {
                        Destroy(whatamilookinat, 0.5f);
                    }
                }
                else if (hitObject.GetComponent<Collider>().tag == "FireWall")
                {
                    crosshair.color = Color.cyan;
                    whatamilookinat = hitObject.GetComponent<Collider>().gameObject;
                    if (Input.GetKeyDown(k_larm) && inv.state != 2 && inv.equip_selc_pos[1].y == posy1 && inv.inv_icons[9].enabled && plyr_charges > 0)
                    {
                        // Water will only be used up if a fire is erased
                        Destroy(whatamilookinat, 0.5f);
                        plyr_charges--;
                        inv.UpdateAmount(1, plyr_charges);
                    }
                }
                else
                {
                    crosshair.color = Color.white;
                }
            }
        }
        // --------------------------------------------------------------------------
    }

    private void UseAdAttack()
    {
        if (Input.GetKeyDown(k_rarm) && inv.state != 2 && inv.equip_selc_pos[2].y == posy2 && inv.inv_icons[12].enabled && ads_amount == 0)
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            current_hp -= 5;
            hp_bar.value -= 5;
            hp_text.text = "HP  " + current_hp + " /  75";
            digi.intensity.value = 0.25f;
            aud.clip = aud_damage;
            aud.Play();

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            current_hp -= 5;
            hp_bar.value -= 5;
            hp_text.text = "HP  " + current_hp + " /  75";
            digi.intensity.value = 0.25f;
            ShakeCamera(5f, 0.5f);
            aud.clip = aud_damage;
            aud.Play();
        }

        if (col.gameObject.name == "Anti_Virus_Active_Warning_Trigger")
        {
            if (!isInSecretRoom)
            {              
                //antivirus_overlay_flashing = true;
                CPUlidAnim.SetTrigger("playAnim");
                antiwanti.SetActive(true);
                AntiAnim.SetTrigger("virusPlay");
                isInSecretRoom = true;
                aud.clip = aud_damage;
                aud.Play();
            }
        }

        if (col.gameObject.name == "Final_Desktop_Trigger" && isInSecretRoom)
        {
            enteredfinaldesktop = true;
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        // Leon:Remember, you need to set the variable "Frequency Gain" in the CinemachineVirtualCamera component.
        //      It's in CinemachineVirtualCamera => Noise => Frequency Gain.
        //      The default was 0, which shows nothing. I have set it to 5, but remember to adjust it if you are making a new one.

        CinemachineBasicMultiChannelPerlin cinePerlin = shakeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinePerlin.m_AmplitudeGain = intensity;
        ShakeTimer = time;
    }
}