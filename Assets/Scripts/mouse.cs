// https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
// https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
// https://docs.unity3d.com/ScriptReference/Quaternion-eulerAngles.html
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tolist?view=net-6.0#system-linq-enumerable-tolist-1(system-collections-generic-ienumerable((-0)))

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class mouse : MonoBehaviour
{
	[Header("Timer")]
    public float 
    Time_Until_Folder_Open = 10.0f,
    Time_Until_TutorialEnd = 60.0f,
    Time_Until_Folder_Open_Original = 10.0f,
    Time_Until_Folder_Close = 10.0f,
    Time_Until_Folder_CloseDocuments = 10.0f,
    Time_Until_Folder_CloseWelcomeWindow;
    public int 
    second_passed = 0;
    //==========
    [Header("Status")]
    public GameObject player;
    public MeshRenderer
    mouse_model,
    mouse_wait1,
    mouse_wait2,
    mouse_hand1,
    mouse_hand2;
	public bool 
	looking = true;
	public int
	rando = 0,										// Random Number
    prev_behav,
	behaviour;										// 0 = find, 1 = chasing, 2 = reeling, 3 = move to folder, 4 = gotowindow, 5 = browse, 6 = close
	public float 
    patrol_speed,                                   // 3.5 patrol, 7 chasing, 0.75 circling?
    patrol_speed_after_powerup,
    chasing_speed,
    chasing_speed_after_powerup,
    circling_speed,
    radius;
	private float speed, angle;
	public GameObject 
	rr,                                             // Range
	targetpos,
    windowspawn,
	documents,
	internet,
	mycomputer,
	solitaire,
	recylingbin,
    rollo;
    public GameObject winn0, winn1, winn2, winn3, winn4, spawned_window, windowspawnexit;
    public List<GameObject> ad_exits;
    public int ad_target;
    public bool ad_init;
	private IEnumerator bc;							// Behaviour Change
    public cmd_log cmd;
    public solitaire st;
	private AudioSource aud;
	public AudioClip 
    open_window,
    close_window;
    private                         Quaternion currentRotation;

    public GameObject d_bar, d_handle;
    public bool pushing_d_bar;

    [Header("Indicator")]
    public MouseIndicator mouseIndicator;

    //change to timer

	void Start()
	{
        //bc = BChange(Time_Until_Folder_Open, 3);
        bc = Seconds(1);
		StartCoroutine(bc);
		aud = GetComponent<AudioSource>();
        Time_Until_Folder_Open = Time_Until_TutorialEnd;
	}

    void Update()
    {
		switch (behaviour)
        {
            // Waiting Before Welcome Window
            case 9:
                spawned_window = GameObject.Find("WelcomeWindow");
                // windowspawnexit = GameObject.Find("Exit Button");
                // targetpos.transform.position = windowspawnexit.transform.position;
                if(second_passed > Time_Until_Folder_CloseWelcomeWindow)
                {
                    speed = chasing_speed;
                    behaviour = 6;
                }
                break;
            // Stopping the Bar
            case 8:

                mouseIndicator.SetIcon(2);

                targetpos.transform.position = d_handle.transform.position;
                speed = patrol_speed_after_powerup;

                if(transform.position == d_bar.transform.position)
                {
                    pushing_d_bar = true;
                    mouse_model.enabled = false;
                    mouse_hand1.enabled = true;
                    mouse_hand2.enabled = true;
                }

                // Testing taking the mouse off the Delete Bar
                // if(Input.GetKey("j"))
                // {
                //     pushing_d_bar = false;
                //     mouse_model.enabled = true;
                //     mouse_hand1.enabled = false;
                //     mouse_hand2.enabled = false;
                //     transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
                // }
                break;

            // Ads Interuption
            case 7:

                mouseIndicator.SetIcon(2);
                pushing_d_bar = false;

                if (ad_exits.Count < player.GetComponent<player>().ad_window.Length)
                {
                    ad_exits = GameObject.FindGameObjectsWithTag("Exit_Button").ToList();
                    ad_target = 0;
                    speed = chasing_speed;
                }

                if (ad_exits.Count == player.GetComponent<player>().ad_window.Length)
                {
                    if (transform.position != ad_exits[ad_target].transform.position && ad_target < player.GetComponent<player>().ad_window.Length) {
                        targetpos.transform.position = ad_exits[ad_target].transform.position;
                    }
                
                    // When the mouse reaches a way point
                    else
                    { 
                        //transform.parent.
                        Destroy(ad_exits[ad_target].transform.parent.gameObject);
                        player.GetComponent<player>().ads_amount--;
                        if(ad_target < player.GetComponent<player>().ad_window.Length)
                        {
                            ad_target++;
                        }
                    }

                    if (ad_target == player.GetComponent<player>().ad_window.Length)
                    {
                        ad_target = 0;
                        ad_exits.Clear();

                        if(prev_behav == 4 || prev_behav == 5 || prev_behav == 6)
                        {
                            behaviour = 6;
                            mouse_model.enabled = true;
                            mouse_wait1.enabled = false;
                            mouse_wait2.enabled = false;
                        }
                        if(prev_behav == 0 || prev_behav == 1 || prev_behav == 2)
                        {
                            behaviour = 2;
                            mouse_model.enabled = true;
                            mouse_wait1.enabled = false;
                            mouse_wait2.enabled = false;
                        }
                        if(prev_behav == 8)
                        {
                            behaviour = 8;
                        }
                    }
                }

                break;

            // Close Window
            case 6:

                mouseIndicator.SetIcon(2);

                windowspawnexit = GameObject.Find("Exit Button");
                targetpos.transform.position = windowspawnexit.transform.position;
                if(transform.position == targetpos.transform.position)
                {
                    behaviour = 2;
                    aud.clip = close_window;
                    aud.Play();
                    Destroy(spawned_window);
                }
                break;

        	// Browsing Window
        	case 5:

                mouseIndicator.SetIcon(2);

                float circlespeed = (Mathf.PI * 2) / circling_speed;
                angle += Time.deltaTime * circlespeed;
                float movementx = radius * Mathf.Cos(angle);
                float movementy = radius * Mathf.Sin(angle);
                targetpos.transform.position = new Vector3 (spawned_window.transform.position.x + movementx, 
                                                            spawned_window.transform.position.y + movementy + 4, spawned_window.transform.position.z + 4);

                if(second_passed >= Time_Until_Folder_CloseDocuments && spawned_window == GameObject.Find("customization_window(Clone)")) {
                    behaviour = 6;
                    speed = patrol_speed;
                }
                if(second_passed >= Time_Until_Folder_Close && spawned_window == GameObject.Find("Window(Clone)")) {
                    behaviour = 6;
                    speed = patrol_speed;
                    
                }
                if(second_passed >= Time_Until_Folder_CloseDocuments && spawned_window == GameObject.Find("Window2_1(Clone)")) {
                    behaviour = 6;
                    speed = patrol_speed;
                }
        		break;

            // Go infront of Window
            case 4:

                mouseIndicator.SetIcon(2);

                targetpos.transform.position = new Vector3(spawned_window.transform.position.x, spawned_window.transform.position.y + 4, spawned_window.transform.position.z + 4);
                if(transform.position == targetpos.transform.position)
                {
                    behaviour = 5;
                    mouse_model.enabled = true;
                    mouse_wait1.enabled = false;
                    mouse_wait2.enabled = false;
                }
                break;

        	// Move to file shortcut
        	case 3:

                mouseIndicator.SetIcon(2);
                Time_Until_Folder_Open = Time_Until_Folder_Open_Original;

                switch (rando) {
                    // Rubbish Bin
                    case 5:
                        targetpos.transform.position = recylingbin.transform.position;
                        break;
        			// Folder
        			case 4:
        				targetpos.transform.position = documents.transform.position;
                        // mouse_wait1.enabled = false;
                        // mouse_wait2.enabled = false;
                        // mouse_hand1.enabled = false;
                        // mouse_hand2.enabled = false;
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
        				//targetpos.transform.position = recylingbin.transform.position;
                        targetpos.transform.position = documents.transform.position;
                        //Destroy(winn4);
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
                    if(targetpos.transform.position == documents.transform.position)
                    {
                        Instantiate(winn0, windowspawn.transform.position, Quaternion.identity);
                        spawned_window = GameObject.Find("Window2_1(Clone)");
                        spawned_window.GetComponent<window>().bar_text.text = "THE Folder";
                        cmd.UpdateCommand(21);
                    }
                    if(targetpos.transform.position == internet.transform.position)
                    {
                        Instantiate(winn1, windowspawn.transform.position, Quaternion.identity);
                        spawned_window = GameObject.Find("Window(Clone)");
                        spawned_window.GetComponent<window>().bar_text.text = "Bum Explorer";
                        cmd.UpdateCommand(22);
                    }
                    if(targetpos.transform.position == mycomputer.transform.position)
                    {
                        currentRotation.SetLookRotation(new Vector3(90, 10000, 0), Vector3.forward);
                        Instantiate(winn2, windowspawn.transform.position, currentRotation);
                        spawned_window = GameObject.Find("customization_window(Clone)");
                        cmd.UpdateCommand(23);
                    }
                    if(targetpos.transform.position == recylingbin.transform.position)
                    {
                        behaviour = 2;
                        mouse_model.enabled = false;
                        mouse_hand1.enabled = true;
                        mouse_hand2.enabled = true;
                        Instantiate(rollo, transform.position, Quaternion.identity, this.transform);
                    }
                    if(targetpos.transform.position != recylingbin.transform.position)
                    {
                        behaviour = 4;
                        speed = chasing_speed * 2;
                        second_passed = 0;
                        mouse_model.enabled = true;
                        mouse_wait1.enabled = false;
                        mouse_wait2.enabled = false;
                    }
	            }
                if(transform.position == targetpos.transform.position && rando == 1)
                {
                    aud.clip = open_window;
                    aud.Play();
                    st.BeginGame();
                    behaviour = 2;
                }

                if(rr.GetComponent<range>().inrange)
                {
                 behaviour = 1;
                }
	            break;

        	// Reeling Back
        	case 2:

                mouseIndicator.SetIcon(2);

        		if(transform.position.z < 10.54)
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

                mouseIndicator.SetIcon(1);

	            if(!rr.GetComponent<range>().inrange)
		        {
		        	behaviour = 2;
		        }
	            break;

	        // Searching
	        case 0:

	            targetpos.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 15);
                Vector3 raycastpos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	            speed = patrol_speed;

                mouseIndicator.SetIcon(0);

	         //    RaycastHit hit;
          //       Debug.DrawRay(raycastpos, transform.TransformDirection(Vector3.forward), Color.red);
		        // if (Physics.Raycast(raycastpos, transform.TransformDirection(Vector3.forward), out hit))
		        // {
          //   		if(hit.transform.tag == "Player") 
          //   		{
          //   			behaviour = 1;
          //   		}
          //   	}

                if(rr.GetComponent<range>().inrange)
                {
                 behaviour = 1;
                }

            	// Counter until next phase
            	if(second_passed >= Time_Until_Folder_Open) {
            		rando = (int)Mathf.Round(Random.Range(0, 6));
                    //rando = 5;
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
        //targetpos.transform.position = d_handle.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, targetpos.transform.position, speed * Time.deltaTime);
    }

    IEnumerator Seconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        second_passed++;
        StartCoroutine(Seconds(1));
    }
}