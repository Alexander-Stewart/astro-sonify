using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CameraToHand : MonoBehaviour {
#if false
    public GameObject MainCamera;
    //public Transform cameraTransform;
	// Use this for initialization
	void Start () {
        //cameraTransform.parent = transform;

    }
	
	// Update is called once per frame
	void LateUpdate () {
        MainCamera.transform.localPosition = new Vector3(0f,0f,0f);
        transform.localPosition = new Vector3(0f, 0f, 0f);
       // transform.position = parent.transform.position;
        //cameraTransform.parent = transform;
    }
#endif
}
