using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class HideOnClick : MonoBehaviour
{
  public GameObject TabPanel;

  // additional for VR:
  private void Start()
  {
    // listen for events of the Vive controllers
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  void OnMouseDown()
  {
    gameObject.SetActive(false);
    TabPanel.SetActive(true);
  }

  // additional for VR: a public method for calling OnMouseDown() from other scripts
  public void CallOnMouseDown()
  {
    this.OnMouseDown();
  }
  // additional for VR:
  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    // if target of the Vive controller click is the Start screen where this script is assigned to, call OnMouseDown()
    if (e.target == this.transform)
    {
      this.OnMouseDown();
    }
  }
}