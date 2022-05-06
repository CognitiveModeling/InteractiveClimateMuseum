using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script manages the menu canvas in the entry scene "Menu" and in the scene "Museum" with the buttons Select Language and Quit and Explore, respectively.
// It is assigned to the Menu canvas in both scenes in the editor.

public class MenuManager : MonoBehaviour
{
    // two buttons for selecting languages
    public Button EnglishButton;
    public Button DeutschButton;

    // If Explore button is clicked (in scene Museum), the menu is closed so the user comes back into the museum.
    public void ExploreMuseum()
    {
        this.gameObject.SetActive(false);
    }

    // If English/German button is clicked (in scene Menu), the Museum is loaded in the selected language.
    public void LoadGame(string language)
    {
        // save selected language 
        LanguageController.language = language;
        // load scene with selected language
        SceneManager.LoadScene("Museum");
    }

    // If quit button is clicked (in scene Museum), a log message about the quit museum is printed and the whole museum is quit.
    public void QuitMuseum()
    {
        Debug.Log("Museum has quit");
        Application.Quit();
    }
}
