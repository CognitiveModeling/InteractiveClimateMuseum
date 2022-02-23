using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

// This script prepares toggles, scrollbars and browsers in all panels
// so they can be used by the event listener scripts ToggleVRSupport, ScrollbarVrSupport and the scripts important for the browser interaction.
// It is assigned to the object Panels (Museum_VR - Panels) in the editor.

public class ToggleVRSupportHelper : MonoBehaviour
{
  // an event system, the object Panels is assigned in the editor
  public UnityEngine.EventSystems.EventSystem MainEventSystem;

  // a VR camera, the object VRCamera (Museum_VR - VR_Player - SteamVRObjects - VRCamera) is assigned in the editor
  public Camera VRCamera;
  
  // goes through the hierarchy, checks for toggles, scrollbars and browsers and prepares them for future use / event listeners
  void Start()
  {
    // collect all toggles in all the panels, including inactive toggles
    Toggle[] toggles = this.GetComponentsInChildren<Toggle>(true);

    // for each toggle in this list:
    foreach (Toggle toggle in toggles)
    {
      // if the toggle has no box collider
      if (!toggle.GetComponent<BoxCollider>())
      {
        // add a box collider of the size of the toggle (using its rect transform)
        BoxCollider collider = toggle.gameObject.AddComponent<BoxCollider>();
        // get toggle's rect transform (infos for a rectangle)
        RectTransform rect = toggle.gameObject.GetComponent<RectTransform>();
        // set the box collider's size to rectangle
        collider.size = new Vector3(rect.rect.size.x, rect.rect.size.y, .05f);
      }

      // if the toggle does not have the event system script ToggleVRSupport (applies to every toggle)
      if (!toggle.GetComponent<ToggleVRSupport>())
      {
        // add the script, set its controlled toggle and event system to the current ones
        ToggleVRSupport vrSupport = toggle.gameObject.AddComponent<ToggleVRSupport>();
        vrSupport.ControlledToggle = toggle;
        vrSupport.ToggleEventSystem = this.MainEventSystem;
      }
    }

    // repeat the same procedure for buttons

    // collect all buttons in all the panels, including inactive buttons
    Button[] buttons = this.GetComponentsInChildren<Button>(true);

    // for each button in this list:
    foreach (Button button in buttons)
    {
      // if the button has no box collider
      if (!button.GetComponent<BoxCollider>())
      {
        // add a box collider of the size of the button (using its rect transform)
        BoxCollider collider = button.gameObject.AddComponent<BoxCollider>();
        // get button's rect transform (infos for a rectangle)
        RectTransform rect = button.gameObject.GetComponent<RectTransform>();
        // set the box collider's size to rectangle
        collider.size = new Vector3(rect.rect.size.x, rect.rect.size.y, .1f);
      }

      // if the button does not have the event system script ButtonVRSupport (applies to every button)
      if (!button.GetComponent<ButtonVRSupport>())
      {
        // add the script, set its controlled button and event system to the current ones
        ButtonVRSupport vrSupport = button.gameObject.AddComponent<ButtonVRSupport>();
        vrSupport.ControlledButton = button;
        vrSupport.ButtonEventSystem = this.MainEventSystem;
      }
    }

    // repeat the same procedure for scrollbars

    // collect all scrollbars in all the panels, including inactive scrollbars
    Scrollbar[] scrollbars = this.GetComponentsInChildren<Scrollbar>(true);

    // for each scrollbar in this list:
    foreach (Scrollbar scrollbar in scrollbars)
    {
      if (!scrollbar.GetComponent<BoxCollider>())
      {
        // add a box collider of the size of the scrollbar (using its rect transform)
        BoxCollider collider = scrollbar.gameObject.AddComponent<BoxCollider>();
        RectTransform rect = scrollbar.gameObject.GetComponent<RectTransform>();
        collider.size = new Vector3(rect.rect.size.x, rect.rect.size.y, .05f);
      }

      // if the scrollbar does not have the script ToggleVRSupport
      if (!scrollbar.GetComponent<ScrollbarVRSupport>())
      {
        // add the script, set its controlled scrollbar and event system to the current ones
        ScrollbarVRSupport vrSupport = scrollbar.gameObject.AddComponent<ScrollbarVRSupport>();
        vrSupport.ControlledScrollbar = scrollbar;
        vrSupport.ScrollbarEventSystem = this.MainEventSystem;
      }
    }

    // repeat a similar procedure for browsers

    // main issue is that the browser is only rendered to one eye... this is probably due to the shader
    // edit: changed it to the default unlit texture and it works

    // collect all browsers in all the panels, including inactive ones
    PointerUIMesh[] browsers = this.GetComponentsInChildren<PointerUIMesh>(true);
    
    // for each browser in this list:
    foreach (PointerUIMesh browser in browsers)
    {
      // enable VR input, set browser's camera to the VRCamera and change browser's position
      browser.enableVRInput = true;
      browser.viewCamera = this.VRCamera;
      browser.transform.localPosition += new Vector3(0f, 0f, -.1f);
    }

    // remove this script
    GameObject.Destroy(this);
  }

}
