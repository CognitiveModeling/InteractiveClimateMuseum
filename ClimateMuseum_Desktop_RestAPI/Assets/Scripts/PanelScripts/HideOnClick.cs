using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script hides the start screen of a panel if the user clicks on it.
// It is assigned to the StartScreen of each panel in the editor.

public class HideOnClick : MonoBehaviour
{
    // the corresponding tab panel that appears instead of start screen
    public GameObject TabPanel;

    // if user clicks onto the start screen, start screen is deactivated and tab panel is activated
    void OnMouseDown()
    {
    	gameObject.SetActive(false);
	    TabPanel.SetActive(true);
    }
}