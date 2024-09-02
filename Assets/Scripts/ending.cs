using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
	public GameObject light1, light2, light3, overlay, overlay2, game_manager;

    public float Time_Until_Ending_Overlay;
    public int second_passed = 0;
	private IEnumerator timer;
	private AudioSource aud;
	public AudioClip victory;

    public bool isEnded;

    void Start()
    {
    	overlay = GameObject.Find("GameComplete_Text");
    	overlay2 = GameObject.Find("GameComplete_Text02");
    	light1 = GameObject.Find("Microwave Light1");
    	light2 = GameObject.Find("Microwave Light2");
    	light3 = GameObject.Find("Microwave Light3");
    	light1.GetComponent<Light>().enabled = true;
	    light2.GetComponent<Light>().enabled = true;
	    light3.GetComponent<Light>().enabled = true;

        timer = SecondsPassed(1);
		StartCoroutine(timer);
		aud = GetComponent<AudioSource>();

		game_manager = GameObject.Find("Game_Manager");
		for (int i = 0; i < game_manager.GetComponent<game>().UIelements.Length; i++)
        {
            game_manager.GetComponent<game>().UIelements[i].SetActive(false);
        }
    }

    void Update()
    {
        if(second_passed > Time_Until_Ending_Overlay)
        {
            //overlay.SetActive(true);

            if (!isEnded)
            {
                aud.clip = victory;
                aud.Play();
                overlay.GetComponent<TextMeshProUGUI>().enabled = true;
                overlay2.GetComponent<TextMeshProUGUI>().enabled = true;
                isEnded = true;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Title02");
            }
        }
    }


    IEnumerator SecondsPassed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        second_passed++;
        StartCoroutine(SecondsPassed(1));
    }
}
