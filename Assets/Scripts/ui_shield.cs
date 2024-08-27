// https://docs.unity3d.com/ScriptReference/Transform.GetChild.html
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_shield : MonoBehaviour
{
	public int shields_current;
	public int shields_max_amount;
	public float[] shield_xpos;		// Where the shields are positioned on the x-axis of the UI when active
	private Vector3[] shield_pos;	// General position
	public AudioClip gain_sfx;
    public AudioClip lose_sfx;
	private AudioSource aud;

    void Start()
    {
    	aud = GetComponent<AudioSource>();
    	shield_pos = new Vector3[shields_max_amount];

    	for(int i = 0; i < shields_max_amount; i++)
    	{
    		shield_pos[i] = this.gameObject.transform.GetChild(i).transform.position;
    	}
    }

    void Update()
    {
    	for(int i = 0; i < shields_max_amount; i++)
    	{
    		this.gameObject.transform.GetChild(i).transform.position = shield_pos[i];
    	}
    }

    public void GainShield(int shield_number)
    {
    	if(shields_current < shields_max_amount)
    	{
    		shield_pos[shield_number].x = shield_xpos[shield_number];
    		shields_current++;
    		aud.clip = gain_sfx;
	        aud.Play();
    	}
    }

    public void LoseShield(int shield_number)
    {
    	if(shields_current > 0)
    	{
    		shield_pos[shield_number-1].x = -150;
    		shields_current--;
            aud.clip = lose_sfx;
            aud.Play();
    	}
        if(shield_number == 0)
        {
            //SceneManager.LoadScene("game_over");
        }
    }
}