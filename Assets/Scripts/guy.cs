using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guy : MonoBehaviour
{
	public GameObject player;
	public GameObject eyes;
	public mouse mos;
	public float speed;
	public float eyemin, eyemax;
	public Vector3 eyespd;

    void Start()
    {
    	// something here
    }

    void FixedUpdate ()
    {
		transform.LookAt(player.transform);

		if(mos.behaviour == 1 && eyes.transform.localScale.y > eyemin) 
		{
			eyes.transform.localScale -= eyespd;
		} 
		if(mos.behaviour != 1 && eyes.transform.localScale.y < eyemax)
		{
			eyes.transform.localScale += eyespd;
		}
    }
}
