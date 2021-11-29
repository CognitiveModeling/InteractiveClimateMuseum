using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEnableScript : MonoBehaviour
{
	public GameObject image;
    public GameObject header;


    void OnEnable()
    {
        image.SetActive(true);
        header.SetActive(true);
    }

    void OnDisable()
    {
        image.SetActive(false);
        header.SetActive(false);
    }
}
