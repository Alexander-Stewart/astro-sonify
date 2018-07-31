using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * a script that will go on the left controller game object, 
 * and the main purpose is to move the camera rig around relative
 * to where the left hand is inside the play area. 
 **/
public class MS2Move : MonoBehaviour {

    private Transform cameraRig;
    private Transform CasA;

	// Use this for initialization
	void Start () {

        // getting the super nova transform
        CasA = GameObject.FindGameObjectWithTag("SuperNova").transform;


        // getting the camera rig transform
        cameraRig = GameObject.FindGameObjectWithTag("CameraRigBase").transform;
    
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
	}

    private void UpdatePosition()
    {
        // getting distance from hand to center
        Vector3 dis = transform.position - cameraRig.transform.position;  

        // getting the where the hand is inside of the play area.
        Vector3 relPos = cameraRig.transform.InverseTransformPoint(transform.position);

        // getting the position of where the camera rig should be inside the supernova;
        Vector3 updateRelPos = CasA.InverseTransformPoint(relPos);

        updateRelPos.y -= 1.8f;
        updateRelPos.z += .6f;

        cameraRig.localPosition = updateRelPos * dis.magnitude * 45f;
    }
}
