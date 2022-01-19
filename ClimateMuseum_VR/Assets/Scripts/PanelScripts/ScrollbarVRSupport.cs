using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

// This script handles the clicks/pointers that the player performs with the Vive controllers on the scrollbars of the panels.
// It is not assigned in the editor at the beginning, but the helper script ToggleVRSupportHelper assigns it to every toggle.

public class ScrollbarVRSupport : MonoBehaviour
{
  // a scrollbar and event system assigned via ToggleVRSupportHelper
  public Scrollbar ControlledScrollbar;
  public UnityEngine.EventSystems.EventSystem ScrollbarEventSystem;

  void Start()
  {
    // listen for pointer events of the Vive controllers
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    // if target of the Vive controller click is the assigned scrollbar, a pointer event is performed
    if (e.target == this.transform)
    {
      // TODO: 29.11. this is not working at the moment, we do not have the texture coordinates which would be necessary to create a fake event
      //this.ControlledScrollbar.OnPointerDown(new UnityEngine.EventSystems.PointerEventData(this.ScrollbarEventSystem));
      //this.ControlledScrollbar.OnPointerUp(new UnityEngine.EventSystems.PointerEventData(this.ScrollbarEventSystem));
    }
  }
}
