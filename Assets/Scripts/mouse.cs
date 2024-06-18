using UnityEngine;

public class mouse : MonoBehaviour
{
	public GameObject player;

    void Update()
    {
    	transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
    }
}
