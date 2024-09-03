using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScene : MonoBehaviour
{
    public SceneField02 titleScene;


    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ReturntoTitle()
    {
        SceneManager.LoadScene(titleScene);
    }
}
