using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script manages the menu canvas in the entry scene "Menu" and in the scene "Museum" with the buttons Select Language and Quit and Explore, respectively.
// It is assigned to the Menu canvas in both scenes in the editor.

public class MenuManager : MonoBehaviour
{
    public BrowserSync browserSync;
    public GameObject quitCanvas;

    // If Explore button is clicked (in scene Museum), the menu is closed so the user comes back into the museum.
    public void ExploreMuseum()
    {
        this.gameObject.SetActive(false);
    }

    // If English/German button is clicked (in scene Menu), the Museum is loaded in the selected language.
    public void LoadGame(string language)
    {
        // save selected language as a static variable
        LanguageController.language = language;
        // load scene with selected language
        SceneManager.LoadScene("Museum");
    }

    // If quit button is clicked (in scene Museum), a log message about the quit museum is printed and the whole museum is quit.
    public void QuitMuseum()
    {
        // in scene "Museum"
        if (browserSync != null) {
            browserSync.setQuit();
            // If the simulator has been activated and the environment keeps refreshing:
            // Reset materials to baseline values when quitting the museum
            if (browserSync.getBusy()) {
                browserSync.setNotBusy();
                StopAllCoroutines();
                //browserSync.doResetMaterials();
            }
        }        

        // in both scenes
        Debug.Log("Museum has quit");
        Application.Quit();
    }

    public void ShowQuitCanvas()
    {
        // quit canvas is shown
        // if canvas is not active, activate it
        if (!quitCanvas.activeSelf)
        {
            this.gameObject.SetActive(false);
            quitCanvas.SetActive(true);
        }

        // if canvas is already active, close it
        else
        {
            quitCanvas.SetActive(false);
        }
    }
}
