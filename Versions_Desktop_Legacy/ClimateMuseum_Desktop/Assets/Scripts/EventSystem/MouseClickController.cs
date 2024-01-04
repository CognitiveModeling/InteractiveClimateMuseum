using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickController : MonoBehaviour
{
    public GameObject mouseProxy;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                this.mouseProxy.transform.position = hit.transform.position;
            }
            else
            {
                this.mouseProxy.transform.position = new Vector3(0, 2, 0);
            }
        }
    }
}
