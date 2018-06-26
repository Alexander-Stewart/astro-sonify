#define ALT
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using RealSpace3D;
using VRTK;

public class AudioManager : MonoBehaviour {
    /**
     * this is the audio manager, where all audio transformations will take place
     **/
    private RealSpace3D_AudioSource audioSource;
    private GameObject audioSourceGameObject;
    private GameObject audioListener;
    // will find way to make private!
    public AudioMixer audioMixer;

    //if Alt
#if ALT
    private NearOrigin nearOriginScript;

    private VRTK_TransformFollow audioFollowLeft;
    private VRTK_TransformFollow audioFollowRight;

    public GameObject leftHandController;
    public GameObject rightHandController;
#endif

    private Dictionary<Vector3, float> densData;


    // Use this for initialization
    void Start () {
        audioSourceGameObject = GameObject.Find("RS3D_AudioSource");
        //Debug.Log("The Audio Source Game Object is: " + audioSourceGameObject);

        //if MS1 Alt
#if ALT
        //GameObject temp = GameObject.FindGameObjectWithTag("SensorRight");
        nearOriginScript = rightHandController.GetComponent<NearOrigin>();

        VRTK_TransformFollow[] theScripts =  GetComponents<VRTK_TransformFollow>();

        audioFollowLeft = theScripts[0];
        audioFollowRight = theScripts[1];

        //getting the controllers
        //leftHandController = GameObject.FindGameObjectWithTag("SensorLeft");
        //rightHandController = GameObject.FindGameObjectWithTag("SensorRight");
#endif

        audioSource = audioSourceGameObject.GetComponent<RealSpace3D_AudioSource>();

        audioListener = GameObject.FindGameObjectWithTag("Ears");
        //Debug.Log("The Audio Listener Game Object is: " + audioListener);

        // getting audioMixer
        //Debug.Log("This is the Audio Mixer: " + audioMixer);

        // getting densData
        densData = GetComponent<DataReader>().densData;
	}
	
	// Update is called once per frame
	void Update () {
        if (nearOriginScript.getLeftOrigin())
        {
            audioFollowRight.enabled = true;
            audioFollowLeft.enabled = false;
        } else
        {
            audioFollowLeft.enabled = true;
            audioFollowRight.enabled = false;
        }

        UpdateSound();
        //UpdateMixer();
	}

    /**
     * used to update the sound a given audios source.
     **/
    private void UpdateSound()
    {
#if ALT
        float density = 0;
        Vector3 curPos;
        Vector3 roundedPos;
        if (nearOriginScript.getLeftOrigin())
        {
            curPos = rightHandController.transform.InverseTransformPoint(leftHandController.transform.position);
        } else
        {
            curPos = leftHandController.transform.InverseTransformPoint(rightHandController.transform.position);
        }

        roundedPos = new Vector3((float)Math.Round(curPos.x, 1),
               (float)Math.Round(curPos.y, 1),
               (float)Math.Round(curPos.z, 1));
#else
        float density = 1;
        Vector3 curPos = audioListener.transform.InverseTransformPoint(audioSourceGameObject.transform.position);
        Vector3 roundedPos = new Vector3(Mathf.RoundToInt(curPos.x), 
            Mathf.RoundToInt(curPos.y), 
            Mathf.RoundToInt(curPos.z));
#endif

        if (densData.ContainsKey(roundedPos))
        {
            density = densData[roundedPos] / 2f;
        }
        if (density == 0)
        {
            density = .5f;
        }
        //float distance = Vector3.Distance(audioListener.transform.position, audioSourceGameObject.transform.position);
        //Debug.Log("The Distance between the Audio Source and the Audio Listener is: " + distance);
        Debug.Log("The rounded Point is: " + roundedPos);
        Debug.Log("The Density is: " + density);


        audioSource.rs3d_AdjustPitch(3f * Mathf.Sin(density / 5f));
    }

    private void UpdateMixer()
    {
        float heightDif = Mathf.Abs(audioListener.transform.position.y - audioSourceGameObject.transform.position.y)/5f;
        float headRotation = (audioListener.transform.parent.localEulerAngles.y) / 100f / 8f;
        float invHeightDif = 1f - heightDif;
        float invHeadRotation = 0f;
        if(headRotation <= 0)
        {
            invHeadRotation = Mathf.Abs(headRotation);
            headRotation = 1f - invHeadRotation;
        } else
        {
            invHeadRotation = 1f - headRotation;
        }
        //Debug.Log("The height Difference between the Audio Source and the Audio Listener is: " + heightDif);
        //Debug.Log("The rotation of the audioListener is: " + headRotation);

        //changing audioMixer effects
        audioMixer.SetFloat("EchoMix", headRotation);
        audioMixer.SetFloat("FlangeMix", heightDif);
        audioMixer.SetFloat("EchoDry", invHeadRotation);
        audioMixer.SetFloat("FlangeDry", invHeightDif);
    }
}
