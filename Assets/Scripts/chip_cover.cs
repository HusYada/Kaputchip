using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chip_cover : MonoBehaviour
{
	public inventory inv;
    public solitaire sol;
	public int which_wall;                     // 0 = back panel, 1 = chip covers, 3 = sliding-door
	public int spd;
	public GameObject Command_Log;
	public GameObject destorywall;
    [HideInInspector] public GameObject left_door;
    [HideInInspector] public GameObject right_door;
	private destroyme cya;
	private Rigidbody rb;
	private AudioSource aud;
	public AudioClip reallygoodsoundeffect;
    private GameObject desktopcam;
    //private bool sliding;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();
        cya = GetComponent<destroyme>();

        if(which_wall == 3)
        {
            left_door = GameObject.Find("Left_Door");
            right_door = GameObject.Find("Right_Door");
            desktopcam = GameObject.Find("Desktop_Camera");
        }
    }
    void OnCollisionEnter(Collision col) 
	{
		if(which_wall == 1 && col.gameObject.tag == "Player") 
        {
        	rb.constraints = RigidbodyConstraints.None;
        	rb.AddForce(transform.forward * spd);
        	cya.enabled = true;
        	aud.clip = reallygoodsoundeffect;
	        aud.Play();
        }
	}

    void OnTriggerEnter(Collider col) 
	{
		if((which_wall == 0 || which_wall == 2) && col.gameObject.tag == "Player" && inv.inv_icons[0].enabled) 
        {
        	this.gameObject.GetComponent<Collider>().isTrigger = false;
        	rb.constraints = RigidbodyConstraints.None;
        	rb.AddForce(-transform.forward * spd);
        	//Destroy(destorywall);
        	if(which_wall == 0)
            {
                Command_Log.GetComponent<cmd_log>().UpdateCommand(20);
            }
            if(which_wall == 2)
            {
                Command_Log.GetComponent<cmd_log>().UpdateCommand(27);
            }
        	cya.enabled = true;
            sol.backpanel_broke = true;
        	inv.inv_icons[0].enabled = false;
        	aud.clip = reallygoodsoundeffect;
	        aud.Play();
        }
        if(which_wall == 2 && col.gameObject.tag == "Player" && inv.inv_icons[0].enabled == false) 
        {
            Command_Log.GetComponent<cmd_log>().UpdateCommand(28);
        }

        // Sliding Door
        if(which_wall == 3 && col.gameObject.tag == "Player" && inv.inv_icons[0].enabled) 
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Renderer>().enabled = false;
            Destroy(destorywall);
            Command_Log.GetComponent<cmd_log>().UpdateCommand(20);
            inv.inv_icons[0].enabled = false;
            aud.clip = reallygoodsoundeffect;
            aud.Play();
            left_door.GetComponent<sliding_door>().enabled = true;
            right_door.GetComponent<sliding_door>().enabled = true;
            desktopcam.GetComponent<zoomout>().zoomingout = true;
        }
	}
}