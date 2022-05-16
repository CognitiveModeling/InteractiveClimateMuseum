using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script manages the user's control of the player and is assigned to the Player in the editor.
// TO DO: WASD, middle mouse key, rotation?

public class UserMovement : MonoBehaviour
{
    // speeds for the player's movement, rotation and scrolling
    public float movementSpeed;
    public float rotateSpeed;
    public float scrollSpeed;

    // the camera from the player's point of view
    public GameObject playerCamera;

    // Update is called once per frame
    void FixedUpdate()
    {
        // player movement forwards and backwards:
        // if user presses "Shift" and arrow key up, player position changes with 2.5 times the normal movement speed forwards
        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed && Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }
        // if user only presses arrow key up, player position changes with normal movement speed forwards
        else if (Keyboard.current.upArrowKey.isPressed && !Keyboard.current.leftShiftKey.isPressed || Keyboard.current.wKey.isPressed && !Keyboard.current.leftShiftKey.isPressed)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        // if user presses arrow key down, player position changes with normal movement speed backwards
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        // player movement left and right:
        // if user presses arrow key left, player position changes with normal movement speed to the left
        if ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && (!Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.dKey.isPressed))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        // if user presses arrow key right, player position changes with normal movement speed to the right
        else if ((Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) && (!Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.aKey.isPressed))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * movementSpeed;
        }

        // if user presses right button of mouse and moves it, player rotates with given speed of rotation
        // Depending on the device, rotation by pressing the middle button is also possible (if (Mouse.current.middleButton.isPressed) {...} )
        // Then also the instruction text/image "How to move" in the Information panel has to be adjusted.
        if (Mouse.current.rightButton.isPressed || Mouse.current.middleButton.isPressed)
        {
            // player rotates horizontally ("looks left and right")
            float horizontalRotation = rotateSpeed * Input.GetAxis("Mouse X");
            this.transform.Rotate(0, horizontalRotation, 0);

            // player rotates vertically ("looks up and down")
            float verticalRotation = rotateSpeed * Input.GetAxis("Mouse Y");
            playerCamera.transform.Rotate(-verticalRotation, 0, 0);
            
            /*Vector3 EulerRot = playerCamera.transform.localRotation.eulerAngles;
            
            Debug.Log(EulerRot.x);
        
            playerCamera.transform.localRotation = Quaternion.Euler(
                                                    Mathf.Clamp(EulerRot.x - verticalRotation, -60, 60),
                                                    0,
                                                   0);
            */
        }

//Quaternion(0,0,0,1) euler: Vector3(0,0,0)

        // if user scrolls, player position changes with the current mouse scroll delta and 2.5 times the normal scroll speed forwards
        if (Mouse.current.scroll != null)
        {
                transform.position += transform.TransformDirection(Vector3.forward) * Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed * 2.5f;
        }
    }
}
