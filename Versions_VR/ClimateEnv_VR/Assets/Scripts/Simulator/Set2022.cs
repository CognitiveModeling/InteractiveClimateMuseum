using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class Set2022 : MonoBehaviour
{
    public GameObject thisBackground;
    public GameObject otherBackground;
    public EnvironmentUpdate environmentUpdate;
    
    // VR
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean interactAction;
    public GameObject controller;

    public void Start()
    {
        //interactAction.AddOnStateDownListener(OnTriggerDown, handType);
        // listen for events of the Vive controllers
        SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
    }
    

    private void HandleVivePointerEvent(object sender, PointerEventArgs e)
    {
        // if an event from the VR controllers comes in and its target is the current Floor Position where this script is assigned to, call OnMouseDown
        if (e.target == this.transform)
        {
            this.OnMouseDown();
        }
    }


    void OnMouseDown()
    {
        set2022();
        Debug.Log("jetzt ist 2022 :)");
    }

    void set2022()
    {
        thisBackground.GetComponent<Image>().color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
        otherBackground.GetComponent<Image>().color = new Color(0.7887148f, 0.7900782f, 0.7924528f, 1f);

        environmentUpdate.active2100 = false;
        environmentUpdate.set2022();
    }
}
