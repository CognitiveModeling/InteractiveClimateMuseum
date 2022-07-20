using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnClick : MonoBehaviour
{

    public Set2100 set2100;

    public GameObject[] Content;
    void OnMouseDown()
    {
        gameObject.SetActive(false);
        foreach (GameObject obj in this.Content)
        {
            Vector3 pos = obj.transform.position;

            float y = 1.15f;
            pos.y = y;
            obj.transform.position = pos;
        }
        set2100.set2100();
    }
}