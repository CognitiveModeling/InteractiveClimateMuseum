using UnityEngine;
using System.Collections;

public class MouseViewPort : MonoBehaviour {

    public float ZValue = 5;


    void FixedUpdate()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ZValue);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        transform.position = cursorPosition;
        transform.LookAt(Camera.main.transform);
    }
}