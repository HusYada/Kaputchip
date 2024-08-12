using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chip_cover : MonoBehaviour
{
	public inventory inv;
    public solitaire sol;
	public int which_wall;                     // 0 = back panel, 1 = chip covers
	public int spd;
	public GameObject Command_Log;
	public GameObject destorywall;
	private destroyme cya;
	private Rigidbody rb;
	private AudioSource aud;
	public AudioClip reallygoodsoundeffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();
        cya = GetComponent<destroyme>();
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
		if(which_wall == 0 && col.gameObject.tag == "Player" && inv.inv_icons[0].enabled) 
        {
        	this.gameObject.GetComponent<Collider>().isTrigger = false;
        	rb.constraints = RigidbodyConstraints.None;
        	rb.AddForce(-transform.forward * spd);
        	Destroy(destorywall);
        	Command_Log.GetComponent<cmd_log>().UpdateCommand(20);
        	cya.enabled = true;
            sol.backpanel_broke = true;
        	inv.inv_icons[0].enabled = false;
        	aud.clip = reallygoodsoundeffect;
	        aud.Play();
        }
	}
}