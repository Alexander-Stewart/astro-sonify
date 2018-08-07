using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookAtCamera : MonoBehaviour {

    private MenuInteract menuInteract;
    private GameObject mainCamera;
    private Quaternion restingTransform;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Camera (eye)");
        restingTransform = this.transform.rotation;
        menuInteract = GetComponent<MenuInteract>();
	}
	
	// Update is called once per frame
	void Update () {
        if (menuInteract.isActiveAndEnabled)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
        else
        {
            this.transform.rotation = restingTransform;
        }
	}
}
