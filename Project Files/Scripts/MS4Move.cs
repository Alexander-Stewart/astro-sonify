using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MS4Move : MonoBehaviour {
    private GameObject cameraRig;
    protected VRTK_ControllerEvents contrlEvents;
    private Vector3 startingPos;


    // Use this for initialization
    void Start()
    {
        cameraRig = GameObject.FindGameObjectWithTag("CameraRigBase");
        contrlEvents = GetComponent<VRTK_ControllerEvents>();
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += DragAndMove_TriggerPressed;
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += DragAndMove_TriggerReleased;
    }

    private void DragAndMove_TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Released");
    }

    private void DragAndMove_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        Vector3 startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (contrlEvents.triggerPressed)
        {
            Vector3 curPos = transform.position;
            Vector3 distanceMoved = curPos - startingPos;
            if (distanceMoved.magnitude > .55)
            {
                distanceMoved = .55f * distanceMoved;
            }
            Debug.Log("This is the magnitude: " + distanceMoved.magnitude);
            if (distanceMoved.magnitude > .4f)
            {
                cameraRig.transform.position += distanceMoved * .1f;
            }
        }
    }
}
