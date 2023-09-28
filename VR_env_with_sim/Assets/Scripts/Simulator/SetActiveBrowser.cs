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

    // todo: noch chaos hier mit if-schachtelung
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            Debug.Log("mouse clicked" + browser.activeSelf);

            if (browser.activeSelf)
            browser.SetActive(false);
        }

        else {
            browser.SetActive(true);
        }
        
    }
}
