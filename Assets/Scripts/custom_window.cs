// https://docs.unity3d.com/ScriptReference/Input.GetAxis.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom_window : MonoBehaviour
{
	// Inputs
    private KeyCode
    k_larm = KeyCode.Mouse0,
    k_rarm = KeyCode.Mouse1;

	[HideInInspector] public player plyr;
	[HideInInspector] public solitaire sol;
	public Renderer[] desktop_colo;
	public Material[] themes;
	private int westwallbroke;					// this defaults to 0
	public float 
	hue = 0.5f,
	sat = 1f,
	val = 0.45f;
	public float
	slider_xpos,
	slider_min_xpos,
	slider_max_xpos,
	slider_increment,
	mouseXsave;
	public GameObject[] butts;	// 0 = slider, 1-3 = music, 4-8 = theme
	[HideInInspector] public AudioSource aud;
	public AudioClip mus1;
	public AudioClip mus2;
	public AudioClip mus3;

    void Start()
    {
    	plyr = GameObject.Find("Player").GetComponent<player>();
    	sol = GameObject.Find("SolitaireTime").GetComponent<solitaire>();
        aud = GameObject.Find("Game_Manager").GetComponent<AudioSource>();
        slider_xpos = butts[0].transform.position.x + mouseXsave;
        //desktop_colo.material.color = Color.HSVToRGB(hue, sat, val);
        desktop_colo[1] = GameObject.Find("Ceiling").GetComponent<Renderer>();
        desktop_colo[2] = GameObject.Find("Wall_South").GetComponent<Renderer>();
        desktop_colo[3] = GameObject.Find("Wall_North").GetComponent<Renderer>();
        if(sol.backpanel_broke)
        {
        	desktop_colo[4] = GameObject.Find("Wall_West (Backside)").GetComponent<Renderer>();
    	}
    }

    void Update()
    {
    	if(sol.backpanel_broke)
    	{
    		westwallbroke = -1;
    	}

    	if(plyr.whatamilookinat == butts[0] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
			if(Input.GetAxis("Mouse X") < 0 && slider_xpos > slider_min_xpos)
			{
				slider_xpos -= slider_increment;
				mouseXsave -= slider_increment;
				if(hue > 0.2f)
				{
					hue-=0.02f;
				}
			}
			if(Input.GetAxis("Mouse X") > 0 && slider_xpos < slider_max_xpos)
			{
				slider_xpos += slider_increment;
				mouseXsave += slider_increment;
				if(hue < 0.98f)
				{
					hue+=0.02f;
				}
			}

			for(int i = 0; i < desktop_colo.Length + westwallbroke; i++)
    		{
    			desktop_colo[i].material.color = Color.HSVToRGB(hue, sat, val);
    		}

		}

		// Music Select (can defo clean this up)
		if(plyr.whatamilookinat == butts[1] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		aud.clip = mus1;
            aud.Play();
    	}
    	if(plyr.whatamilookinat == butts[2] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		aud.clip = mus2;
            aud.Play();
    	}
    	if(plyr.whatamilookinat == butts[3] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		aud.clip = mus3;
            aud.Play();
    	}

    	// Theme Select
    	if(plyr.whatamilookinat == butts[4] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		for(int i = 0; i < desktop_colo.Length + westwallbroke; i++)
    		{
    		desktop_colo[i].material = themes[0];
    		}
    	}
    	if(plyr.whatamilookinat == butts[5] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		for(int i = 0; i < desktop_colo.Length + westwallbroke; i++)
    		{
    		desktop_colo[i].material = themes[1];
    		}
    	}
    	if(plyr.whatamilookinat == butts[6] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		for(int i = 0; i < desktop_colo.Length + westwallbroke; i++)
    		{
    		desktop_colo[i].material = themes[2];
    		}
    	}
    	if(plyr.whatamilookinat == butts[7] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		for(int i = 0; i < desktop_colo.Length + westwallbroke; i++)
    		{
    		desktop_colo[i].material = themes[3];
    		}
    	}
    	if(plyr.whatamilookinat == butts[8] && (Input.GetKey(k_larm) || Input.GetKey(k_rarm)))
    	{
    		for(int i = 0; i < desktop_colo.Length + westwallbroke; i++)
    		{
    		desktop_colo[i].material = themes[4];
    		}
    	}

		butts[0].transform.SetPositionAndRotation(new Vector3 (slider_xpos, butts[0].transform.position.y, butts[0].transform.position.z), Quaternion.identity);
    }
}
