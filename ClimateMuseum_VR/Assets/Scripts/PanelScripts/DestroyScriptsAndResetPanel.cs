using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyScriptsAndResetPanel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Delete))
        {
            //Destroy all scripts used to correctly initialize panel
            if (this.gameObject.GetComponent<PanelRenderer>())
            {
                Destroy(this.gameObject.GetComponent<PanelRenderer>());
                Component[] toDestroy = this.gameObject.GetComponentsInChildren<CorrectContentPosition>(true);
            
                foreach (Component obj in toDestroy)
                {
                    Destroy(obj);
                }
            }
            
         

            // Reset start screen and tab panel
            this.transform.Find("Start Screen").gameObject.SetActive(true);
            this.transform.Find("Tab Panel").gameObject.SetActive(false);

            // Reset toggles such that only first is active
            Transform chapter = this.transform.Find("Tab Panel").Find("Chapter Container");

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


                Transform tab = this.transform.Find("Tab Panel").Find("Chapter Container").GetChild(i).Find("Tab Container");
                bool inactive = true;

                for (int j = 0; j < tab.childCount; j++)
                {
                    // Only first active tab toggle is on                   
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


                // Destroy this script at last
                Destroy(this.gameObject.GetComponent<DestroyScriptsAndResetPanel>());
            }
        }
    }

}
