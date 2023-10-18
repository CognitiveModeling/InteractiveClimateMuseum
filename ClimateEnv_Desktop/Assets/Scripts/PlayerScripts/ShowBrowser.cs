using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowBrowser : MonoBehaviour
{

    public GameObject browser;

    // Start is called before the first frame update
    void Start()
    {
        browser.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame) {

            if (!browser.activeSelf)
            {
                browser.SetActive(true);
                //Debug.Log(browser.activeSelf);
            }
                
            else
            {
                browser.SetActive(false);
                //Debug.Log(browser.activeSelf);
            }
        }  
        
    }
}
