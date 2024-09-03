using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driver_grab : MonoBehaviour
{
	public player plyr;
    public bool grabbed;
	// Inputs
    private KeyCode
    k_larm = KeyCode.Mouse0,
    k_rarm = KeyCode.Mouse1;
    private Rigidbody rb;
    private Vector3 grabscl;
    private Vector3 grabsclreturn;
    public GameObject deletingcdrivewindow;
    public cmd_log cmd;

    void Start()
    {
        plyr = GameObject.Find("Player").GetComponent<player>();
        rb = GetComponent<Rigidbody>();
        grabscl = new Vector3(transform.localScale.x/8, transform.localScale.y/8, transform.localScale.z/8);
        grabsclreturn = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        Vector3 grabpos = new Vector3(
        	plyr.transform.position.x, 
        	plyr.transform.position.y, 
        	plyr.transform.position.z);

        if(plyr.whatamilookinat == this.gameObject && (Input.GetKeyDown(k_larm)))
        {
        	grabbed = true;
            transform.localScale = grabscl;
            cmd.UpdateCommand(30);
        }

        if(grabbed)
        {
            this.transform.position = grabpos + plyr.cam.transform.forward;
            GetComponent<Collider>().isTrigger = true;

            if(Input.GetKeyDown(k_rarm) && plyr.enteredfinaldesktop)
            {
                grabbed = false;
                transform.localScale = grabsclreturn;
                rb.AddForce(plyr.cam.transform.forward * 2500);
                GetComponent<Collider>().isTrigger = false;
            }
        }

        //Debug.Log("Looking at: " + plyr.whatamilookinat.name);

    }

    void OnCollisionEnter(Collision col)
	{
        if (col.gameObject.name == "RubbishBin")
        {
            //cmd.UpdateCommand(32);
        	Instantiate(deletingcdrivewindow);
            Destroy(this.gameObject);
        }
	}
}


