using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASLookAtPoint : MonoBehaviour {

    // tranform to look at 
    public Vector3 posToLookAt;

    private Vector3 curFacingDir;
    private Transform rotator;

    private bool nearPosToLookAt = false;



    // Use this for initialization
    void Start()
    {

        // getting the rotator transform 
        rotator = transform.parent.transform;

        curFacingDir = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
    }

    private void MoveToCenter()
    {
        
    }

    private void MoveOut()
    {
        
    }

    private void UpdateRotation()
    {

            Vector3 dirToLook;
            Quaternion rotation;

                       // getting the direction to look and the needed rotation
                dirToLook = rotator.transform.InverseTransformPoint(posToLookAt);
        //Debug.Log("dirToLook: " + dirToLook);

                rotation = Quaternion.LookRotation(dirToLook);
                //Debug.Log("rotation: " + rotation);

                // rotating, slerp or lerp??
                rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, rotation, Time.deltaTime * .5f);
            
    }

    private Quaternion SMul(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }

}
