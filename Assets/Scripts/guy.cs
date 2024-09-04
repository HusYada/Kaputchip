using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guy : MonoBehaviour
{
	public GameObject player;
	//public GameObject eyes;
	public mouse mos;
	public float speed;
	public float eyemin, eyemax;
	public Vector3 eyespd;
	public Animator anim;
	public bool ikActive;

	public GameObject eyeR;
	public GameObject eyeL;

    void Start()
    {
    	// something here

    }

    void Update ()
    {
		//transform.LookAt(player.transform);

		if(mos.behaviour == 1 && eyeR.transform.localScale.y > eyemin) 
		{
			eyeL.transform.localScale -= eyespd;
			eyeR.transform.localScale -= eyespd;
			//print("shrinking");
		} 
		if(mos.behaviour != 1 && eyeR.transform.localScale.y < eyemax)
		{
			eyeL.transform.localScale += eyespd;
			eyeR.transform.localScale += eyespd;
		}
		//OnAnimatorIK();
	}

// private void OnAnimatorIK(int layerIndex)
//     {
//         if(anim)
//         {
//         	print("hasanim");
//             if(ikActive)
//             {
//                 if(player!= null)
//                 {
//                 	print("playerisnotnull");
//                     if(mos.behaviour == 1 && eyeR.transform.localScale.y > eyemin) 
// 					{
// 						eyeL.transform.localScale -= eyespd;
// 						eyeR.transform.localScale -= eyespd;
// 						print("shrinking");
// 					} 
// 					if(mos.behaviour != 1 && eyeR.transform.localScale.y < eyemax)
// 					{
// 						eyeL.transform.localScale += eyespd;
// 						eyeR.transform.localScale += eyespd;
// 					}
//                 }
//             }
//         }
//     }
}