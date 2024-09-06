using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class delete_bar : MonoBehaviour
{
    /*
	10 > 9
	0.95 > 1.45
	-1
	+0.5
	*/

    public float
    bar_start,
    bar_end,
    bar_startpos,
    bar_endpos,
    bar_spd,
    bar_scale;

    public GameObject mousywousy, fire_scene;
    public Transform progressBar, deleteGrab;
    public player plyr;
    public TMP_Text progress;

    public Material fireMaterial;
    public GameObject[] walls;

    public Animator hackerAnim;

    void Start()
    {
        plyr = GameObject.Find("Player").GetComponent<player>();
        mousywousy = GameObject.Find("Mouse Pointer (Desktop)");
        hackerAnim = GameObject.Find("NewMainCharacter@Sitting Idle").GetComponent<Animator>();
    }

    void Update()
    {
        if(mousywousy.GetComponent<mouse>().behaviour != 8 && mousywousy.GetComponent<mouse>().pushing_d_bar == false)
        {
            mousywousy.GetComponent<mouse>().behaviour = 8;
        }
        if (mousywousy.GetComponent<mouse>().pushing_d_bar == false && progressBar.transform.localScale.x < 10)
        {
            Vector3 scaleChange = new Vector3(bar_scale, 0, 0);
            //hmm
            //progressBar.transform.position = Vector3.MoveTowards(progressBar.transform.position,
            //new Vector3(bar_endpos, progressBar.transform.position.y, progressBar.transform.position.z), bar_spd * Time.deltaTime);
            progressBar.transform.localScale += scaleChange;
            float _value = (progressBar.transform.localScale.x * 2);
            SetDeleteGrabPosition(_value);
            progress.text = (int)_value + "%";
        }
        if (mousywousy.GetComponent<mouse>().pushing_d_bar == true && progressBar.transform.localScale.x > 0)
        {
            Vector3 scaleChange = new Vector3(bar_scale, 0, 0);
            //hmm
            //progressBar.transform.position = Vector3.MoveTowards(progressBar.transform.position,
            //new Vector3(bar_startpos, progressBar.transform.position.y, progressBar.transform.position.z), bar_spd * Time.deltaTime);
            progressBar.transform.localScale -= scaleChange;
            float _value = (progressBar.transform.localScale.x * 2);
            SetDeleteGrabPosition(_value);
            progress.text = (int)_value + "%";
        }
        if (progressBar.transform.localScale.x >= 10)
        {
            progressBar.transform.localScale = new Vector3(-1000, -1000, -1000); ;
            Instantiate(fire_scene);
            ChangeMaterial();
            hackerAnim.SetTrigger("playAnim");
            plyr.ShakeCamera(8, 1);
        }
    }

    private void SetDeleteGrabPosition(float x)
    {
        Vector3 pos = deleteGrab.localPosition;
        pos.x = -x;
        deleteGrab.localPosition = pos;
    }

    private void ChangeMaterial()
    {
        foreach (GameObject go in walls)
        {
            Material[] materials = go.GetComponent<MeshRenderer>().sharedMaterials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = fireMaterial;
            }
            go.GetComponent<Renderer>().sharedMaterials = materials;

            materials = go.GetComponent<Renderer>().materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = fireMaterial;
            }
        }
    }
}
