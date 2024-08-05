using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickup : MonoBehaviour
{
    public GameObject UI_Shields;
    //public GameObject UI_Shields_Number;
    public GameObject Command_Log;
    public int which_pickup;            // 0 = triple shield, 1 = fire extinguisher
    private int shields = 3;

    void OnTriggerEnter(Collider col) 
	{
        // Triple Shield
		if(which_pickup == 0 && col.gameObject.tag == "Player" && shields > 0) 
		{
            UI_Shields.GetComponent<ui_shield>().GainShield(UI_Shields.GetComponent<ui_shield>().shields_current);
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            shields--;
            //Command_Log.GetComponent<cmd_log>().playtext = true;
		}
        // Fire Ext
	}
}
