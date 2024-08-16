using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	// 0 = shooter, 1 = chaser
	public int enemytype;

	// The target the boss is going to shoot at
	public Transform player;

	// The projectile the boss is going to use
	public Rigidbody projectile;

	// The distance the boss can shoot at
	private float distance = 50.0f;

	// The power behind the projectile (can also be speed)
	public float speed = 30.0f;

	// Checks if the player is in the range of the boss
	private bool range = false;

    void Start()
    {
    	player = GameObject.Find("Player").transform;

    	if(enemytype == 0)
    	{
    		InvokeRepeating("Shoot", 0.5f, 0.5f);
    	}
    }

	void Update() 
	{
		// The range is set to be less than the distance
		range = Vector3.Distance (transform.position, player.position) < distance;

		// If the player is on the boss's range, the boss will look at the player
		if(range)
		{
			transform.LookAt(player);
			if(enemytype == 1)
    		{
    			transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    		}
		}
	}

	// Function for shooting
	void Shoot()
	{
		// If range exsists, it will instantiate an exsisting bullet and then add force that bullet so it will launch
		if(range)
		{
			
			Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);

			bullet.AddForce(transform.forward * speed, ForceMode.Impulse);

			// Destroy the instantiated bullet two seconds after it's launch
			Destroy (bullet.gameObject, 2);

			return;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player" && enemytype == 1)
		{
			//projectile.AddForceAtPosition(direction.normalized, transform.position);
		}
	}
}
