using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_collision : MonoBehaviour
{
	public ui_shield noshieldsforu;

    void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.tag == "Player")
        {
            noshieldsforu.LoseShield(noshieldsforu.shields_current);
        }
    }
}