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
    void Update() {
        Vector2 axisChange = contrlEvents.GetTouchpadAxis();
        //Debug.Log("This is angle: " + contrlEvents.GetTouchpadAxisAngle());
        float dirMagnitude = axisChange.SqrMagnitude();
        
        // to go forward
        if ((contrlEvents.GetTouchpadAxisAngle() < 45f || contrlEvents.GetTouchpadAxisAngle() > 315f)
            && contrlEvents.GetTouchpadAxisAngle() != 90f)
        {

            //Debug.Log("This is the mf magnitude: " + dirMagnitude);

            Vector3 dir = transform.forward;

            Debug.Log("This is the mf direction: " + dir);
            if (dir.y >= 0)
            {
                dir.y -= .5f;
            }
            cameraRig.transform.position += (dir * dirMagnitude * .2f);
        }

        // to rotate right
        else if ((contrlEvents.GetTouchpadAxisAngle() < 135f && contrlEvents.GetTouchpadAxisAngle() >= 45f)
          && contrlEvents.GetTouchpadAxisAngle() != 90f)
        {
            //Debug.Log("This is the angle: " + contrlEvents.GetTouchpadAxisAngle());
            cameraRig.transform.Rotate(Vector3.up * dirMagnitude);
        }

        // to rotate left
        else if ((contrlEvents.GetTouchpadAxisAngle() > 225f && contrlEvents.GetTouchpadAxisAngle() <= 315f)
          && contrlEvents.GetTouchpadAxisAngle() != 90f)
        {
            //Debug.Log("This is the angle: " + contrlEvents.GetTouchpadAxisAngle());
            cameraRig.transform.Rotate(Vector3.down * dirMagnitude);
        }

        // to go backward
        else if ((contrlEvents.GetTouchpadAxisAngle() > 135f && contrlEvents.GetTouchpadAxisAngle() <= 225f)
          && contrlEvents.GetTouchpadAxisAngle() != 90f)
        {

            Vector3 dir = transform.forward;

            //Debug.Log("This is the mf direction: " + -dir);
            if (dir.y >= 0)
            {
                dir.y -= .5f;
            }
            cameraRig.transform.position += (-dir * dirMagnitude * .2f);
        }
    }
}