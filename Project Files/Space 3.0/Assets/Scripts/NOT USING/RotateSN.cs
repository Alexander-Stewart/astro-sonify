using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RotateSN : MonoBehaviour {

    protected VRTK_ControllerEvents controllerEvents;
    private GameObject superNova;
    public float scaleFactor;

    void Awake()
    {
        superNova = GameObject.FindGameObjectWithTag("SuperNova");
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        float axisAngle = controllerEvents.GetTouchpadAxisAngle();
        Vector2 axis = controllerEvents.GetTouchpadAxis();
        Debug.Log("The axis is: " + axis);
        if (controllerEvents.buttonOnePressed)
        {
            superNova.transform.localEulerAngles += Vector3.up * Time.deltaTime * scaleFactor;
        }
        else if (controllerEvents.buttonTwoPressed)
        {
            superNova.transform.localEulerAngles += Vector3.down * Time.deltaTime * scaleFactor;
        }
    }
}
