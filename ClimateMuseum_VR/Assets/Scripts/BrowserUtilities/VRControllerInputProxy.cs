using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using Valve.VR;

public class VRControllerInputProxy : MonoBehaviour
{
    public VRBrowserHand VRBrowserHand;
    // I see why they use the action mapping system, but I really dont like it, for the different action types, an overview is provided here:
    // https://valvesoftware.github.io/steamvr_unity_plugin/articles/SteamVR-Input.html
    // we stick with a simple binary action, if you are interested in the trigger strength, then SteamVR_Action_Single would be the way to go
    public SteamVR_Action_Boolean TriggerClick;
    private SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    // alternatively, we could have used SteamVR_Input_Sources.RightHand as input source, in this case, this:
    // SteamVR_Actions._default.Squeeze.GetAxis(RightInputSource)
    // would have given us the trigger strength

    private void OnEnable()
    {
        TriggerClick.AddOnStateDownListener(this.Press, this.inputSource);
        TriggerClick.AddOnStateUpListener(this.Release, this.inputSource);
    }

    private void OnDisable()
    {
        TriggerClick.RemoveOnStateDownListener(this.Press, this.inputSource);
        TriggerClick.RemoveOnStateUpListener(this.Release, this.inputSource);
    }

    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        this.VRBrowserHand.LeftClickAmplitude = 1.0f;
        print("press...");
    }

    private void Release(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        this.VRBrowserHand.LeftClickAmplitude = 0.0f;
        print("release...");
    }
}
