using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class SetActiveBrowser : MonoBehaviour
{
    public GameObject browser;


    // VR
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean interactAction;

    public void Start()
    {
        browser.SetActive(true);
        //interactAction.AddOnStateDownListener(OnTriggerDown, handType);
        // listen for events of the Vive controllers
        SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
    }
    

    private void HandleVivePointerEvent(object sender, PointerEventArgs e)
    {
        // if an event from the VR controllers comes in and its target is the current Floor Position where this script is assigned to, call OnMouseDown
            Debug.Log("handlevive");
            this.OnMouseDown();
        
    }

    
    // todo: if stimmt, aber maus wird wohl nach rstem klick nicht mehr erkannt -> vr stattdessen -> baue um zu controller, if maus swieso nicht genutzt wird
    void OnMouseDown()
    {
        //if (Mouse.current.rightButton.wasPressedThisFrame) {
            //Debug.Log("mouse clicked: " + Mouse.current.rightButton.wasPressedThisFrame);

            if (!browser.activeSelf) {
                browser.SetActive(true);
                Debug.Log(browser.activeSelf);
            }
                
            else {
                browser.SetActive(false);
                Debug.Log(browser.activeSelf);
            }
        //}  

    }



}
