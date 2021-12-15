using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class HideOnClick : MonoBehaviour
{
  public GameObject TabPanel;

  private void Start()
  {
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  void OnMouseDown()
  {
    gameObject.SetActive(false);
    TabPanel.SetActive(true);
  }

  public void CallOnMouseDown()
  {
    this.OnMouseDown();
  }
  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    if (e.target == this.transform)
    {
      this.OnMouseDown();
    }
  }
}