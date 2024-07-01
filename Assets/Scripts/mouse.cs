// https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
using UnityEngine;

public class mouse : MonoBehaviour
{
	public GameObject player;
	public bool looking = true;
	public int behaviour = 0;						// 0 = still, 1 = chasing, 2 = ??
	public float patrol_speed, chasing_speed;		// 1
	private float speed;
	public GameObject rr;
	public Transform targetpos;

    void Update()
    {
		switch (behaviour)
        {
        	// Reeling Back
        	case 2:

        		if(transform.position.z < 14.5f)
	            {
	            	targetpos.transform.position = new Vector3(transform.position.x, transform.position.y, 15);
	            	speed = patrol_speed;
	            	// transform.Translate(Vector3.up * 3 * Time.deltaTime, Space.World);
	            }
	            else
	            {
	            	behaviour = 0;
	            }
	            break;

	        // Claw!!
	        case 1:

	            targetpos.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
	            speed = chasing_speed;
	            if(!rr.GetComponent<range>().inrange)
		        {
		        	behaviour = 2;
		        }
	            break;

	        // Searching
	        case 0:

	            targetpos.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 15);
	            speed = patrol_speed;

	            RaycastHit hit;
		        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
		        {
            		if(hit.transform.tag == "Player") 
            		{
            			behaviour = 1;
            		}
            	}

	            break;

	        default:
	            print ("Nothin Happenin");
	            break;
        }

    	if(looking)
    	{
    		transform.LookAt(player.transform);
    	}

        transform.position = Vector3.MoveTowards(transform.position, targetpos.transform.position, speed * Time.deltaTime);
    }
}