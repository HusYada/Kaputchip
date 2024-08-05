using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class window : MonoBehaviour
{
	//public Transform this_position;
	public Transform window_position;
	public float velo;
	public float speed;
	private Vector3 distance;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

    void Update()
    {
    	distance = window_position.position - transform.position;
    	velo = distance.magnitude / speed;
    	rb.velocity = distance / Mathf.Max(velo, Time.deltaTime);
    }
}
