using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class MenuAndInstructions : MonoBehaviour {
    private bool onOffSwitch = false;
    private Text screenText;
    private Text title;
    private GameObject canvas;

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

        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += MenuAndInstructions_ButtonTwoReleased;

    }

    private void MenuAndInstructions_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        onOffSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (contrlEvents.buttonTwoPressed)
        {
            title.text = "Controls:";
            screenText.text = "Pointers: A button/X button" +
                "\n" +
                "Movement: point either controller in wanted direction, tilt forward analog stick/touchpad";
            canvas.SetActive(true);
        } else if (onOffSwitch)
        {
            canvas.SetActive(false);
            onOffSwitch = false;
        }
        
    }
}
