using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotateSpeed;
    public float scrollSpeed;
    public GameObject playerCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            float horiztonalRotation = rotateSpeed * Input.GetAxis("Mouse X");
            this.transform.Rotate(0, horiztonalRotation, 0);

            //float verticalRotation = rotateSpeed * Input.GetAxis("Mouse Y");
            //playerCamera.transform.Rotate(-verticalRotation, 0, 0);
        }

        if (Input.mouseScrollDelta != null)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed * 2.5f;
        }
    }
}
