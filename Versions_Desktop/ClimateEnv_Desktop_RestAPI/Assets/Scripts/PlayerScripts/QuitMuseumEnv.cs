using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics;

// This script quits the application and stops all coroutines and the RestAPI processes if the user presses 'Esc'.

public class QuitMuseumEnv : MonoBehaviour
{
    public EnvironmentUpdate environmentUpdate;
    //public BrowserSync browserSync;
    public GameObject quitCanvas;
    public RestAPICallTest restAPICall;

    void Update()
    {
        // if "Esc" is pressed, quit canvas is shown
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {

            //ShowQuitCanvas();
            QuitMuseum();
        }
    }


    public void QuitMuseum()
    {
        // in scene "Museum"
        if (environmentUpdate != null) {
            //environmentUpdate.setQuit();
            // If the simulator has been activated and the environment keeps refreshing:
            // Reset materials to baseline values when quitting the museum
            if (environmentUpdate.busy) {
                //environmentUpdate.setNotBusy();
                StopAllCoroutines();
                //browserSync.doResetMaterials();
            }
        }

        // kill rest api processes
        Process[] workers = Process.GetProcessesByName("electron-app");
        foreach (Process worker in workers)
        {
            //UnityEngine.Debug.Log(worker);
            worker.Kill();
        }
        UnityEngine.Debug.Log("Rest API processes killed");

        // in both scenes
        UnityEngine.Debug.Log("Museum has quit");
        Application.Quit();
    }


    public void ShowQuitCanvas()
    {
        // quit canvas is shown
        // if menu is not active, activate it
        if (!quitCanvas.activeSelf)
        {
            //this.gameObject.SetActive(false);
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
