using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guy : MonoBehaviour
{
	public GameObject player;
	//public GameObject eyes;
	public mouse mos;
	public float speed;
	public float eyemin, eyemax;
	public Vector3 eyespd;
	public Animator anim;

	public GameObject eyeR;
	public GameObject eyeL;

    void Start()
    {
    	// something here
    }

    void FixedUpdate ()
    {
		//transform.LookAt(player.transform);

		if(mos.behaviour == 1 && eyeR.transform.localScale.y > eyemin) 
		{
			eyeL.transform.localScale -= eyespd;
			eyeR.transform.localScale -= eyespd;
		} 
		if(mos.behaviour != 1 && eyeR.transform.localScale.y < eyemax)
		{
			eyeL.transform.localScale += eyespd;
			eyeR.transform.localScale += eyespd;
		}
	}
}
