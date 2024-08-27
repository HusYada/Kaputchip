using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class delete_bar : MonoBehaviour
{
	/*
	10 > 9
	0.95 > 1.45
	-1
	+0.5
	*/

	public float
	bar_start,
	bar_end,
	bar_startpos,
	bar_endpos,
	bar_spd,
	bar_scale;

	public GameObject mousywousy;
	public TMP_Text progress;

    void Start()
    {
        mousywousy = GameObject.Find("Mouse Pointer (Desktop)");
    }

    void Update()
    {
        if(mousywousy.GetComponent<mouse>().pushing_d_bar == false && transform.localScale.x < 10)
        {
        	Vector3 scaleChange = new Vector3(bar_scale, 0, 0);
        	//hmm
        	transform.position = Vector3.MoveTowards(transform.position, 
        	new Vector3(bar_endpos, transform.position.y, transform.position.z), bar_spd * Time.deltaTime);
        	transform.localScale += scaleChange;
        	progress.text = (int)(transform.localScale.x*10) + "%";
        }
        if(mousywousy.GetComponent<mouse>().pushing_d_bar == true && transform.localScale.x > 0)
        {
        	Vector3 scaleChange = new Vector3(bar_scale, 0, 0);
        	//hmm
        	transform.position = Vector3.MoveTowards(transform.position, 
        	new Vector3(bar_startpos, transform.position.y, transform.position.z), bar_spd * Time.deltaTime);
        	transform.localScale -= scaleChange;
        	progress.text = (int)(transform.localScale.x*10) + "%";
        }
    }
}
