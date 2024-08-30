using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_collision : MonoBehaviour
{
	public ui_shield noshieldsforu;
    public mouse ms;

    void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.tag == "Player")
        {
            noshieldsforu.LoseShield(noshieldsforu.shields_current);
        }
        if(col.gameObject.tag == "ExitButton")
        {
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "Delete_Handle")
        {
            ms.pushing_d_bar = true;
            ms.mouse_model.enabled = false;
            ms.mouse_hand1.enabled = true;
            ms.mouse_hand2.enabled = true;
        }
    }
}