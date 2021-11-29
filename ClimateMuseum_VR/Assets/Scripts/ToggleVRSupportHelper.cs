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
        // these two components are shifted in the hierarchy and the colliders need an offset so they do not overlap; would be better to fix this in the
        // editor
        if (toggle.gameObject.name.Equals("Information"))
        {
          collider.center += new Vector3(0f, -17f, 0f);
        }
        else if (toggle.gameObject.name.Equals("Simulator Tutorial"))
        {
          collider.center += new Vector3(0f, -34f, 0f);
        }
      }

      if (!toggle.GetComponent<ToggleVRSupport>())
      {
        ToggleVRSupport vrSupport = toggle.gameObject.AddComponent<ToggleVRSupport>();
        vrSupport.ControlledToggle = toggle;
        vrSupport.ToggleEventSystem = this.MainEventSystem;
      }
    }
    Scrollbar[] scrollbars = this.GetComponentsInChildren<Scrollbar>(true);

    foreach (Scrollbar scrollbar in scrollbars)
    {
      if (!scrollbar.GetComponent<BoxCollider>())
      {
        BoxCollider collider = scrollbar.gameObject.AddComponent<BoxCollider>();
        RectTransform rect = scrollbar.gameObject.GetComponent<RectTransform>();
        collider.size = new Vector3(rect.rect.size.x, rect.rect.size.y, .05f);
      }

      if (!scrollbar.GetComponent<ScrollbarVRSupport>())
      {
        ScrollbarVRSupport vrSupport = scrollbar.gameObject.AddComponent<ScrollbarVRSupport>();
        vrSupport.ControlledScrollbar = scrollbar;
        vrSupport.ScrollbarEventSystem = this.MainEventSystem;
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
