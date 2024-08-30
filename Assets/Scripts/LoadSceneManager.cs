using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    //[SerializeField] private GameObject loadingBarObject;
    [SerializeField] private GameObject[] objectsToHide;

    [Header("Scenes to Load")]
    [SerializeField] private  SceneField02 menuScene;
    [SerializeField] private SceneField02 mainScene;

    private List<AsyncOperation> scenestoLoad = new List<AsyncOperation>();

    public bool isStarted = false;

    private void Awake()
    {
        //loadingBarObject.SetActive(true);
    }

    private void Start()
    {
        scenestoLoad.Add(SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive));
    }

    private void Update()
    {
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                HideMenu();
                isStarted = true;
            }
        }
    }


    private void HideMenu()
    {
        for(int i =0; i<objectsToHide.Length;i++)
        {
            objectsToHide[i].SetActive(false);
        }

    }
}
