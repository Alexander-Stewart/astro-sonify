using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class ASRotation : MonoBehaviour {


    //actually where the head of the person is
    private GameObject audioListener;
    //right hand is to tell "where the user is"
    private GameObject rightHand;
    private Dictionary<Vector3, Vector3> gradientData;
    private GameObject CasA;


    // Use this for initialization
    void Start () {
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        DataReader dataReader = playArea.GetComponent<DataReader>();
        gradientData = dataReader.gradientData;

        CasA = GameObject.FindGameObjectWithTag("Origin");

        rightHand = GameObject.FindGameObjectWithTag("SensorRight");

        audioListener = GameObject.FindGameObjectWithTag("Ears");

	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(rightHand.transform);

        //now to rotate!!!
        Vector3 curPos = rightHand.transform.InverseTransformPoint(CasA.transform.position);
        Vector3 roundedPos = new Vector3(Mathf.RoundToInt(curPos.x),
            Mathf.RoundToInt(curPos.y),
            Mathf.RoundToInt(curPos.z));

        Debug.Log("curPos: " + roundedPos);

        if (gradientData.ContainsKey(roundedPos))
        {
            Debug.Log("Hwewwe");
            Vector3 rotationalAxis = Vector3.Cross(audioListener.transform.position - gradientData[roundedPos],
                                                        audioListener.transform.position - transform.position);

            float angle = Vector3.Angle(audioListener.transform.position - transform.position, 
                                            audioListener.transform.position - gradientData[roundedPos]);

            transform.RotateAround(audioListener.transform.position, rotationalAxis, angle);
        }
         
	}
}
