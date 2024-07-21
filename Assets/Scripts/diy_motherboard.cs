using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class diy_motherboard : MonoBehaviour
{
    Vector3 offset;
    public string destinationTag = "DropArea";
    private Rigidbody rb;

    void Start()
    {
    	rb = GetComponent<Rigidbody>();
    }
 
    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        rb.useGravity = false;
    }
 
    void OnMouseDrag()
    {
        //transform.position = MouseWorldPosition() + offset;
        //transform.position.y = MouseWorldPosition().y + offset.y + 10f;
        transform.position = new Vector3(
        	MouseWorldPosition().x + offset.x + 0f,
        	-12f,
        	MouseWorldPosition().z + offset.z + 0f);
    }
 
    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if(Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if(hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
            }
        }
        transform.GetComponent<Collider>().enabled = true;
        rb.useGravity = true;
    }
 
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}