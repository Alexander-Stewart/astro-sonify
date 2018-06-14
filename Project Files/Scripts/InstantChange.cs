using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InstantChange : MonoBehaviour {
//#if MS1
    // USES VRTK

    protected VRTK_ControllerEvents controllerEvents;
    public GameObject superNova;
    public Vector3 wantedSuperNovaPos;
    public int scaleFactor;

    void Awake()
    {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        controllerEvents.ButtonOnePressed += ControllerEvents_ButtonOnePressed;
        controllerEvents.ButtonTwoPressed += ControllerEvents_ButtonTwoPressed;
    }

    private void ControllerEvents_ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        Vector3 size = superNova.transform.localScale;

        if (!(size.x <= 1))
        {
            size = size / scaleFactor;
            while (superNova.transform.localScale.x > size.x)
            {
                superNova.transform.localScale -= new Vector3(1.0f, 1.0f, 1.0f);
                superNova.transform.localPosition = wantedSuperNovaPos;
            }
            //superNova.transform.localScale = size;
            //superNova.transform.localPosition = wantedSuperNovaPos;
        }
    }

    private void ControllerEvents_ButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        //increases size of superNova.
        Vector3 size = superNova.transform.localScale;

        if (!(size.x >= 100))
        {
            size = size * scaleFactor;
            while (superNova.transform.localScale.x < size.x)
            {
                superNova.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
                superNova.transform.localPosition = wantedSuperNovaPos;
            }
           //superNova.transform.localScale = size;
           //superNova.transform.localPosition = wantedSuperNovaPos;
        }
    }
//#endif
}
