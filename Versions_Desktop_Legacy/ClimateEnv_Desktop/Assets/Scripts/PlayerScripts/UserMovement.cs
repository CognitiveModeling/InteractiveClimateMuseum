using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script manages the user's control of the player and is assigned to the Player in the editor.

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
            Vector3 EulerRot = playerCamera.transform.localRotation.eulerAngles;
            
            // vertical rotation is limited to floor (90 deg) and ceiling (270 deg)
            if ((EulerRot.x <= 90 && EulerRot.x >= 0) || (EulerRot.x <= 360 && EulerRot.x >= 270))
            {
                playerCamera.transform.localRotation = Quaternion.Euler(EulerRot.x - verticalRotation, 0, 0);
            }
            else
            {
                playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            
        }

        // if user scrolls, player position changes with the current mouse scroll delta and 2.5 times the normal scroll speed forwards
        if (Mouse.current.scroll != null)
        {
                transform.position += transform.TransformDirection(Vector3.forward) * Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed * 2.5f;
        }
        
    }
}
