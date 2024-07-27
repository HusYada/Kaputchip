using UnityEngine;

public class spinny : MonoBehaviour
{

	[HideInInspector]
	public int yrotatespeed = 50;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * yrotatespeed, 0);
    }
}
