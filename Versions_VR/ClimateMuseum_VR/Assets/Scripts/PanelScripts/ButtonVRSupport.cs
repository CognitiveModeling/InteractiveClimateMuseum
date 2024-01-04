using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

// This script handles the clicks/pointers that the player performs with the Vive controllers on the panels.
// It is not assigned in the editor at the beginning, but the helper script ToggleVRSupportHelper assigns it to every toggle.

public class ButtonVRSupport : MonoBehaviour
{
  // a button and event system assigned via ToggleVRSupportHelper
  public Button ControlledButton;
  public UnityEngine.EventSystems.EventSystem ButtonEventSystem;

  void Start()
  {
    // listen for events of the Vive controllers
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    // if target of the Vive controller click is the assigned button, a pointer event is performed
    if (e.target == this.transform)
    {
      this.ControlledButton.OnPointerClick(new UnityEngine.EventSystems.PointerEventData(this.ButtonEventSystem));
    }
  }
}
