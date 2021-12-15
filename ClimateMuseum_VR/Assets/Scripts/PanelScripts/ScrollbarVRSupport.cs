using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class ScrollbarVRSupport : MonoBehaviour
{
  public Scrollbar ControlledScrollbar;
  public UnityEngine.EventSystems.EventSystem ScrollbarEventSystem;
  void Start()
  {
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    if (e.target == this.transform)
    {
      // TODO: 29.11. this is not working at the moment, we do not have the texture coordinates which would be necessary to create a fake event
      //this.ControlledScrollbar.OnPointerDown(new UnityEngine.EventSystems.PointerEventData(this.ScrollbarEventSystem));
      //this.ControlledScrollbar.OnPointerUp(new UnityEngine.EventSystems.PointerEventData(this.ScrollbarEventSystem));
    }
  }
}
