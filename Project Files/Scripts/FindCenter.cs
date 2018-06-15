using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;

public class FindCenter : MonoBehaviour {

    // in FindCenter, have onTriggerStay active, and then allow for 
    // teleport that moves player to center(re-center)
    // give haptic feedback to controllers to let them know they are inside the center.

    public GameObject leftHand;
    public GameObject rightHand;

    private GameObject leftHandModel;

    private SteamVR_TrackedObject trackedRightHand;
    private SteamVR_TrackedObject trackedLeftHand;

    private SteamVR_Controller.Device device1;
    private SteamVR_Controller.Device device2;

    // Use this for initialization
    void Start () {
        // getting tracked object components
        trackedRightHand = rightHand.GetComponent<SteamVR_TrackedObject>();
        trackedLeftHand = leftHand.GetComponent<SteamVR_TrackedObject>();

        //getting left hand model
        leftHandModel = leftHand.transform.GetChild(0).gameObject;

        device1 = SteamVR_Controller.Input((int)trackedRightHand.index);
        device2 = SteamVR_Controller.Input((int)trackedLeftHand.index);

    }
	

    void OnTriggerStay(Collider Other)
    {

        if(!leftHandModel.activeSelf && Other.tag == "LeftController")
        {
            Debug.Log("Left hand found center of Super Nova");
            // left hand is device2
            device2 = SteamVR_Controller.Input((int)trackedLeftHand.index);
            StartCoroutine("PulseVibration");
        } else if (leftHandModel.activeSelf && Other.tag == "RightController")
        {
            Debug.Log("Left hand found center of Super Nova");
            // right hand is device1
            device1 = SteamVR_Controller.Input((int)trackedRightHand.index);
            StartCoroutine("PulseVibration");
        }
    }
   
    void OnTriggerExit(Collider Other)
    {
        if(!leftHandModel.activeSelf && Other.tag == "LeftController")
        {
            StopCoroutine("PulseVibration");
        } else if (leftHandModel.activeSelf && Other.tag == "RightController")
        {
            StopCoroutine("PulseVibration");
        }
    }

    IEnumerator PulseVibration()
    {
        if (!leftHandModel.activeSelf)
        {
            device2 = SteamVR_Controller.Input((int)trackedLeftHand.index);
            device2.TriggerHapticPulse(800);
        }
        else
        {
            device1 = SteamVR_Controller.Input((int)trackedRightHand.index);
            device1.TriggerHapticPulse(800);
        }
        yield return new WaitForSeconds(8f);
    }

}
