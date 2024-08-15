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
        if(raisefloor && solitaire_floor.transform.localScale.y < floor_y)
        {
        	solitaire_floor.transform.localScale = new Vector3(solitaire_floor.transform.localScale.x, solitaire_floor.transform.localScale.y + speed, solitaire_floor.transform.localScale.z);
        }
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

    	cmd.UpdateCommand(25);
    	raisefloor = true;
    	solitaire_floor.GetComponent<MeshRenderer>().enabled = true;
    }
}
