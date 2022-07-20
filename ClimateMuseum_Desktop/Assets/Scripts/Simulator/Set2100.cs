using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set2100 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public BrowserSync browserSync;

    public void set2100()
    {
        thisBackground.GetComponent<Image>().color = Color.green;
        otherBackground.GetComponent<Image>().color = Color.grey;

        browserSync.active2100 = true;
        browserSync.apply();
    }


    void OnMouseDown()
    {
        set2100();
    }
}
