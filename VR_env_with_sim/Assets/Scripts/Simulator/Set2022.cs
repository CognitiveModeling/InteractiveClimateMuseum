using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set2022 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public EnvironmentUpdate environmentUpdate;

    void OnMouseDown()
    {
        thisBackground.GetComponent<Image>().color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
        otherBackground.GetComponent<Image>().color = new Color(0.7887148f, 0.7900782f, 0.7924528f, 1f);

        environmentUpdate.active2100 = false;
        environmentUpdate.set2022();
    }
}
