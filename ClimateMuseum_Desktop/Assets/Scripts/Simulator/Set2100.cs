using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set2100 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public EvironmentUpdate evironmentUpdate;

    public void set2100()
    {
        thisBackground.GetComponent<Image>().color = Color.green;
        otherBackground.GetComponent<Image>().color = Color.grey;

        evironmentUpdate.active2100 = true;
    }


    void OnMouseDown()
    {
        set2100();
    }
}
