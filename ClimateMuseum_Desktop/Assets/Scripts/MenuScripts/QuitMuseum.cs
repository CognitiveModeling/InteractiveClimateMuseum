using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMuseum : MonoBehaviour
{
    public BrowserSync browserSync;

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
        Debug.Log("Muesum has quit");
        Application.Quit();
    }
}
