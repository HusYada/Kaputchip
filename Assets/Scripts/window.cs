// https://docs.unity3d.com/ScriptReference/Component.GetComponentsInChildren.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class window : MonoBehaviour
{
	//public Transform this_position;
	public Transform[] window_locations;
    public TMP_Text bar_text;
	public float speed;
	public int action;
	private float velocity;
	public GameObject wsp;
	private Vector3 distance;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		wsp = GameObject.Find("Window_Pos"); 				// Window_Spawn_Positions
		// foreach (Transform child in window_locations)
  //       {
  //       	//
  //       }
	}

    void Update()
    {
    	if(action == 0)
    	{
    		Move();
    	}
    }
    void Move()
    {
    	distance = wsp.transform.position - transform.position; //window_locations[0].position - transform.position;
    	velocity = distance.magnitude / speed;
    	rb.velocity = distance / Mathf.Max(velocity, Time.deltaTime);
    }
}
