// https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class mouse : MonoBehaviour
{
	[Header("Timer")]
    public float 
    Time_Until_Folder_Open = 10.0f;
    public int 
    second_passed = 0;
    //==========
	public GameObject player;
	public bool 
	window_opened,
	looking = true;
	public int
	rando = 0,										// Random Number
	behaviour;										// 0 = find, 1 = chasing, 2 = reeling, 3 = move to folder, 4 = browse, 5 = close
	public float patrol_speed, chasing_speed;		// 3.5 patrol, 7 chasing
	private float speed;
	public GameObject 
	rr,
	targetpos,
	documents,
	internet,
	mycomputer,
	solitaire,
	recylingbin;
	private IEnumerator bc;							// Behaviour Change
	private AudioSource aud;
	public AudioClip open_window;

	void Start()
	{
        //bc = BChange(Time_Until_Folder_Open, 3);
        bc = Seconds(1);
		StartCoroutine(bc);
		aud = GetComponent<AudioSource>();
	}

    void Update()
    {
		switch (behaviour)
        {
        	// Close Window
        	case 5:
        		//
        		break;

        	// Browsing
        	case 4:
        		//
        		break;

        	// Move to file shortcut
        	case 3:
        		switch (rando) {
        			// Folder
        			case 4:
        				targetpos.transform.position = documents.transform.position;
        				break;
        			// Internet
        			case 3:
        				targetpos.transform.position = internet.transform.position;
        				break;
        			// Computer
        			case 2:
        				targetpos.transform.position = mycomputer.transform.position;
        				break;
        			// Solitaire
        			case 1:
        				targetpos.transform.position = solitaire.transform.position;
        				break;
        			// Bin
        			case 0:
        				targetpos.transform.position = recylingbin.transform.position;
        				break;
        			// Browsing
        			default:
        				targetpos.transform.position = documents.transform.position;
        				break;
        		}
	            speed = chasing_speed;
	            if(transform.position == targetpos.transform.position)
	            {
	            	aud.clip = open_window;
	            	aud.Play();
	            	behaviour = 2;
	            }
	            if(rr.GetComponent<range>().inrange)
		        {
		        	behaviour = 2;
		        }
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
	            	second_passed = 0;
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

            	// Counter until next phase
            	if(second_passed >= Time_Until_Folder_Open) {
            		rando = (int)Mathf.Round(Random.Range(0, 5));
            		behaviour = 3;
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

    IEnumerator Seconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        second_passed++;
        StartCoroutine(Seconds(1));
    }
}