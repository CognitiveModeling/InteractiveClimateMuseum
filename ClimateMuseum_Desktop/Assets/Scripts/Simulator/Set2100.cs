using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set2100 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public BrowserSyncTemperatureOnly browserSyncTemperatureOnly;

    public void set2100()
    {
        thisBackground.GetComponent<Image>().color = Color.green;
        otherBackground.GetComponent<Image>().color = Color.grey;

        browserSyncTemperatureOnly.active2100 = true;
        browserSyncTemperatureOnly.apply();
    }


    void OnMouseDown()
    {
        set2100();
    }
}
