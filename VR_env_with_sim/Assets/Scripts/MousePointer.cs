using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public float ZValue = 5;
    public Camera cam;


    void FixedUpdate()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ZValue);
        Vector3 cursorPosition = cam.ScreenToWorldPoint(cursorPoint);
        
        //transform.position = cursorPosition;
        //transform.LookAt(cam.transform);
    }
}
