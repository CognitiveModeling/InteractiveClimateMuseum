using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemFinder : MonoBehaviour
{
    void Awake()
    {
        UnityEngine.EventSystems.EventSystem sceneEventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
        Debug.Log("wer bin ich: " + sceneEventSystem.name);
    }
    
}
