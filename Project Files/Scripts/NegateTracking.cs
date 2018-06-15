using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class NegateTracking : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
	}
}
