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
        thisBackground.GetComponent<Image>().color = Color.green;
        otherBackground.GetComponent<Image>().color = Color.grey;

        environmentUpdate.active2100 = false;
        environmentUpdate.set2022();
    }
}
