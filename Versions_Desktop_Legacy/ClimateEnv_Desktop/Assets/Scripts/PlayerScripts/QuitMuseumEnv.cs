using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitMuseumEnv : MonoBehaviour
{
    // the quit canvas
    public GameObject quitCanvas;

    void Update()
    {
        // if "Esc" is pressed, quit canvas is shown
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {

            ShowQuitCanvas();
        }
    }

    public void ShowQuitCanvas()
    {
        // quit canvas is shown
        // if menu is not active, activate it
        if (!quitCanvas.activeSelf)
        {
            this.gameObject.SetActive(false);
            quitCanvas.SetActive(true);
        }

        // if menu is already active, close it
        else
        {
            quitCanvas.SetActive(false);
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
