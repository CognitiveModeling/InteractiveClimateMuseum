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

    public void Start()
    {
        // 2100-button is green when game starts
        thisBackground.GetComponent<Image>().color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
    }

    // if 2100-button is clicked, environment changes
    void OnMouseDown()
    {
        set2100();
    }

    public void set2100()
    {
        // button is green when it is clicked
        thisBackground.GetComponent<Image>().color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
        
        // button is white if other button (2022) is clicked
        otherBackground.GetComponent<Image>().color = new Color(0.7887148f, 0.7900782f, 0.7924528f, 1f);

        // environment is 2100 and slider values are applied to environment (see EnvironmentUpdate.cs)
        environmentUpdate.active2100 = true;
        environmentUpdate.startReadOutAndApplyValues();
    }
    
}
