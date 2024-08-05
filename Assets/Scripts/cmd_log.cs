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
	private AudioSource aud;

    void Start()
    {
        txt_line = GetComponent<TMPro.TextMeshProUGUI>();
        aud = GetComponent<AudioSource>();
        ts = Type_Text(all_dialog_text[whichline]);
    }

    void Update()
    {   
    	ts = Type_Text(all_dialog_text[whichline]);
        if(Input.GetKeyDown("o"))
        {
        	StartCoroutine(ts);
        	//whichline++;			// temporary
            whichline = 0;
        }
    }

    private IEnumerator Type_Text (string t) 
    {
    	txt_line.text += "\n> ";
		foreach (char letter in t.ToCharArray()) 
		{
			txt_line.text += letter;
            aud.Play();
			yield return new WaitForSeconds(typespd);
		}
	}
}
