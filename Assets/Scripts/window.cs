// https://docs.unity3d.com/ScriptReference/Component.GetComponentsInChildren.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class window : MonoBehaviour
{
	//public Transform this_position;
	public Transform[] window_locations;
    public TMP_Text bar_text;
	public float speed;
	public int action;
	public GameObject[] wsp;
    public int randopos;

    // Ads
    public bool isanad;
    public int randoad;
    public Material[] ad_materials;
    public GameObject window_front, window_back;

    // privates
    private float velocity;
	private Vector3 distance;
	private Rigidbody rb;
    private destroyme cya;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
        cya = GetComponent<destroyme>();
		// Window_Spawn_Positions
        wsp = GameObject.FindGameObjectsWithTag("Window_Pos");
        randopos = (int)Mathf.Round(Random.Range(0, wsp.Length));

        // Set Ad Material
        if(isanad)
        {
            randoad = (int)Mathf.Round(Random.Range(0, ad_materials.Length));
            window_front.GetComponent<Renderer>().material = ad_materials[randoad];
            window_back.GetComponent<Renderer>().material = ad_materials[randoad];
            cya.enabled = true;
        }
	}

    void Update()
    {
    	if(action == 0 && gameObject.name != "Window2_1(Clone)")
    	{
    		Move();
    	}
        if (action == 0 && gameObject.name != "shield_window(Clone)")
        {
            Move();
        }
    }
    void Move()
    {
    	distance = wsp[randopos].transform.position - transform.position;
    	velocity = distance.magnitude / speed;
    	rb.velocity = distance / Mathf.Max(velocity, Time.deltaTime);
    }
}
