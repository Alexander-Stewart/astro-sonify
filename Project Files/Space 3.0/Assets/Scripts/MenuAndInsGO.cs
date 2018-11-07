using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class MenuAndInsGO : MonoBehaviour {
    private bool onOffSwitch = true;
    private Text screenText;
    private Text title;
    private GameObject canvas;
    private bool startUp = true;

    protected VRTK_ControllerEvents contrlEvents;

    // Use this for initialization
    void Start()
    {
        contrlEvents = GetComponent<VRTK_ControllerEvents>();

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        GameObject textObj1 = canvas.transform.GetChild(0).gameObject;
        title = textObj1.GetComponent<Text>();

        GameObject textObj2 = canvas.transform.GetChild(1).gameObject;
        screenText = textObj2.GetComponent<Text>();

       

    }

    private void MenuAndInstructions_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Emitted");
        onOffSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");

            GameObject textObj1 = canvas.transform.GetChild(0).gameObject;
            title = textObj1.GetComponent<Text>();

            GameObject textObj2 = canvas.transform.GetChild(1).gameObject;
            screenText = textObj2.GetComponent<Text>();
        }
        else if (contrlEvents.buttonTwoPressed)
        {
            // changing the onOffSwitch.
            if (onOffSwitch)
            {
                onOffSwitch = false;
            } else
            {
                onOffSwitch = true;
            }

            // changing the canvas.
            if (onOffSwitch)
            {
                title.text = "Controls:";
                screenText.text = "Pointer: Trigger Button" +
                    "\n" +
                    "Movement: point controller in wanted direction, and slide thumb forward on touchpad." +
                    "\n" +
                    "To close this window, press the back button.";
                canvas.SetActive(true);
            } else
            {
                canvas.SetActive(false);
                onOffSwitch = false;
            }
            
        }

    }

}
