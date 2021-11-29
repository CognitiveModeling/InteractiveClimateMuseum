using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnClick : MonoBehaviour
{
public GameObject TabPanel;
    void OnMouseDown ()
    {
    	gameObject.SetActive(false);
	TabPanel.SetActive(true);
    }
}