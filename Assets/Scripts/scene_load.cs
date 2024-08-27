using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scene_load : MonoBehaviour {

	public string level;

	public void Level() 
	{    
        SceneManager.LoadScene(level);
	}

	public void LevelGo(string levelname) 
	{    
        SceneManager.LoadScene(levelname);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(level);
		}
	}
}