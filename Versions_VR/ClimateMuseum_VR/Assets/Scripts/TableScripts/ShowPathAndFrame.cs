using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class ShowPathAndFrame : MonoBehaviour
{
  public GameObject Path;
  public GameObject Frame;

  private void Start()
  {
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  void OnMouseDown()
  {
    if (!(Frame.activeSelf))
    {
      GameObject[] tableCardFrames;
      tableCardFrames = GameObject.FindGameObjectsWithTag("Table Card Frame");

      GameObject[] paths;
      paths = GameObject.FindGameObjectsWithTag("Path");

      foreach (GameObject frame in tableCardFrames)
      {
        frame.SetActive(false); //deactivate all frames, also correlation frames
      }

      foreach (GameObject path in paths)
      {
        path.SetActive(false);
      }

      Path.SetActive(true);
      Frame.SetActive(true);
    }

    else
    {
      Path.SetActive(false);
      Frame.SetActive(false);

    }

  }

  private void HandleVivePointerEvent(object sender, PointerEventArgs e)
  {
    if (e.target == this.transform)
    {
      this.OnMouseDown();
    }
  }
}
