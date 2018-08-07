using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;

public class FindCenter : MonoBehaviour {

    /**
     * The FindCenter class is for finding the center of the 
     * super nova in the MS2 system. 
     **/

   
    private VRTK_InteractableObject io;
    private VRTK_ControllerReference controllerReference;

    // Use this for initialization
    void Update () {
        transform.localPosition = Vector3.zero;
    }

    void OnEnable()
    {
        io = GetComponent<VRTK_InteractableObject>();
        io.InteractableObjectTouched += Io_InteractableObjectTouched;
        io.InteractableObjectUntouched += Io_InteractableObjectUntouched;
    }

    /**
     * InteractableObjectUntouched is a method that is activated when one of the controllers
     * leaves the interactable objects collider.
     * @params: sender- the sender is the game object that is getting interacted with(io)
     * @params: e- e is the object that is interacting with the io.
     **/
    private void Io_InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("Untouched");
        VRTK_ControllerHaptics.CancelHapticPulse(controllerReference);
        controllerReference = null;
    }

    /**
     * InteractableObjectUntouched is a method that is activated when one of the controllers
     * leaves the interactable objects collider.
     * @params: sender- the sender is the game object that is getting interacted with(io)
     * @params: e- e is the object that is interacting with the io.
     **/
    private void Io_InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("Touched");
        controllerReference = VRTK_ControllerReference.GetControllerReference(e.interactingObject);
        VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, 1f, 10f, 1f);
    }

 //   IEnumerator PulseVibration()
 //   {
 //       if (!leftHandModel.activeSelf)
 //       {
 //           device2 = SteamVR_Controller.Input((int)trackedLeftHand.index);
 //           device2.TriggerHapticPulse(800);
 //      }
 //       else
 //       {
 //           device1 = SteamVR_Controller.Input((int)trackedRightHand.index);
 //           device1.TriggerHapticPulse(800);
 //       }
 //       yield return new WaitForSeconds(8f);
 //   }

}
