using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

	//public GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
       // gm = gm.GetComponent<game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) 
	{
		if(col.gameObject.tag == "Player") 
		{
			Destroy(this);

		}
	}
}
