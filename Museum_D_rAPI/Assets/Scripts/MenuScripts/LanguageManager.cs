using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// When loading the scene Museum, this script destroys the texts in the language that was not selected by the user in the scene Menu.
// It is assigned to the object Panels and Table in the editor.

public class LanguageManager : MonoBehaviour
{
    void Awake()
    {
        // for each component in Panels/Table and its children
        foreach (Component textComp in this.transform.GetComponentsInChildren<Component>(true))
        {
            if (textComp != null) {
                // if the component has the tag "DE" and English was selected or if the component has the tag "EN" and German was selected
                if ((textComp.CompareTag("DE")
                && LanguageController.language == "en")
                || (textComp.CompareTag("EN")
                && LanguageController.language == "de"))
                {
                    // destroy the component's game object
                    Destroy(textComp.gameObject);
                    Debug.Log("destroyed");
                }
            }
        }
    }
}
