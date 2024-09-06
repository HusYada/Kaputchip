using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_warp : MonoBehaviour
{
	[Header("0 = Start Pos, 1 = Motherbaord Enterance, 2 = Chip Key Room, 3 = Secret Room Enterance, 4 = Next to Driver")]
	private player plyr;
	public Vector3[] warp_points;

    // Start is called before the first frame update
    void Start()
    {
        plyr = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
        	plyr.transform.SetPositionAndRotation(warp_points[0], Quaternion.identity);
        }
        if(Input.GetKeyDown("2"))
        {
        	plyr.transform.SetPositionAndRotation(warp_points[1], Quaternion.identity);
        }
        if(Input.GetKeyDown("3"))
        {
        	plyr.transform.SetPositionAndRotation(warp_points[2], Quaternion.identity);
        }
        if(Input.GetKeyDown("4"))
        {
        	plyr.transform.SetPositionAndRotation(warp_points[3], Quaternion.identity);
        }
        if(Input.GetKeyDown("5"))
        {
        	plyr.transform.SetPositionAndRotation(warp_points[4], Quaternion.identity);
        }
    }
}
