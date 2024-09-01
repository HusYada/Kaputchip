using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enem_spawner : MonoBehaviour
{
	public GameObject enem;
	public player plyr;
	public float Time_Until_Enemy_Spawn;
    public int second_passed = 0;
	private IEnumerator spawn_timer;

    void Start()
    {
        spawn_timer = SecondsPassed(1);
		StartCoroutine(spawn_timer);
    }

    void Update()
    {
        if(plyr.isInSecretRoom && second_passed > Time_Until_Enemy_Spawn)
        {
        	Instantiate(enem);
        	second_passed = 0;
        }
    }

    IEnumerator SecondsPassed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        second_passed++;
        StartCoroutine(SecondsPassed(1));
    }
}
