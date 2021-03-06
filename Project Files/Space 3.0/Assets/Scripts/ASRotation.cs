﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASRotation : MonoBehaviour
{

    private Dictionary<Vector3, Vector3> gradientData;
    private GameObject rightHand;
    private GameObject audioListener;
    private GameObject CasA;
    private Vector3 curFacingDir;
    private Transform rotator;
    private GameObject headFollow;

    // for checking MS version
    private MovementSystem MSL;

    // for MS1
    private GameObject CasALeft;
    private GameObject CasARight;
    // for MS1

    //for telling if they want max or min dir of change.
    public bool WantMinimum = false;

    // Use this for initialization
    void Start()
    {
        // getting the MS logger to see which version the MS is
        MSL = GameObject.Find("MSLogger").GetComponent<MSLogger>().MS;

        // getting the gradient data
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        gradientData = playArea.GetComponent<DataReader>().gradientData;

        // getting the right hand
        rightHand = GameObject.FindGameObjectWithTag("SensorRight");

        // getting the GameObject the Audio Listener is on
        audioListener = GameObject.FindGameObjectWithTag("Ears");


        // getting the CasA
        if(MSL == MovementSystem.MS1)
        {
            CasALeft = GameObject.FindGameObjectWithTag("SuperNovaLeft");
            CasARight = GameObject.FindGameObjectWithTag("SuperNovaRight");
        } else
        {
            CasA = GameObject.FindGameObjectWithTag("SuperNova");
        }
        

        // getting the rotator transform 
        rotator = transform.parent.transform;

        // getting the headfollow
        headFollow = GameObject.FindGameObjectWithTag("HeadFollow");

        curFacingDir = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand == null)
        {
            // getting the right hand
            rightHand = GameObject.FindGameObjectWithTag("SensorRight");
        }
        else
        {
            UpdateRotation();
        }
    }

    private void UpdateRotation()
    {
        // getting the current position
        // pos from the origin of the supernova to the headset
        Vector3 curPos;
        
        // for finding which hand is active in MS1, or if it is MS1 at all.
        if (MSL == MovementSystem.MS1) {
            if (CasALeft.activeSelf)
            {
                curPos = headFollow.transform.InverseTransformPoint(CasALeft.transform.position) / CasALeft.transform.localScale.x;
            }
            else
            {
                curPos = headFollow.transform.InverseTransformPoint(CasARight.transform.position) / CasARight.transform.localScale.x;
            }
        } else
        {
            curPos = headFollow.transform.InverseTransformPoint(CasA.transform.position);
        }
        
         Vector3 roundedPos = new Vector3(Mathf.RoundToInt(curPos.x),
             Mathf.RoundToInt(curPos.y),
             Mathf.RoundToInt(curPos.z));

        if (gradientData.ContainsKey(roundedPos))
        {
            Vector3 dirToLook;
            Quaternion rotation;

            //Debug.Log("In If statement: ASRotation");
            Vector3 gradientVector = gradientData[roundedPos];
            Debug.Log("Position: " + roundedPos);
            Debug.Log("Gradient: " + gradientVector);
            if (gradientVector != Vector3.zero)
            {
                // getting the direction to look and the needed rotation
                dirToLook = rotator.transform.InverseTransformDirection(gradientVector).normalized;
                if (WantMinimum)
                {
                    dirToLook = -dirToLook;
                    Debug.Log("Gradient direction (Min): " + dirToLook);
                }
                else
                {
                    Debug.Log("Gradient direction(Max): " + dirToLook);
                }
                rotation = Quaternion.LookRotation(dirToLook);
                Debug.Log("rotation: " + rotation);

                // rotating, slerp or lerp??
                rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, rotation, Time.deltaTime * .1f);
            }

        }
    }
}
