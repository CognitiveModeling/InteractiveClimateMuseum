using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set2100 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public EnvironmentUpdate environmentUpdate;

    public void set2100()
    {
        thisBackground.GetComponent<Image>().color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
        otherBackground.GetComponent<Image>().color = new Color(0.7887148f, 0.7900782f, 0.7924528f, 1f);
        environmentUpdate.active2100 = true;
        environmentUpdate.startReadOutAndApplyValues();
    }


    void OnMouseDown()
    {
        set2100();
    }
}
