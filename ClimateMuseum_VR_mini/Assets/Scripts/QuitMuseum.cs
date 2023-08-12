using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMuseum : MonoBehaviour
{
    public void quit()
    {
        Debug.Log("Muesum has quit");
        Application.Quit();
    }
}
