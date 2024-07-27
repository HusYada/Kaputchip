using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cmd_log : MonoBehaviour
{

	public string[] all_dialog_text;
	public int whichline;				// The string that will appear
    public bool playtext;
	private float typespd = 0.02f;
	private TMP_Text txt_line;
	private IEnumerator ts;
	private AudioSource source;

    void Start()
    {
        txt_line = GetComponent<TMPro.TextMeshProUGUI>();
        source = GetComponent<AudioSource>();
        ts = Type_Text(all_dialog_text[whichline]);
        if(Input.GetKeyDown("q"))
        {
            playtext = false;
            StartCoroutine(ts);
            whichline++;            // temporary
        }
    }

    void Update()
    {   
    	ts = Type_Text(all_dialog_text[whichline]);
        if(Input.GetKeyDown("q") || playtext)
        {
        	StartCoroutine(ts);
        	whichline++;			// temporary
        }
    }

    private IEnumerator Type_Text (string t) 
    {
    	txt_line.text = "";
		foreach (char letter in t.ToCharArray()) 
		{
			txt_line.text += letter;
            source.Play();
			yield return new WaitForSeconds(typespd);
		}
	}
}
