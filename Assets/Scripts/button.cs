using UnityEngine;

public class button : MonoBehaviour
{
	public bool active;

	void OnCollisionEnter(Collision col) 
	{
		if(col.gameObject.tag == "Player") 
		{
			active = true;
		}
	}

    void OnCollisionStay(Collision col) 
	{
		if(col.gameObject.tag == "Player") 
		{
			active = true;
		}
	}
}
