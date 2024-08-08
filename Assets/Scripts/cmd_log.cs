using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cmd_log : MonoBehaviour
{

	public string[] all_dialog_text;
	public int whichline;				// The string that will appear
    public bool playtext;
    public bool typingended;
	private float typespd = 0.02f;
	private TMP_Text txt_line;
	private IEnumerator ts;
    public RectMask2D msk;
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
        if(playtext)
        {
        	StartCoroutine(ts);
            typingended = false;
            playtext = false;
        	//whichline++;			// temporary
            //whichline = 0;
        }
        if(Input.GetKeyDown("'") && typingended)
        {
            playtext = true;
        }
        if(Input.GetKeyDown("/"))
        {
            msk.enabled = !msk.enabled;
        }
    }

    public void UpdateCommand(int whichstring)
    {
        whichline = whichstring;
        playtext = true;
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
        typingended = true;
	}
}