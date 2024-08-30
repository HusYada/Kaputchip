using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camera_titleswoosh : MonoBehaviour
{

	public CinemachineVirtualCamera primaryCamera;
	public CinemachineVirtualCamera targetCamera;

	//public player player;
	public bool isSwitching = false;


    void Update()
    {
		if (isSwitching == false)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				primaryCamera.Priority = 2;
				targetCamera.Priority = 12;

				isSwitching = true;
			}
		}
    }
}

