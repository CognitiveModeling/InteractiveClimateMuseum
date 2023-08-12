using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    public float scrollSpeed;
    public GameObject playerCamera;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }
        else if(!Keyboard.current.leftShiftKey.isPressed && Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        if (Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * movementSpeed;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            float horiztonalRotation = rotateSpeed * Mouse.current.delta.ReadValue().x;
            this.transform.Rotate(0, horiztonalRotation, 0);

            float verticalRotation = rotateSpeed * Mouse.current.delta.ReadValue().y;
            playerCamera.transform.Rotate(-verticalRotation,0,0);
        }

    }
}
