using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRayCast : MonoBehaviour
{
  // 29.11.21: this script was used to test a raycast based interaction scheme, for the actual VR input management, we rely on the SteamVR_LaserPointer.PointerClick delegate (see for
  // instance the HideOnClick script)

  // indicator for the point in world space where a ray hit a collider
  public GameObject Indicator;

  void Update()
  {
    // if right mouse button is pressed
    if (Mouse.current.rightButton.isPressed)
    {
      Debug.Log("here...");
      // Current camera Position
      Vector3 cameraPosition;
      // Current camera Orientation
      Vector3 cameraForwardOrientation;
      // Ray originated from Left Camera
      Ray rayFromCamera;
      // Info of the ray hitting
      RaycastHit rayHitInfo;

      // get camera position & orientation
      cameraPosition = this.gameObject.GetComponent<Camera>().ScreenToWorldPoint(Mouse.current.position.ReadValue());
      cameraForwardOrientation = this.gameObject.transform.rotation * Vector3.forward;
      // Ray from Camera to facing direction has to build new every Frame
      rayFromCamera = this.gameObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue()); //new Ray(cameraPosition, cameraForwardOrientation);
      Debug.DrawLine(cameraPosition, cameraForwardOrientation * 100, Color.red, 100);
      
      // Check if ray hits something
      if (Physics.Raycast(rayFromCamera, out rayHitInfo))
      {
        Debug.Log("Hit! Collider is " + rayHitInfo.collider);
        if (rayHitInfo.collider != null)
        {
          this.Indicator.transform.position = rayHitInfo.point;

          // depending on the hit object (collider) and its scripts, call the methods inside (here exemplary for scripts MovePlayerToOptimalPosition and HideOnClick)
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
