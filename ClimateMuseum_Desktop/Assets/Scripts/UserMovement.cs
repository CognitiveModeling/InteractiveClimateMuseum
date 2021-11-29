using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    public float scrollSpeed;
    public GameObject playerCamera;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }
        else if(Input.GetKey(KeyCode.UpArrow) && ! Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * movementSpeed;
        }

        if (Input.GetMouseButton(2))
        {
            float horiztonalRotation = rotateSpeed * Input.GetAxis("Mouse X");
            this.transform.Rotate(0, horiztonalRotation, 0);

            float verticalRotation = rotateSpeed * Input.GetAxis("Mouse Y");
            playerCamera.transform.Rotate(-verticalRotation,0,0);
        }

        if (Input.mouseScrollDelta != null)
        {
            transform.position += transform.TransformDirection(Vector3.forward)*Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed * 2.5f;
        }

    }
}
