using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetActiveBrowser : MonoBehaviour
{
    public GameObject browser;

    // Start is called before the first frame update
    void Start()
    {
        browser.SetActive(true);
    }

    // Update is called once per frame

    // todo: if stimmt, aber maus wird wohl nach rstem klick nicht mehr erkannt -> vr stattdessen -> baue um zu controller, if maus swieso nicht genutzt wird
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            Debug.Log("mouse clicked: " + Mouse.current.rightButton.wasPressedThisFrame);

            if (!browser.activeSelf) {
                browser.SetActive(true);
            }
                
            else {
                browser.SetActive(false);
                Debug.Log(browser.activeSelf);
            }
        }  

    }



}
