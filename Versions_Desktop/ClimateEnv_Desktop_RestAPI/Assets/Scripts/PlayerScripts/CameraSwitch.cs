using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject topDownCamera;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cam1"))
        {
            mainCamera.SetActive(true);
            player.SetActive(true);
            topDownCamera.SetActive(false);
        }
        else if (Input.GetButtonDown("Cam2"))
        {
            player.SetActive(false);
            //mainCamera.SetActive(false);
            topDownCamera.SetActive(true);
        }
    }
}
