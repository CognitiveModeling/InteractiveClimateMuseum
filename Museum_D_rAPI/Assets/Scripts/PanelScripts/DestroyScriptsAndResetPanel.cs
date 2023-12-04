using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// This script destroys all scripts that are used to initialize the panels, if the user presses the Delete-key.
// It is not assigned to any object in the editor because it was only used for debugging purposes.

public class DestroyScriptsAndResetPanel : MonoBehaviour
{
    void Update()
    {
        // if user presses the Delete-key
        if (Keyboard.current.deleteKey.wasPressedThisFrame)
        {
            // destroy all scripts used to correctly initialize panel: PanelRenderer, each use of CorrectContentPosition
            if (this.gameObject.GetComponent<PanelRenderer>())
            {
                Destroy(this.gameObject.GetComponent<PanelRenderer>());
                Component[] toDestroy = this.gameObject.GetComponentsInChildren<CorrectContentPosition>(true);

                foreach (Component obj in toDestroy)
                {
                    Destroy(obj);
                }
            }

            // reset start screen and tab panel
            this.transform.Find("Start Screen").gameObject.SetActive(true);
            this.transform.Find("Tab Panel").gameObject.SetActive(false);

            // reset toggles such that only first (left-most) is active
            Transform chapter = this.transform.Find("Tab Panel").Find("Chapter Container");

            // for each general/chapter tab (General Info, Correlations):
            for (int i = 0; i < chapter.childCount; i++)
            {
                // only general information toggle is on
                if (i == 0)
                {
                    chapter.GetChild(i).GetComponent<Toggle>().isOn = true;

                }
                else
                {
                    chapter.GetChild(i).GetComponent<Toggle>().isOn = false;
                    chapter.GetChild(i).transform.Find("Tab Container").gameObject.SetActive(false);
                }

                // hold all child/sub tabs of the chapter in one Transform tab
                Transform tab = this.transform.Find("Tab Panel").Find("Chapter Container").GetChild(i).Find("Tab Container");
                bool inactive = true;

                // for each child/sub tab of General Info/Correlations:
                for (int j = 0; j < tab.childCount; j++)
                {
                    // only first active tab toggle is on                   
                    if (tab.GetChild(j).gameObject.activeSelf && inactive)
                    {
                        tab.GetChild(j).GetComponent<Toggle>().isOn = true;
                        inactive = false;
                    }
                    else
                    {
                        tab.GetChild(j).GetComponent<Toggle>().isOn = false;
                    }
                }

                // destroy this script at last
                Destroy(this.gameObject.GetComponent<DestroyScriptsAndResetPanel>());
            }
        }
    }

}
