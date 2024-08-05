using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chip_cover : MonoBehaviour
{
	public inventory inv;
	public int which_wall;
	public int spd;
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

    void OnTriggerEnter(Collider col) 
	{
		if(which_wall == 0 && col.gameObject.tag == "Player" && inv.inv_icons[0].enabled) 
        {
        	this.gameObject.GetComponent<Collider>().isTrigger = false;
        	rb.constraints = RigidbodyConstraints.None;
        	rb.AddForce(-transform.forward * spd);
        	Destroy(destorywall);
        	cya.enabled = true;
        	inv.inv_icons[0].enabled = false;
        	aud.clip = reallygoodsoundeffect;
	        aud.Play();
        }
	}
}