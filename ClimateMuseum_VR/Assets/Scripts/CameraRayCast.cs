using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRayCast : MonoBehaviour
{
  // 29.11.21: this script was used to test a raycast based interaction scheme, for the actual VR input management, we rely on the SteamVR_LaserPointer.PointerClick delegate (see for
  // instance the HideOnClick script)
  public GameObject Indicator;

  void Update()
  {
    if (Mouse.current.rightButton.isPressed)
    {
      Debug.Log("here...");
      Vector3 cameraPosition; // Current camera Position
      Vector3 cameraForwardOrientation; // Current camera Orientation
      Ray rayFromCamera; // Ray originated from Left Camera
      RaycastHit rayHitInfo; // Info of the ray hitting

      // get camera position & orientation
      cameraPosition = this.gameObject.GetComponent<Camera>().ScreenToWorldPoint(Mouse.current.position.ReadValue());
      cameraForwardOrientation = this.gameObject.transform.rotation * Vector3.forward;
      // Ray from Camera to facing direction has to build new every Frame
      rayFromCamera = this.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());//new Ray(cameraPosition, cameraForwardOrientation);
      Debug.DrawLine(cameraPosition, cameraForwardOrientation * 100, Color.red, 100);
      // Check if ray hits something
      if (Physics.Raycast(rayFromCamera, out rayHitInfo))
      {
        Debug.Log("Hit! Collider is " + rayHitInfo.collider);
        if (rayHitInfo.collider != null)
        {
          this.Indicator.transform.position = rayHitInfo.point;

          if (rayHitInfo.collider.transform.gameObject.GetComponent<MovePlayerToOptimalPosition>())
          {
            rayHitInfo.collider.transform.gameObject.GetComponent<MovePlayerToOptimalPosition>().CallOnMouseDown();
          }

          if (rayHitInfo.collider.transform.gameObject.GetComponent<HideOnClick>())
          {
            rayHitInfo.collider.transform.gameObject.GetComponent<HideOnClick>().CallOnMouseDown();
          }
        }
      }
    }
  }
}
