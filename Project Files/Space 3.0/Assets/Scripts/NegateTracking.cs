using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class NegateTracking : MonoBehaviour {

	/**
     * this class is for negating the tracking on the headset
     * in order to be able to have it positioned and following the 
     * motion controllers.
     **/
	// Update is called once per frame
	void Update () {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }
}
