using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script quits the museum if the user clicks the quit button in the left upper corner.
// It is assigned to the Quit Button in the editor.

public class QuitMuseum : MonoBehaviour
{
    // If quit button is clicked, a log message about the quit museum is printed and the whole museum is quit
    public void quit()
    {
        Debug.Log("Museum has quit");
        Application.Quit();
    }
}