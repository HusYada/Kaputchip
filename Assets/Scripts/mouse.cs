// https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
using UnityEngine;
using System.Collections;

public class mouse : MonoBehaviour
{
	[Header("Timer")]
    public float 
    Time_Until_Folder_Open = 30.0f;
    //==========
	public GameObject player;
	public bool looking = true;
	public int
	rando = 0,										// Random Number
	behaviour;										// 0 = still, 1 = chasing, 2 = reeling, 3 = move to folder, 4 = dvdlogo, 5 = close
	public float patrol_speed, chasing_speed;		// 1
	private float speed;
	public GameObject rr;
	public Transform 
	targetpos,
	documents,
	internet,
	mycomputer,
	solitaire;
	private IEnumerator bc;							// Behaviour Change

	void Start()
	{
        bc = BChange(Time_Until_Folder_Open, 3);
		StartCoroutine(bc);
	}

    void Update()
    {
		switch (behaviour)
        {
        	// Move to folder
        	case 3:
        		targetpos.transform.position = documents.position;
	            speed = chasing_speed;
	         //    if(!rr.GetComponent<range>().inrange)
		        // {
		        // 	behaviour = 2;
		        // }
	            break;

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
	            	//BC(Time_Until_Folder_Open, 3);
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

            	// Counter until next phase
            	//if()

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

	// Behaviour Change
    void BC(float seconds, int behav)
    {
    	StopCoroutine(bc);
    	bc = BChange(seconds, behav);
		StartCoroutine(bc);
    }

    // Change behaviour after some time passes
    IEnumerator BChange(float seconds, int behav)
    {
        yield return new WaitForSeconds(seconds); 
        behaviour = behav;
    }
}