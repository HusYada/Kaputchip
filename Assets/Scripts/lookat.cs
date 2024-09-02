using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lookat : MonoBehaviour
{
	//[HideInInspector]
	public GameObject plyr;
	public Animator anim;
    public bool notpickup;
	public bool ikActive;

	void Start()
	{
		//plyr = GameObject.FindWithTag("Player");
        if(notpickup)
        {
		  anim = anim.GetComponent<Animator>();
        }
	}

    void Update()
    {
        transform.LookAt(plyr.transform);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(anim && notpickup)
        {
            if(ikActive)
            {
                if(plyr!= null)
                {
                    anim.SetLookAtWeight(1);
                    anim.SetLookAtPosition(plyr.transform.position);
                }
            }
        }
    }
}