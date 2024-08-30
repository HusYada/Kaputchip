using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding_door : MonoBehaviour
{

	public int direction; // -1 = left, 1 = right
	public float speed;
	//private Transform destination;

	void Start()
    {
        //aud = GetComponent<AudioSource>();
        //cya = GetComponent<destroyme>();
        //destination.position = new Vector3(transform.position.x + (1000 * direction), transform.position.y, transform.position.z);
    }

    void Update()
    {
    	//sd
    	//transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
    	transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + (1000 * direction), transform.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
