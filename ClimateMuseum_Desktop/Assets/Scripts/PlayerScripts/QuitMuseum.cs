using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitMuseum : MonoBehaviour
{

    void Update()
    {
        // if "Esc" is pressed, museum is quit
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            Debug.Log("Museum has quit");
            Application.Quit();
        }
    }


    /*public BrowserSync browserSync;

    public void quit()
    {
        browserSync.setQuit();
        // If the simulator has been activated and the environment keeps refreshing:
        // Reset materials to baseline values when quitting the museum
        if (browserSync.getBusy())
        {
            browserSync.setNotBusy();
            StopAllCoroutines();
            browserSync.doResetMaterials();
        }
        Debug.Log("Museum has quit");
        Application.Quit();
    }
    */

}
