using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ControllerFlight : MonoBehaviour {

    private GameObject cameraRig;
    protected VRTK_ControllerEvents contrlEvents;


	// Use this for initialization
	void Start () {
        cameraRig = GameObject.FindGameObjectWithTag("CameraRigBase");
        contrlEvents = GetComponent<VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 axisChange = contrlEvents.GetTouchpadAxis();
        if((axisChange.x > 0 || axisChange.y > 0) && 
            (contrlEvents.GetTouchpadAxisAngle() < 60f || contrlEvents.GetTouchpadAxisAngle() > 315f))
        {
            float dirMagnitude = axisChange.SqrMagnitude();

            Debug.Log("This is the mf magnitude: " + dirMagnitude);

            Vector3 dir = transform.forward;

            Debug.Log("This is the mf direction: " + dir);
            if (dir.y >= 0)
            {
                dir.y -= .5f;
            }
            cameraRig.transform.position += (dir * dirMagnitude * .2f);
        }
	}
}
