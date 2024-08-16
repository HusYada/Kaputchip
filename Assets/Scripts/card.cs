using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
	public player plyr;
	// Inputs
    private KeyCode
    k_larm = KeyCode.Mouse0,
    k_rarm = KeyCode.Mouse1;

    void Start()
    {
        plyr = GameObject.Find("Player").GetComponent<player>();
    }

    void Update()
    {
        if(plyr.whatamilookinat == this && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
        {
        	//
        	this.transform.position = plyr.cam.transform.forward;
        }
    }
}
