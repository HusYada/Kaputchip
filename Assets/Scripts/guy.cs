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
        //eyesize = eyes.transform.localScale.y;
        //eyespd = new Vector3(0f, 0.01f, 0f);
    }

    void FixedUpdate ()
    {
  		// Vector3 direction = player.transform.position - transform.position;
		// Quaternion toRotation = Quaternion.FromToRotation(-transform.forward, direction);
		// transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);
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
