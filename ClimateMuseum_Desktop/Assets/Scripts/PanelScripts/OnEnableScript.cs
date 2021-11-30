using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This helper script manages the en- and disabling of header and image in a panel tab.
// Thus, it is assigned to every tab (Key Dynamics, Potential Co-Benefits, ...) and Content object in a panel in the editor.

public class OnEnableScript : MonoBehaviour
{
    // an image and a header, assigned in the editor
	public GameObject image;
    public GameObject header;

    // image and header are activated
    public void OnEnable()
    {
        image.SetActive(true);
        header.SetActive(true);
    }

    // image and header are deactivated
    public void OnDisable()
    {
        image.SetActive(false);
        header.SetActive(false);
    }
}
