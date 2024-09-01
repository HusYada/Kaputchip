using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorTrigger : MonoBehaviour
{
    public GameObject indicatorCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            indicatorCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            indicatorCanvas.SetActive(false);
        }
    }
}
