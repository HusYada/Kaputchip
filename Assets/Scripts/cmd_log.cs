using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cmd_log : MonoBehaviour
{

	public string[] all_dialog_text;
	private float typespd = 0.02f;
	private TMP_Text txt_line;
	private IEnumerator ts;
	private AudioSource source;

    void Start()
    {
        txt_line = GetComponent<TextMeshProUGUI>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {   
    	ts = Type_Text(all_dialog_text[0]);
        if(Input.GetKeyDown("p"))
        {
        	//print(all_dialog_text[0]);
        	StartCoroutine(ts);
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
