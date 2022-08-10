using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set2022 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public BrowserSyncTemperatureOnly browserSyncTemperatureOnly;

    void OnMouseDown()
    {
        thisBackground.GetComponent<Image>().color = Color.green;
        otherBackground.GetComponent<Image>().color = Color.grey;

        browserSyncTemperatureOnly.active2100 = false;

        browserSyncTemperatureOnly.set2022();
    }
}
