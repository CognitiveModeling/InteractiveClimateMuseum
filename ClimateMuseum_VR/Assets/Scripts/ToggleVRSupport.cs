using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class ToggleVRSupport : MonoBehaviour
{
  public Toggle ControlledToggle;
  public UnityEngine.EventSystems.EventSystem ToggleEventSystem;
  void Start()
  {
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    if (e.target == this.transform)
    {
      this.ControlledToggle.OnPointerClick(new UnityEngine.EventSystems.PointerEventData(this.ToggleEventSystem));
    }
  }
}
