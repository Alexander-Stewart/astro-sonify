using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;

public class NearOrigin : MonoBehaviour {
//#if MS1
    public float delayTime;
    public ushort hapticStrength;
    private bool inOrigin = false;
    private bool leftOrigin = true;

    private SteamVR_TrackedObject trackedRightHand;
    private SteamVR_TrackedObject trackedLeftHand;

    private SteamVR_Controller.Device device1;
    private SteamVR_Controller.Device device2;

    private GameObject superNovaLeftObject;
    private GameObject superNovaRightObject;
    //private VRTK_TransformFollow superNovaLeft;
    //private VRTK_TransformFollow superNovaRight;
    public GameObject leftHand;


    // Use this for initialization
    void Start()
    {
        // getting the left hand and supernova game objects(supernova directly gets the transform follow component)
        superNovaLeftObject = GameObject.FindGameObjectWithTag("SuperNovaLeft");
        superNovaRightObject = GameObject.FindGameObjectWithTag("SuperNovaRight");
        //superNovaLeft = superNovaLeftObject.GetComponent<VRTK_TransformFollow>();
        //superNovaRight = superNovaRightObject.GetComponent<VRTK_TransformFollow>();
        
        superNovaRightObject.SetActive(false);
        Debug.Log("This is the left hand: " + leftHand);

        // getting tracked object components
        trackedLeftHand = leftHand.GetComponent<SteamVR_TrackedObject>();
        trackedRightHand = GetComponent<SteamVR_TrackedObject>();
        
    }
	
	// Update is called once per frame
	void Update () {
        device1 = SteamVR_Controller.Input((int)trackedRightHand.index);
        device2 = SteamVR_Controller.Input((int)trackedLeftHand.index);
        if (inOrigin)
        {
            if (leftOrigin)
            {
                device2.TriggerHapticPulse(hapticStrength);
            } else
            {
                device1.TriggerHapticPulse(hapticStrength);
            }
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("Entered");
   
        if(Other.tag == "LeftController")
        {
            StartCoroutine("MyCoroutine");
            Debug.Log("Tagged");
            inOrigin = true;
        }
    }

    void OnTriggerExit(Collider Other)
    {
        Debug.Log("Exited");

        if(Other.tag == "LeftController")
        {
            StopCoroutine("MyCoroutine");
            
            inOrigin = false;
        }
    }


    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(delayTime);

        if (leftOrigin)
        {
            // changes origin to right hand
            superNovaRightObject.SetActive(true);
            superNovaRightObject.transform.localScale = superNovaLeftObject.transform.localScale;
            superNovaLeftObject.SetActive(false);
            

            // updates vibration to right hand
            leftOrigin = false;
        } else
        {
            // switches origin to left hand
            superNovaLeftObject.SetActive(true);
            superNovaLeftObject.transform.localScale = superNovaRightObject.transform.localScale;
            superNovaRightObject.SetActive(false);
            

            // updates vibration to left hand
            leftOrigin = true;
        }

    }

    // private VRTK_ControllerReference LController;
    //private VRTK_ControllerReference RController;

    // LController = VRTK_ControllerReference.GetControllerReference(3);
    // RController = VRTK_ControllerReference.GetControllerReference(4);

    // VRTK_ControllerHaptics.TriggerHapticPulse(RController, .5f, 1.5f, .5f);
    // VRTK_ControllerHaptics.TriggerHapticPulse(LController, .5f, 1.5f, .5f);

    // Debug.Log("This is the Left controller: " + LController);
    //  Debug.Log("This is the Right controller: " + RController);
//#endif
}
