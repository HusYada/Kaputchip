using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickup : MonoBehaviour
{
    public GameObject UI_Shields;
    public GameObject Command_Log;
    public inventory inv;
    public int which_pickup;            // 0 = triple shield, 1 = fire extinguisher, 2 = key, 3 = chip
    private int shields = 3;
    public AudioClip gain_sfx;
    public AudioSource aud;

    void OnTriggerEnter(Collider col) 
	{
        // Triple Shield
		if(which_pickup == 0 && col.gameObject.tag == "Player" && shields > 0) 
		{
            UI_Shields.GetComponent<ui_shield>().GainShield(UI_Shields.GetComponent<ui_shield>().shields_current);
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            shields--;
            //Command_Log.GetComponent<cmd_log>().playtext = true;
            Command_Log.GetComponent<cmd_log>().UpdateCommand(0);
            inv.inv_icons[1].enabled = true;
            inv.UpdateAmount(which_pickup, UI_Shields.GetComponent<ui_shield>().shields_current);
		}

        // Hus - i can definitely clean up the code below, but for now i will not

        // Fire Ext
        if(which_pickup == 9 && col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
            inv.inv_icons[2].enabled = true;
            //Command_Log.GetComponent<cmd_log>().UpdateCommand(1);
            inv.UpdateAmount(1, 3);
        }
        // Key
        if(which_pickup == 2 && col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
            inv.inv_icons[0].enabled = true;
            Command_Log.GetComponent<cmd_log>().UpdateCommand(2);
        }
        // Chip
        if(which_pickup > 2 && col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
            inv.inv_icons[which_pickup].enabled = true;
            Command_Log.GetComponent<cmd_log>().UpdateCommand(which_pickup);
        }
        // Any Pickup
        if(which_pickup > 0 && col.gameObject.tag == "Player") 
        {
            aud.clip = gain_sfx;
            aud.Play();
        }
	}
}
