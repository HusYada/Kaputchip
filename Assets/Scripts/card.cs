using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
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

    void Start()
    {
        plyr = GameObject.Find("Player").GetComponent<player>();
        rb = GetComponent<Rigidbody>();
        grabscl = new Vector3(transform.localScale.x/4, transform.localScale.y/4, transform.localScale.z/4);
        grabsclreturn = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        Vector3 grabpos = new Vector3(plyr.transform.position.x, plyr.transform.position.y, plyr.transform.position.z + plyr.cam.transform.forward.z);

        if(plyr.whatamilookinat == this.gameObject && (Input.GetKeyDown(k_larm)))
        {
        	grabbed = true;
            transform.localScale = grabscl;
        }

        if(grabbed)
        {
            //this.transform.position = grabpos + plyr.cam.transform.forward;
            transform.position = Vector3.MoveTowards(transform.position,  grabpos + plyr.cam.transform.forward, 20 * Time.deltaTime);
            transform.LookAt(plyr.transform.position);
            GetComponent<Collider>().isTrigger = true;

            if(Input.GetKeyDown(k_rarm))
            {
                grabbed = false;
                transform.localScale = grabsclreturn;
                rb.AddForce(plyr.cam.transform.forward * 3000);
                GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}