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
    // the quit canvas
    public GameObject quitCanvas;
    // the info canvas
    public GameObject infoCanvas;

    void Update()
    {
        /*
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
        */

        // if "Esc" is pressed, quit canvas is activated
        if (Keyboard.current.escapeKey.wasPressedThisFrame)

            // if quit canvas is not active, activate it
            if (!quitCanvas.activeSelf)
            {
                menu.SetActive(false);
                infoCanvas.SetActive(false);
                quitCanvas.SetActive(true);
            }
        
        // if "I" is pressed, info canvas is activated
        if (Keyboard.current.iKey.wasPressedThisFrame)

            // if info canvas is not active, activate it
            if (!infoCanvas.activeSelf)
            {
                menu.SetActive(false);
                quitCanvas.SetActive(false);
                infoCanvas.SetActive(true);
            }

            // if quit canvas is already active, close it
            else
            {
                infoCanvas.SetActive(false);
            }
    }
}
