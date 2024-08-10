using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solitaire : MonoBehaviour
{
	public Renderer[] walls;
	// when the backpanel is destoryed, script needs to stop checking to update the material
	public Renderer backpanel;
    public bool backpanel_broke;
	// public GameObject[] icons;
	//public GameObject main_floor;
	public GameObject solitaire_floor;
	//public Transform targetfloorpos;
	public float floor_y;
	public float speed;
	public bool raisefloor;
	//public Material original_mat;
	public Material solitaire_mat;
	public cmd_log cmd;

    void Start()
    {
        //BeginGame();
    }

    void Update()
    {
        //if(raisefloor && solitaire_floor.transform.position.y < floor_y)
        if(raisefloor && solitaire_floor.transform.localScale.y < floor_y)
        {
        	//targetfloorpos.position = new Vector3(solitaire_floor.transform.position.x, solitaire_floor.transform.position.y + floor_y, solitaire_floor.transform.position.z);
        	//main_floor.GetComponent<BoxCollider>().enabled = true;
        	solitaire_floor.transform.localScale = new Vector3(solitaire_floor.transform.localScale.x, solitaire_floor.transform.localScale.y + speed, solitaire_floor.transform.localScale.z);
        }

        //solitaire_floor.transform.position = Vector3.MoveTowards(solitaire_floor.transform.position, targetfloorpos.position, speed * Time.deltaTime);
    }

    public void BeginGame()
    {
    	for(int i = 0; i < walls.Length; i++)
    	{
    		walls[i].material = solitaire_mat;
    	}
        if(!backpanel_broke)
        {
            backpanel.material = solitaire_mat;
        }
    	// for(int i = 0; i < icons.Length; i++)
    	// {
    	// 	icons[i].GetComponent<MeshRenderer>().enabled = false;
    	// }

    	cmd.UpdateCommand(25);
    	raisefloor = true;
    	//main_floor.GetComponent<BoxCollider>().enabled = false;
    	solitaire_floor.GetComponent<MeshRenderer>().enabled = true;
    }
}
