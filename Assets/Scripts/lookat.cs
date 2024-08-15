using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lookat : MonoBehaviour
{
	[HideInInspector]
	public GameObject plyr;

	void Start()
	{
		plyr = GameObject.FindWithTag("Player");
	}

    void Update()
    {
        transform.LookAt(plyr.transform);
    }
}