// https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Array-length.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{

	#region Script Variables

	// Inputs
    private KeyCode
    k_left = KeyCode.A,
    k_right = KeyCode.D,
    k_up = KeyCode.W,
    k_down = KeyCode.S,
    k_inv = KeyCode.Q,
    k_equip =  KeyCode.Mouse0;

	// Inventory Movement
	private Vector3 inventory_pos;
	public float[] inventory_xpos;		// Where the inventory is positioned on the x-axis of the UI
	public int state;					// 0 = off, 1 = main, 2 = full, 3 = loop back around
	public bool moving;

	// Cursor Movement
	public GameObject cursor;
	public Vector3 cursor_pos;
	public int current_cursor_x;
	public int current_cursor_y;
	public int[] cursorx;
	public int[] cursory;

	// Equipping
	public GameObject[] equip_selection;
	public Vector3[] equip_selc_pos;
	public SpriteRenderer map_sprite;
	public Sprite butterfly;
	public Sprite tux;

	// Audio
	public AudioClip open_sfx;
	public AudioClip close_sfx;
	private AudioSource aud;

	#endregion

    void Start()
    {
    	aud = GetComponent<AudioSource>();
        inventory_pos = this.gameObject.transform.position;
        cursor_pos = cursor.transform.position;

        for(int i = 0; i < equip_selection.Length; i++)
    	{
    		equip_selc_pos[i] = equip_selection[i].transform.position;
    		equip_selc_pos[i].x = equip_selection[i].transform.position.x - 624;
    	}
    }

    void Update()
    {
    	 this.gameObject.transform.position = inventory_pos;
    	 cursor.transform.position = cursor_pos;

    	if(Input.GetKeyDown(k_inv))
    	{
        	moving = true;
        	if(state < 3)
        	{
        		state++;
        		aud.clip = open_sfx;
	        	aud.Play();
        	}
        	if(state == 3)
        	{
        		state = 0;
        		aud.clip = close_sfx;
	        	aud.Play();
        	}
    	}
    	if(moving)
    	{
    		Move(state);
    	}

    	// Cursor Movement
    	if(state == 2)
    	{
    		cursor_pos = new Vector3(cursorx[current_cursor_x]+960+520-4,cursory[current_cursor_y]+540-400,0);

    		if(Input.GetKeyDown(k_left) && current_cursor_x > 0) { current_cursor_x--; }
    		if(Input.GetKeyDown(k_right) && current_cursor_x < cursorx.Length-1) { current_cursor_x++; }
    		if(Input.GetKeyDown(k_up) && current_cursor_y < cursory.Length-1) { current_cursor_y++; }
    		if(Input.GetKeyDown(k_down) && current_cursor_y > 0) { current_cursor_y--; }

    		// Equipping
    		if(Input.GetKeyDown(k_equip))
    		{
    			equip_selc_pos[current_cursor_x] = new Vector3(equip_selc_pos[current_cursor_x].x, cursory[current_cursor_y]+120+20, 0);
    			if(current_cursor_x == 0 && current_cursor_y == 3)
    			{
    				map_sprite.sprite = butterfly;
    			}
    			if(current_cursor_x == 0 && current_cursor_y == 2)
    			{
    				map_sprite.sprite = tux;
    			}
    		}

    		for(int i = 0; i < equip_selection.Length; i++)
    		{
    			equip_selection[i].transform.position = equip_selc_pos[i];
    		}
    	}
    	else
    	{
    		cursor_pos = new Vector3(960,540,0);
    	}
    }

    void Move(int next_pos)
    {
    	if(transform.position.x > inventory_xpos[next_pos]+960 && state > 0)
    	{
    		inventory_pos.x-=8*state;
    	}
    	else if(transform.position.x < inventory_xpos[next_pos]+960 && state == 0)
    	{
    		inventory_pos.x+=16;
    	}
    	else
    	{
    		moving = false;
    	}
    }
}