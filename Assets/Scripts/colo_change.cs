// https://docs.unity3d.com/ScriptReference/Renderer-material.html
// https://docs.unity3d.com/ScriptReference/Color.HSVToRGB.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colo_change : MonoBehaviour
{

	public GameObject model;
    public Material mat;
    private Slider slider;
    [HideInInspector]
    public float hue;


    void Start()
    {
        mat = model.GetComponent<Renderer>().material;
        slider = GetComponent<Slider>();
    }

    void Update()
    {
    	hue = slider.value;
        mat.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}