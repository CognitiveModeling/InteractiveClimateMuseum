using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

public class ToggleVRSupportHelper : MonoBehaviour
{
  public UnityEngine.EventSystems.EventSystem MainEventSystem;
  public Camera VRCamera;
  // we go through the hierarchy and check for toggles
  void Start()
  {
    Toggle[] toggles = this.GetComponentsInChildren<Toggle>(true);

    foreach (Toggle toggle in toggles)
    {
      if (!toggle.GetComponent<BoxCollider>())
      {
        BoxCollider collider = toggle.gameObject.AddComponent<BoxCollider>();
        RectTransform rect = toggle.gameObject.GetComponent<RectTransform>();
        collider.size = new Vector3(rect.rect.size.x, rect.rect.size.y, .05f);
      }

      if (!toggle.GetComponent<ToggleVRSupport>())
      {
        ToggleVRSupport vrSupport = toggle.gameObject.AddComponent<ToggleVRSupport>();
        vrSupport.ControlledToggle = toggle;
        vrSupport.ToggleEventSystem = this.MainEventSystem;
      }
    }
    // main issue is that the browser is only rendered to one eye... this is probably due to the shader
    // edit: changed it to the default unlit texture and it works
    PointerUIMesh[] browsers = this.GetComponentsInChildren<PointerUIMesh>(true);
    foreach(PointerUIMesh browser in browsers)
    {
      browser.enableVRInput = true;
      browser.viewCamera = this.VRCamera;
      browser.transform.localPosition += new Vector3(0f, 0f, -.1f);
    }

    // remove this script
    GameObject.Destroy(this);
  }

}
