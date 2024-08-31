using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class game : MonoBehaviour
{
	//public TMP_Text shields;
	//public Image key;
	// 800 x 600 @ 60.0524401590121Hz

	public GameObject canvo;
    public Camera cam;
    public GameObject commandlogmask;
    public GameObject hpbar;
    public TMP_Text cmd_tex;
    public GameObject anothercmdtex1pos;
    public TMP_Text anothercmdtex1;
    public TMP_Text anothercmdtex2;
    public TMP_Text inv_text;

    [Header("Player & Mouse")]
    public player player;
    public mouse mouse;

    [Header("Canvas")]
    public GameObject[] UIelements;

    public float waitingTime;
    public bool isStarted;

    void Start()
    {
        // Hus - Removed this for now, should be kept unchecked in the Inspector?
        //player.CanCtrl = false;

        for(int i = 0;i < UIelements.Length;i++)
        {
            UIelements[i].SetActive(false);
        }

        //shields = GetComponent<TMP_Text>();

        // if using crt 800x600 resolution
        if(Screen.currentResolution.width == 800)
        {
            Vector3 cmdcrtpos = new Vector3(1, -582.2082f);
            Vector2 cmdcrtscl = new Vector3(1935, 208.279f);
            Vector3 hppos = new Vector3(-460, 660);
            Vector3 hpscl = new Vector3(6, 6);
            Vector3 cmdhelptextpos = new Vector3(-679, -680.0998f);

        	//print("working!!");
        	canvo.GetComponent<CanvasScaler>().scaleFactor = 0.42f;
            //cam.rect = new Rect(0.85f, 0.85f, 0.15f, 1.0f);
            cam.rect = new Rect(0.75f, 0.75f, 0.25f, 0.25f);
            commandlogmask.GetComponent<RectTransform>().localPosition = cmdcrtpos;
            commandlogmask.GetComponent<RectTransform>().sizeDelta = cmdcrtscl;
            hpbar.GetComponent<RectTransform>().localPosition = hppos;
            hpbar.GetComponent<RectTransform>().localScale = hpscl;
            cmd_tex.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 72;
            anothercmdtex1pos.GetComponent<RectTransform>().localPosition = cmdhelptextpos;
            anothercmdtex1.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 56;
            anothercmdtex2.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 72;
            inv_text.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 72;
        }
    }

    private void Update()
    {
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(StartGame());
                isStarted = true;
            }
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(waitingTime);

        player.CanCtrl = true;

        for (int i = 0; i < UIelements.Length; i++)
        {
            UIelements[i].SetActive(true);
        }

    }
}