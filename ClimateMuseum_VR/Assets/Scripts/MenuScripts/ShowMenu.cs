using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;

// This script displays the menu canvas if the user presses the "M" key (Desktop Version) or the menu button at the VR controllers (VR Version).
// It is assigned to the player (Desktop Version) and the VR player's hands (VR Version) in the editor.

public class ShowMenu : MonoBehaviour
{
  // the menu canvas
  public GameObject menu;

  // a specific boolean from SteamVR, indicating the click of the menu button at the VR controllers
  public SteamVR_Action_Boolean MenuClick;

  // a specific input source from SteamVR
  private SteamVR_Input_Sources inputSource;

  void Update()
  {
    // if "M" is pressed (Desktop Version), menu canvas is activated
    if (Keyboard.current.mKey.wasPressedThisFrame)
    {
      menu.SetActive(true);
    }
  }

  // adds listeners for pressing (s. Press() below) and for releasing (s. Release() below) to the input source
  private void OnEnable()
  {
    MenuClick.AddOnStateDownListener(this.Press, this.inputSource);
    MenuClick.AddOnStateUpListener(this.Release, this.inputSource);
  }

  // removes listeners for pressing (s. Press() below) and for releasing (s. Release() below) to the input source
  private void OnDisable()
  {
    MenuClick.RemoveOnStateDownListener(this.Press, this.inputSource);
    MenuClick.RemoveOnStateUpListener(this.Release, this.inputSource);
  }

  // if the button is pressed, the menu canvas is activated and a log message is printed
  private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  {
    menu.SetActive(true);
    print("press menu button...");
  }

  // if the button is released, a log message is printed
  private void Release(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
  {
    print("release menu button...");
  }
}
