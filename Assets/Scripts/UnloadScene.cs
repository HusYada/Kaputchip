using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour
{
    [SerializeField] SceneField02[] scenesToUnload;

    public GameObject targetCamera;
    public Vector3 targetPosition;

    private void Update()
    {
        if(targetCamera.transform.position == targetPosition)
        {
            UnloadScenes();
        }
    }

    private void UnloadScenes()
    {
        for(int i=0;i<scenesToUnload.Length;i++)
        {
            for(int j=0;j<SceneManager.sceneCount;j++)
            {
                Scene loadedscene = SceneManager.GetSceneAt(j);
                if(loadedscene.name == scenesToUnload[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(scenesToUnload[i]);
                }
            }
        }
    }
}
