using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyme : MonoBehaviour
{
	public int seconds = 10;
	private IEnumerator d;

    void Start()
    {
    	d = Destroy_Me(seconds);
        StartCoroutine(d);
    }

    IEnumerator Destroy_Me(int s) 
    {
    	yield return new WaitForSeconds(s);
    	Destroy(this.gameObject);
    }
}
