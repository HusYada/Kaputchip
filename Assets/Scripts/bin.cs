using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bin : MonoBehaviour
{
	private Rigidbody rb;

	void Start()
    {
    	rb = GetComponent<Rigidbody>();
    }

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			//new Vector3
			//rb.MovePosition(rb.position -  * plyr_spd * Time.fixedDeltaTime);
			//rb.MovePosition(-2 * Time.fixedDeltaTime, 0.026, 0.98);
		}
	}
}