using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
	public Transform teleport_location;
	[Header("Directions: 0-Left, 1-Right, 2-Up, 3-Down")]
	public int direction;
	private float offsetx, offsety;
	private player plyr;
	public AudioClip teleport_sfx;
	private AudioSource aud;

	void Start() 
	{
		plyr = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
		aud = GetComponent<AudioSource>();
		switch(direction)
		{
			case 0:
				offsetx = 1f;
				break;
			case 1:
				offsetx = -1f;
				break;
			case 2:
				offsety = 1f;
				break;
			case 3:
				offsety = -1f;
				break;
			default:
				//
				break;
		}

	}

	void OnTriggerEnter(Collider col) 
	{
		if(col.gameObject.tag == "Player") 
        {
        	col.gameObject.transform.SetPositionAndRotation(new Vector3(teleport_location.position.x + offsetx, teleport_location.position.y + offsety, teleport_location.position.z), Quaternion.identity);
        	aud.clip = teleport_sfx;
            aud.Play();
        }
	}
}
