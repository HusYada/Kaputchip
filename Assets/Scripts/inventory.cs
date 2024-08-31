// https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Array-length.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    //k_inv_back = KeyCode.E,
    k_equip = KeyCode.Mouse0,
    k_desc = KeyCode.Mouse1;

	// Inventory Movement
	private Vector3 inventory_pos;
	public float[] inventory_xpos;		// Where the inventory is positioned on the x-axis of the UI
	public int state;					// 0 = off, 1 = main, 2 = full, 3 = loop back around
	public bool moving;
	public bool firstopen = true;
	public bool firstbody = true;

	// Cursor Movement
	public GameObject cursor;
	public Vector3 cursor_pos;
	public int current_cursor_x;
	public int current_cursor_y;
	public int[] cursorx;
	public int[] cursory;

	// Equipping & Picking Up
	public GameObject[] equip_selection;
	public Vector3[] equip_selc_pos;
	public SpriteRenderer map_sprite;
	public Image[] inv_icons; /*
	[--][ 6][10][14][18]
	[ 2][ 5][ 9][13][17]
	[ 1][ 4][ 8][12][16]
	[ 0][ 3][ 7][11][15] */
	public TMP_Text shield_amount;
	public TMP_Text fire_charges;
	public cmd_log cmd;

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
        firstopen = true;

        for(int i = 0; i < equip_selection.Length; i++)
    	{
            if(Screen.currentResolution.width == 1920)
            {
    		  equip_selc_pos[i] = equip_selection[i].transform.position;
    		  equip_selc_pos[i].x = equip_selection[i].transform.position.x - 624;
            }
            if(Screen.currentResolution.width == 800)
            {
                equip_selc_pos[i].y = 270;
                equip_selc_pos[i].x = 626 + (49 * i);

                // y = 270
                // 631, 680, 728, 776
            }
    	}

        if(Screen.currentResolution.width == 800)
        {
            inventory_xpos[0] = 380;
            inventory_xpos[1] = 334;
            inventory_xpos[2] = 150;
            //
            inventory_pos.x = 927 - 135;
            // -2, 47, 95, 143
              // 0, 50, 100, 150
              cursorx[0] = -7;
              cursorx[1] = 42;
              cursorx[2] = 90;
              cursorx[3] = 138;
              cursory[0] = 0;
              cursory[1] = 50;
              cursory[2] = 100;
              cursory[3] = 150;
        }
    }

    void Update()
    {
    	this.gameObject.transform.position = inventory_pos;
    	cursor.transform.position = cursor_pos;

        if(Screen.currentResolution.width == 800)
        {
            inventory_pos.y = 120;
        }

    	if(Input.GetKeyDown(k_inv))
    	{
        	moving = true;
        	if(state < 3)
        	{
        		state++;
        		aud.clip = open_sfx;
	        	aud.Play();
	        	if(state == 2 && firstopen)
	        	{
	        		firstopen = false;
	        		cmd.UpdateCommand(19);
	        	}
        	}
        	if(state == 3)
        	{
        		state = 0;
        		aud.clip = close_sfx;
	        	aud.Play();
        	}
    	}

    	// if(Input.GetKeyDown(k_inv_back))
    	// {
    	// 	moving = true;
     //    	if(state > 0)
     //    	{
     //    		state--;
     //    		aud.clip = close_sfx;
	    //     	aud.Play();
     //    	}
     //    	if(state == 0)
     //    	{
     //    		state = 2;
     //    		aud.clip = open_sfx;
	    //     	aud.Play();
     //    	}
    	// }

    	if(moving)
    	{
    		Move(state);
    	}

    	// Cursor Movement
    	if(state == 2)
    	{
            if(Screen.currentResolution.width == 1920)
            {
              cursor_pos = new Vector3(cursorx[current_cursor_x]+960+520-4,cursory[current_cursor_y]+540-400,0);
            }
            if(Screen.currentResolution.width == 800)
            {
              cursor_pos = new Vector3(
                cursorx[current_cursor_x]+633,
                cursory[current_cursor_y]+120, 0);
              //cursor_pos = new Vector3(629, 120, 0);
              //
            }
    		

    		if(Input.GetKeyDown(k_left) && current_cursor_x > 0) { current_cursor_x--; }
    		if(Input.GetKeyDown(k_right) && current_cursor_x < cursorx.Length-1) { current_cursor_x++; }
    		if(Input.GetKeyDown(k_up) && current_cursor_y < cursory.Length-1) { current_cursor_y++; }
    		if(Input.GetKeyDown(k_down) && current_cursor_y > 0) { current_cursor_y--; }

    		// Equipping
    		if(Input.GetKeyDown(k_equip))
    		{
                if(Screen.currentResolution.width == 1920)
                {
    			equip_selc_pos[current_cursor_x] = new Vector3(equip_selc_pos[current_cursor_x].x, cursory[current_cursor_y]+120+20, 0);
                }
                if(Screen.currentResolution.width == 800)
                {
                equip_selc_pos[current_cursor_x] = new Vector3(equip_selc_pos[current_cursor_x].x, cursory[current_cursor_y]+120, 0);
                }


    			if(current_cursor_x == 0 && inv_icons[6-current_cursor_y].enabled)
				{
					map_sprite.sprite = inv_icons[3+current_cursor_y].sprite;
				}
    		}

    		// Description - prob a cleaner way of codin this
    		if(Input.GetKeyDown(k_desc) && cmd.typingended)
    		{
    			if(current_cursor_x == 3 && current_cursor_y == 3 && inv_icons[18].enabled)
    			{
    				cmd.UpdateCommand(18);
    			}
    			if(current_cursor_x == 3 && current_cursor_y == 2 && inv_icons[17].enabled)
    			{
    				cmd.UpdateCommand(17);
    			}
    			if(current_cursor_x == 3 && current_cursor_y == 1 && inv_icons[16].enabled)
    			{
    				cmd.UpdateCommand(16);
    			}
    			if(current_cursor_x == 3 && current_cursor_y == 0 && inv_icons[15].enabled)
    			{
    				cmd.UpdateCommand(15);
    			}
    			if(current_cursor_x == 2 && current_cursor_y == 3 && inv_icons[14].enabled)
    			{
    				cmd.UpdateCommand(14);
    			}
    			if(current_cursor_x == 2 && current_cursor_y == 2 && inv_icons[13].enabled)
    			{
    				cmd.UpdateCommand(13);
    			}
    			if(current_cursor_x == 2 && current_cursor_y == 1 && inv_icons[12].enabled)
    			{
    				cmd.UpdateCommand(12);
    			}
    			if(current_cursor_x == 2 && current_cursor_y == 0 && inv_icons[11].enabled)
    			{
    				cmd.UpdateCommand(11);
    			}
    			if(current_cursor_x == 1 && current_cursor_y == 3 && inv_icons[10].enabled)
    			{
    				cmd.UpdateCommand(10);
    			}
    			if(current_cursor_x == 1 && current_cursor_y == 2 && inv_icons[9].enabled)
    			{
    				cmd.UpdateCommand(9);
    			}
    			if(current_cursor_x == 1 && current_cursor_y == 1 && inv_icons[8].enabled)
    			{
    				cmd.UpdateCommand(8);
    			}
    			if(current_cursor_x == 1 && current_cursor_y == 0 && inv_icons[7].enabled)
    			{
    				cmd.UpdateCommand(7);
    			}
    			if(current_cursor_x == 0 && current_cursor_y == 3 && inv_icons[6].enabled)
    			{
    				cmd.UpdateCommand(6);
    			}
    			if(current_cursor_x == 0 && current_cursor_y == 2 && inv_icons[5].enabled)
    			{
    				cmd.UpdateCommand(5);
    			}
    			if(current_cursor_x == 0 && current_cursor_y == 1 && inv_icons[4].enabled)
    			{
    				cmd.UpdateCommand(4);
    			}
    			if(current_cursor_x == 0 && current_cursor_y == 0 && inv_icons[3].enabled)
    			{
    				cmd.UpdateCommand(3);
    			}
    		}

    		for(int i = 0; i < equip_selection.Length; i++)
    		{
    			equip_selection[i].transform.position = equip_selc_pos[i];
    		}
    	}
    	else
    	{
            if(Screen.currentResolution.width == 1920)
            {
    		  cursor_pos = new Vector3(960,540,0);
            }
            if(Screen.currentResolution.width == 800)
            {
              cursor_pos = new Vector3(400,300,0);
            }
    	}
    }

    void Move(int next_pos)
    {
        if(Screen.currentResolution.width == 1920)
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

        if(Screen.currentResolution.width == 800)
        {
            if(transform.position.x > inventory_xpos[next_pos]+400 && state > 0)
            {
                inventory_pos.x-=8*state;
            }
            else if(transform.position.x < inventory_xpos[next_pos]+400 && state == 0)
            {
                inventory_pos.x+=16;
            }
            else
            {
                moving = false;
            }
        }
    }

    public void UpdateAmount(int whichtxt, int howmuch)
    {
    	if(whichtxt == 0)
    	{
    		shield_amount.GetComponent<TextMeshProUGUI>().text = "x" + howmuch;
    	}
    	if(whichtxt == 1)
    	{
    		fire_charges.GetComponent<TextMeshProUGUI>().text = "x" + howmuch;
    	}
    	if(whichtxt == 2)
    	{
    		//key amount here
    		fire_charges.GetComponent<TextMeshProUGUI>().text = "x" + howmuch;
    	}
    }
}