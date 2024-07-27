using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour {

	public string level;

	public void FixedUpdate() 
	{
		if (Input.GetKey("r")) 
		{
            SceneManager.LoadScene(level);
        }
	}
}