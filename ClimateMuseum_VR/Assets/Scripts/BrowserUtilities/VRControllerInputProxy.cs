using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using Valve.VR;

// This script communicates with the SteamVR and the scripts managing the browser (PonterUIMesh, PointerUIBase, VRBrowserHand).
// In the editor, it is assigned to the object VRBrowser (VRPlayer - SteamVRObjects - RightHand), represented by a cube in the information room.

public class VRControllerInputProxy : MonoBehaviour
{
    // a VRBrowserHand
    public VRBrowserHand VRBrowserHand;
    // I see why they use the action mapping system, but I really dont like it, for the different action types, an overview is provided here:
    // https://valvesoftware.github.io/steamvr_unity_plugin/articles/SteamVR-Input.html
    // we stick with a simple binary action, if you are interested in the trigger strength, then SteamVR_Action_Single would be the way to go

    // a specific boolean from SteamVR, indicating the trigger click
    public SteamVR_Action_Boolean TriggerClick;

    // a specific input source from SteamVR
    private SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    // alternatively, we could have used SteamVR_Input_Sources.RightHand as input source, in this case, this:
    // SteamVR_Actions._default.Squeeze.GetAxis(RightInputSource)
    // would have given us the trigger strength

    // adds listeners for pressing (s. Press() below) and for releasing (s. Release() below) to the input source
    private void OnEnable()
    {
        TriggerClick.AddOnStateDownListener(this.Press, this.inputSource);
        TriggerClick.AddOnStateUpListener(this.Release, this.inputSource);
    }

    // removes listeners for pressing (s. Press() below) and for releasing (s. Release() below) to the input source
    private void OnDisable()
    {
        TriggerClick.RemoveOnStateDownListener(this.Press, this.inputSource);
        TriggerClick.RemoveOnStateUpListener(this.Release, this.inputSource);
    }

    // sets the amplitude of the VR browser hand to 1 if the trigger is pressed
    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        this.VRBrowserHand.LeftClickAmplitude = 1.0f;
        print("press...");
    }

    // sets the amplitude of the VR browser hand to 0 if the trigger is released
    private void Release(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        this.VRBrowserHand.LeftClickAmplitude = 0.0f;
        print("release...");
    }
}
