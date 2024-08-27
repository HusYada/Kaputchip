using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class camera_titleswoosh : MonoBehaviour
{

	public CinemachineVirtualCamera primaryCamera;
	public CinemachineVirtualCamera[] virtualCameras;
	public CinemachineVirtualCamera targetCamera;
	public GameObject campos;
	public Vector3 startthegamepos;

    void Start()
    {
        SwitchToCamera(primaryCamera);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
		{
			//SceneManager.LoadScene(level);
			SwitchToCamera(targetCamera);

			primaryCamera.Priority = 2;
			targetCamera.Priority = 12;
		}

		if(campos.transform.position == startthegamepos)
		{
			SceneManager.LoadScene("main_game");
		}
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
    	foreach ( CinemachineVirtualCamera camera in virtualCameras )
    	{
    		camera.enabled = camera == targetCamera;
    	}
    }
}

