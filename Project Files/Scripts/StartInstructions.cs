using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class StartInstructions : MonoBehaviour {


    private bool start = true;

    private GameObject canvas;

    private VRTK_Pointer PointerLeft;
    private ControllerFlight FlightLeft;
    private MenuAndInstructions MAILeft;

    private VRTK_Pointer PointerRight;
    private ControllerFlight FlightRight;
    private MenuAndInstructions MAIRight;

    protected VRTK_ControllerEvents contrlEventsLeft;
    protected VRTK_ControllerEvents contrlEventsRight;

    private GameObject LeftController;
    private GameObject RightController;

    // Use this for initialization
    void Start () {

        LeftController = GameObject.FindGameObjectWithTag("LeftController");
        RightController = GameObject.FindGameObjectWithTag("RightController");

        contrlEventsLeft = LeftController.GetComponent<VRTK_ControllerEvents>();
        contrlEventsRight = RightController.GetComponent<VRTK_ControllerEvents>();

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        PointerLeft = LeftController.GetComponent<VRTK_Pointer>();
        FlightLeft = LeftController.GetComponent<ControllerFlight>();
        MAILeft = LeftController.GetComponent<MenuAndInstructions>();

        PointerRight = RightController.GetComponent<VRTK_Pointer>();
        FlightRight = RightController.GetComponent<ControllerFlight>();
        MAIRight = RightController.GetComponent<MenuAndInstructions>();




    }
	
	// Update is called once per frame
	void Update () {
        if(canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }
        if (start)
        {
            PointerLeft.enabled = false;
            FlightLeft.enabled = false;
            MAILeft.enabled = false;

            PointerRight.enabled = false;
            FlightRight.enabled = false;
            MAIRight.enabled = false;

            if (contrlEventsLeft.AnyButtonPressed() || contrlEventsRight.AnyButtonPressed())
            {


                PointerLeft.enabled = true;
                FlightLeft.enabled = true;
                MAILeft.enabled = true;

                PointerRight.enabled = true;
                FlightRight.enabled = true;
                MAIRight.enabled = true;

                canvas.SetActive(false);

                start = false;

                this.enabled = false;

            }
        }
	}
}
