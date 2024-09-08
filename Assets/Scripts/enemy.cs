using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class enemy : MonoBehaviour
{
	// 0 = shooter, 1 = chaser, 2 = shooter on rails
	public int enemytype;

	// The target the boss is going to shoot at
	public Transform player;

	// The projectile the boss is going to use
	public Rigidbody projectile;

	// The distance the boss can shoot at
	public float distance;

	// The power behind the projectile (can also be speed)
	public float speed = 30.0f;

	// Shoots every n seconds
	public float shoottime;

	// Checks if the player is in the range of the boss
	private bool range = false;

	public GameObject targetpos;
	public List<GameObject> locations;
	public bool movingforward = true;
	public int currentpos = 1;

    void Start()
    {
    	player = GameObject.Find("Player").transform;
    	locations = GameObject.FindGameObjectsWithTag("E_Locations").ToList();
    	targetpos = locations[currentpos];

    	if(enemytype == 2 || enemytype == 3)
    	{
    		InvokeRepeating("Shoot", shoottime, shoottime);
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
			// if the enemy is a chaser
			if(enemytype == 1)
    		{
    			transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    		}
		}
		else
		{
			if(enemytype == 2)
    		{
				transform.LookAt(targetpos.transform.position);
				if (transform.position != locations[currentpos].transform.position) {
	            	targetpos.transform.position = locations[currentpos].transform.position;
		        } 
		        else // when reaching a way point
				{
					if(transform.position == locations[locations.Count - 1].transform.position)
		            {
		            	movingforward = false;
		            }
		            if(transform.position == locations[0].transform.position)
		            {
		            	movingforward = true;
		            }
					if(movingforward)
		            {
		            	currentpos++;
		            }
		            if(!movingforward)
		            {
		            	currentpos--;
		            }
				}
				
				// move the dude
				transform.position = Vector3.MoveTowards(transform.position, targetpos.transform.position, speed * Time.deltaTime);
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

			Debug.Log("SHOOT");

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
