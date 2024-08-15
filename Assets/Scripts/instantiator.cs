using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiator : MonoBehaviour
{

	public GameObject game_object;

    void Start()
    {
        Instantiate(game_object, transform.position, Quaternion.identity);
    }
}
