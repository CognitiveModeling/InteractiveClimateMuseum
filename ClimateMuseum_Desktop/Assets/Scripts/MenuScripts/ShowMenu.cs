using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script displays the menu canvas if the user presses the "M" key.
// It is assigned to the player in the editor.

public class ShowMenu : MonoBehaviour
{
    // the menu canvas
    public GameObject menu;
    
    void Update()
    {
        // if "M" is pressed, menu canvas is activated
        if (Keyboard.current.mKey.wasPressedThisFrame)
            
            // if menu is not active, activate it
            if (!menu.activeSelf) {
                menu.SetActive(true);
            }
            
            // if menu is already active, close it
            else {
                menu.SetActive(false);
            }
    }
}
