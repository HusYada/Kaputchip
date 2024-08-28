using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatAndRotate : MonoBehaviour
{
    public float amplitude = 0.5f; 
    public float frequency = 1f; 

    public Vector3 rotationSpeed = new Vector3(0, 0, 0); 

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);

        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}