using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class MovePlayerToOptimalPosition : MonoBehaviour
{
  public GameObject player;
  public GameObject panel;

  void Start()
  {
    if (!this.gameObject.GetComponent<Collider>())
    {
      this.gameObject.AddComponent<SphereCollider>();
      this.gameObject.GetComponent<SphereCollider>().isTrigger = true;
    }
    SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
  }

  void OnMouseDown()
  {
    if (GameObject.FindGameObjectWithTag("VRPlayerRoot"))
    {
      Player player = Valve.VR.InteractionSystem.Player.instance;

      Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
      player.trackingOriginTransform.position = this.transform.position + playerFeetOffset;

      if (player.leftHand.currentAttachedObjectInfo.HasValue)
        player.leftHand.ResetAttachedTransform(player.leftHand.currentAttachedObjectInfo.Value);
      if (player.rightHand.currentAttachedObjectInfo.HasValue)
        player.rightHand.ResetAttachedTransform(player.rightHand.currentAttachedObjectInfo.Value);
    }
    else
    {
      Vector3 newPosition = player.transform.position;
      newPosition.z = this.transform.position.z;
      newPosition.x = this.transform.position.x;
      player.transform.position = newPosition;

      player.transform.rotation = panel.transform.rotation;

      player.transform.Find("Camera").transform.localRotation = Quaternion.Euler(0, 0, 0);
    }


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
