using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script manages the menu canvas with the buttons Quit, Explore (and Select Language).
// It is assigned to the Menu canvas in the editor.

public class MenuManager : MonoBehaviour
{
    // two buttons for selecting languages
    //public Button EnglishButton;
    //public Button DeutschButton;

    // If Explore button is clicked, the menu is closed so the user is back in the museum.
    public void ExploreMuseum()
    {
        this.gameObject.SetActive(false);
    }

    // If English button is clicked, the button is selected (colour change) and the English version of the museum is loaded (TODO).
    /*public void SelectEnglishLanguage()
    {
        EnglishButton.Select();
        // TODO:
        // load English Version ...
        // set URL in simulator to "en" ...
    }

    // If German button is clicked, the button is selected (colour change) and the German version of the museum is loaded (TODO).
    public void SelectGermanLanguage()
    {
        DeutschButton.Select();
        // TODO:
        // load German Version ...
        // set URL in simulator to "de" ...
    }
    */

    // If quit button is clicked, a log message about the quit museum is printed and the whole museum is quit
    public void QuitMuseum()
    {
        Debug.Log("Museum has quit");
        Application.Quit();
    }
}
