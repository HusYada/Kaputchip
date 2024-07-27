using UnityEngine;

public class range : MonoBehaviour
{
	public bool inrange;

	void OnTriggerEnter(Collider col) 
	{
		if(col.gameObject.tag == "Player") 
		{
			inrange = true;
		}
	}

	void OnTriggerStay(Collider col) 
	{
		if(col.gameObject.tag == "Player") 
		{
			inrange = true;
		}
	}

	void OnTriggerExit(Collider col) 
	{
		if(col.gameObject.tag == "Player") 
		{
			inrange = false;
		}
	}
}