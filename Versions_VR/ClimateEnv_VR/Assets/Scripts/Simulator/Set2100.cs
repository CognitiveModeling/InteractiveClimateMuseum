using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class Set2100 : MonoBehaviour
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
    

    void OnMouseDown()
    {
        set2100();
        Debug.Log("jetzt ist 2100 :)");
    }

    public void set2100()
    {
        thisBackground.GetComponent<Image>().color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
        otherBackground.GetComponent<Image>().color = new Color(0.7887148f, 0.7900782f, 0.7924528f, 1f);
        environmentUpdate.active2100 = true;
        environmentUpdate.startReadOutAndApplyValues();
    }

    // additional for VR:
    private void HandleVivePointerEvent(object sender, PointerEventArgs e)
    {
        // if an event from the VR controllers comes in and its target is the current Floor Position where this script is assigned to, call OnMouseDown
        if (e.target == this.transform)
        {
            this.OnMouseDown();
        }
    }
    
    /*
    private void Update()
    {
        Debug.Log("interactAction.GetStateDown(handType)2100: " + interactAction.GetStateDown(handType));
        
        if (interactAction.GetStateDown(handType))
        {
            // Check if the controller is colliding with the button.
            //Collider[] colliders = Physics.OverlapSphere(controller.transform.position, 0.05f); // Adjust the radius as needed.

             RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            
            foreach (Collider collider in colliders)
            {
                Debug.Log("collider21: " + collider);
                if (collider.gameObject == this.gameObject)
                {
                    Debug.Log("collider21: " + collider + " " + this.gameObject);
                    Debug.Log("inside collider = button2100");
                    // Change the button color when clicked.
                    set2100();
                    
                }
            }
        }
    }
    

    /*public void OnTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("trigger is down 2100");

        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.05f); // Adjust the radius as needed.
            
            foreach (Collider collider in colliders)
            {
                Debug.Log("collider21: " + collider);
                if (collider.gameObject == this.gameObject)
                {
                    Debug.Log("collider21: " + collider + " " + this.gameObject);
                    Debug.Log("inside collider = button2100");
                    // Change the button color when clicked.
                    set2100();
                    
                }
            }

        //set2100();
    }
    */

    

}
