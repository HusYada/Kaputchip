using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickup : MonoBehaviour
{
    public GameObject UI_Shields;
    [HideInInspector] public cmd_log cmd;
    [HideInInspector] public inventory inv;
    public int which_pickup;            // 0 = triple shield, 1 = fire extinguisher, 2 = key, 3 = chip
    private int shields = 3;
    public AudioClip gain_sfx;
    [HideInInspector] AudioSource aud;

    void Start()
    {
        //cmd = GameObject.Find("Command Text").GetComponent<cmd_log>();
        //inv = GameObject.Find("Inventory").GetComponent<inventory>();
        aud = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col) 
	{

        if(which_pickup != 0 && col.gameObject.tag == "Player")
        {
            cmd = GameObject.Find("Command Text").GetComponent<cmd_log>();
            inv = GameObject.Find("Inventory").GetComponent<inventory>();
        }

        // Triple Shield
		if(which_pickup == 0 && col.gameObject.tag == "Player" && shields > 0) 
		{
            cmd = GameObject.Find("Command Text").GetComponent<cmd_log>();
            inv = GameObject.Find("Inventory").GetComponent<inventory>();
            UI_Shields.GetComponent<ui_shield>().GainShield(UI_Shields.GetComponent<ui_shield>().shields_current);
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            shields--;
            cmd.GetComponent<cmd_log>().UpdateCommand(0);
            inv.inv_icons[1].enabled = true;
            inv.UpdateAmount(which_pickup, UI_Shields.GetComponent<ui_shield>().shields_current);
		}

        // Hus - i can definitely clean up the code below, but for now i will not

        // Fire Ext
        if(which_pickup == 9 && col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
            inv.inv_icons[2].enabled = true;
            inv.UpdateAmount(1, 3);
        }
        // Key
        if((which_pickup == 2 || which_pickup == 21) && col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
            inv.inv_icons[0].enabled = true;
            if(which_pickup == 2)
            {
                cmd.GetComponent<cmd_log>().UpdateCommand(2);
            }
            if(which_pickup == 21)
            {
                cmd.GetComponent<cmd_log>().UpdateCommand(33);
            }
        }
        // Chip
        if(which_pickup > 2 && which_pickup < 21 && col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
            inv.inv_icons[which_pickup].enabled = true;
            // Equipping Body First Time
            // if(inv.firstbody && (inv.inv_icons[3].enabled || inv.inv_icons[4].enabled || inv.inv_icons[5].enabled || inv.inv_icons[6].enabled))
            // {
            //     inv.map_sprite.sprite = inv.inv_icons[which_pickup].sprite;
            //     inv.equip_selc_pos[0].y = inv.cursory[which_pickup-3]+120+20;
            //     inv.firstbody = false;
            // }
            cmd.GetComponent<cmd_log>().UpdateCommand(which_pickup);
        }
        // Any Pickup
        if(which_pickup > 20 && col.gameObject.tag == "Player") 
        {
            aud.clip = gain_sfx;
            aud.Play();
        }
	}
}
