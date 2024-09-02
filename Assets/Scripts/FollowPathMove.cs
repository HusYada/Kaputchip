// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FollowPathMove : MonoBehaviour
// {
//     public GameObject prefab1; 
//     public GameObject prefab2; 
//     public GameObject[] pathPoints; 
//     public float speed;             
//     public float spawnInterval = 2f; 

//     private int currentPointIndex = 0; 
//     private bool toggle = true; 

//     void Start()
//     {
//         StartCoroutine(SpawnObjects());
//     }

//     IEnumerator SpawnObjects()
//     {
//         while (true)
//         {
//             GameObject selectedPrefab = toggle ? prefab1 : prefab2;

//             GameObject newObj = Instantiate(selectedPrefab, pathPoints[0].transform.position, Quaternion.identity);
//             StartCoroutine(MoveAlongPath(newObj));

//             toggle = !toggle;

//             yield return new WaitForSeconds(spawnInterval);
//         }
//     }

//     IEnumerator MoveAlongPath(GameObject obj)
//     {
//         int currentPointIndex = 0;

//         while (currentPointIndex < pathPoints.Length)
//         {
//             Vector3 currentPosition = obj.transform.position;
//             Vector3 targetPosition = pathPoints[currentPointIndex].transform.position;

//             while (Vector3.Distance(currentPosition, targetPosition) > 0.1f)
//             {
//                 obj.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
//                 yield return null;
//                 currentPosition = obj.transform.position;
//             }
//           
//             currentPointIndex++;
//         }

//         Destroy(obj);
//     }
// }




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathMove : MonoBehaviour
{
    public GameObject prefab1; 
    public GameObject prefab2;
    public GameObject[] pathPoints; 
    public float speed;             
    public float spawnInterval = 2f; 
    public float floatAmplitude = 0.5f; 
    public float floatFrequency = 1f; 
    public float lifeTime = 15f; 

    private bool toggle = true; 

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject selectedPrefab = toggle ? prefab1 : prefab2;

            GameObject newObj = Instantiate(selectedPrefab, pathPoints[0].transform.position, Quaternion.identity);

            StartCoroutine(MoveAlongPath(newObj));

            Destroy(newObj, lifeTime);

            toggle = !toggle;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveAlongPath(GameObject obj)
    {
        int currentPointIndex = 0;
        float randomOffset = Random.Range(0f, Mathf.PI * 2f); 

        while (currentPointIndex < pathPoints.Length)
        {
            Vector3 startPosition = obj.transform.position;
            Vector3 targetPosition = pathPoints[currentPointIndex].transform.position;

            float initialY = startPosition.y;

            while (Vector3.Distance(startPosition, targetPosition) > 0.1f)
            {
                float yOffset = Mathf.Sin(Time.time * floatFrequency + randomOffset) * floatAmplitude;

                Vector3 newPosition = Vector3.MoveTowards(startPosition, targetPosition, speed * Time.deltaTime);
                newPosition.y = initialY + yOffset;

                obj.transform.position = newPosition;

                yield return null;

                startPosition = obj.transform.position;
            }
            currentPointIndex++;
        }

        //Destroy(obj);
    }
}
