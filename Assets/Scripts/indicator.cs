using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class indicator : MonoBehaviour
{

	public GameObject player, enemy, pointer;
	public string level;
	private KeyCode
    k_left = KeyCode.A,
    k_right = KeyCode.D,
    k_up = KeyCode.W,
    k_down = KeyCode.S,
    k_reset = KeyCode.R;

    public int px, py;
    public Vector3 v;
    public float spd, ix, iy;
    public TMP_Text distance;
    public Sprite[] indicator_icons; // casual, uh oh, oh no!!!
    private Rigidbody rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
    	float dist = Vector3.Distance (player.transform.position, enemy.transform.position);

        Movement();
        Distance_Check(dist);

        if(player.transform.position.x < enemy.transform.position.x) {
        	ix = 40;
        }
        if (player.transform.position.x > enemy.transform.position.x) {
        	ix = 1920-40;
        }

        if (player.transform.position.z < enemy.transform.position.z) {
        	iy = 540 - (dist * dist * 25);
        }

        if (player.transform.position.z > enemy.transform.position.z) {
        	iy = 540 + (dist * dist * 25);
        }

        pointer.GetComponent<RectTransform>().position = new Vector3(ix, iy, 0);

        if(dist > 4) {
        	pointer.GetComponent<Image>().sprite = indicator_icons[0];
        }
        if(dist < 4 && dist > 2) {
        	pointer.GetComponent<Image>().sprite = indicator_icons[1];
        }
        if(dist < 2 && dist > 0) {
        	pointer.GetComponent<Image>().sprite = indicator_icons[2];
        }

        if (Input.GetKey(k_reset))
        {
            SceneManager.LoadScene(level);
        }
    }

    void Movement()
    {
    	if(Input.GetKey(k_left)) { px = -1; } else if(Input.GetKey(k_right)) { px = 1; } else { px = 0; }
		if(Input.GetKey(k_up)) { py = 1; } else if(Input.GetKey(k_down)) { py = -1; } else { py = 0; }
		v = new Vector3(px, 0, py);
		rb.MovePosition(rb.position + v * spd * Time.deltaTime);
    }
    void Distance_Check(float disty)
    {
        distance.text = "Distance: " + disty;
        pointer.GetComponent<RectTransform>().sizeDelta = new Vector2(170 - disty * 15, 170 - disty * 15);
    }
}