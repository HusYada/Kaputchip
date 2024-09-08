using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomout : MonoBehaviour
{
	public bool zoomingout;
	public float zoomoutspeed;
	public Vector3 zoomoutposition;	// -3, 25, 21.32

    void Update()
    {
        if(zoomingout && this.GetComponent<Camera>().orthographicSize < 48f)
        {
            this.GetComponent<Camera>().orthographicSize += zoomoutspeed;
            //transform.position = new Vector3(-3, 25, desktopcam.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, zoomoutposition, zoomoutspeed * 100 * Time.deltaTime);
        }
    }
}
