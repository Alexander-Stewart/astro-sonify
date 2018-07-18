using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        // getting the gradient data
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        gradientData = playArea.GetComponent<DataReader>().gradientData;

        // getting the right hand
        rightHand = GameObject.FindGameObjectWithTag("SensorRight");

        // getting the GameObject the Audio Listener is on
        audioListener = GameObject.FindGameObjectWithTag("Ears");

        // getting the CasA
        CasA = GameObject.FindGameObjectWithTag("Origin");

        // getting the rotator transform 
        rotator = transform.parent.transform;

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

        Vector3 curPos;
        curPos = rightHand.transform.InverseTransformPoint(CasA.transform.position);

         Vector3 roundedPos = new Vector3(Mathf.RoundToInt(curPos.x),
             Mathf.RoundToInt(curPos.y),
             Mathf.RoundToInt(curPos.z));

        if (gradientData.ContainsKey(roundedPos))
        {
            Vector3 dirToLook;
            Quaternion rotation;

            Debug.Log("In If statement: ASRotation");
            Vector3 gradientVector = gradientData[roundedPos];
            Debug.Log("Gradient: " + gradientVector);
            if (gradientVector != Vector3.zero)
            {
                // getting the direction to look and the needed rotation
                dirToLook = rotator.transform.InverseTransformDirection(gradientVector).normalized;
                rotation = Quaternion.LookRotation(dirToLook);

                // rotating
                rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, rotation, Time.deltaTime);
            }

        }
    }
}
