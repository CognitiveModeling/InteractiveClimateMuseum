using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // variables needed for teleportation
    public Transform target;
    public Vector3 oldPos;
    public bool teleported = false;

    // variables needed for fade in and out 
    public GameObject canvas;
    public GameObject Fader;
    private Animator anim;

    private void Awake()
    {
        anim = Fader.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!teleported)
            {
                canvas.SetActive(true);
                oldPos = this.gameObject.transform.position;

                FadeOut();
                // perform with small temporal delay 
                Invoke("GoToDestination", 0.9f);
                Invoke("FadeIn", 0.7f);

            } else
            {
                canvas.SetActive(true);
                FadeOut();
                // perform with small temporal delay 
                Invoke("GoToOldPos", 0.9f);
                Invoke("FadeIn", 0.7f);
            }
            Invoke("deactivateCanvas", 1.0f);
        }
    }

    void deactivateCanvas()
    {
        canvas.SetActive(false);
    }
    void FadeOut()
    {
        anim.SetBool("FadeIn", false);
        anim.SetBool("FadeOut", true);
    }
    void FadeIn()
    {
        anim.SetBool("FadeOut", false);
        anim.SetBool("FadeIn", true);
    }

    void GoToDestination()
    { 
        this.gameObject.transform.position = new Vector3(target.transform.position.x, 0.6f, target.transform.position.z);
        teleported = true;
    }

    void GoToOldPos()
    {
        this.gameObject.transform.position = oldPos;
        teleported = false;
    }
}
