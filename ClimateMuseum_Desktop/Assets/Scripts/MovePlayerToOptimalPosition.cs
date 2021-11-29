using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToOptimalPosition : MonoBehaviour
{
    public GameObject player;
    public GameObject panel;

    void OnMouseDown ()
    {
        Vector3  newPosition = player.transform.position;
        newPosition.z = this.transform.position.z;
        newPosition.x = this.transform.position.x;
        player.transform.position = newPosition;

        player.transform.rotation = panel.transform.rotation;

        player.transform.Find("Camera").transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

}
