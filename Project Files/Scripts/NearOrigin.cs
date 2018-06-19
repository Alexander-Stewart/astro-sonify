using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;

public class NearOrigin : MonoBehaviour {
    /**
     * NearOrigin is a script that handles haptic feedback
     * for when the controllers are near each other,
     * and also handles switching which controller is mainly
     * used for movement/control in the Movement System.
     * **I want to change many of the public game objects to private,
     * but was having trouble getting them through script, will update later**
     **/
#if MS1
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
#elif MS2
    public float delayTime;
    public ushort hapticStrength;
    private bool inOrigin = false;
    private bool leftOrigin = true;

    private SteamVR_TrackedObject trackedRightHand;
    private SteamVR_TrackedObject trackedLeftHand;

    private SteamVR_Controller.Device device1;
    private SteamVR_Controller.Device device2;

    private VRTK_TransformFollow leftFollow;
    private VRTK_TransformFollow rightFollow;

    private SteamVR_RenderModel leftRender;

    private GameObject leftModel;
    private GameObject rightModel;

    public GameObject leftHand;

    private GameObject cameraRig;

    void Start()
    {
        // getting tracked object components
        trackedLeftHand = leftHand.GetComponent<SteamVR_TrackedObject>();
        trackedRightHand = GetComponent<SteamVR_TrackedObject>();

        // getting the models for the  controllers.
        leftModel = leftHand.transform.GetChild(0).gameObject;
        rightModel = transform.GetChild(0).gameObject;

        //setting up left model
        leftRender = leftModel.GetComponent<SteamVR_RenderModel>();
        leftModel.SetActive(false);

        // getting the VRTK_TransformFollow components
        leftFollow = leftHand.GetComponent<VRTK_TransformFollow>();
        rightFollow = GetComponent<VRTK_TransformFollow>();

        // getting cameraRig and childing the right hand.
        cameraRig = transform.parent.gameObject;
        ChildToObject(this.gameObject, false);

    }

    // Update is called once per frame
    void Update()
    {
        // device1 is right hand, device 2 is left hand.
        device1 = SteamVR_Controller.Input((int)trackedRightHand.index);
        device2 = SteamVR_Controller.Input((int)trackedLeftHand.index);
        if (inOrigin)
        {
            if (leftOrigin)
            {
                device2.TriggerHapticPulse(hapticStrength);
            }
            else
            {
                device1.TriggerHapticPulse(hapticStrength);
            }
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("Entered");

        if (Other.tag == "SensorLeft" || Other.tag == "SensorRight")
        {
            StartCoroutine("MyCoroutine");
            Debug.Log("Tagged");
            inOrigin = true;
        }
    }

    void OnTriggerExit(Collider Other)
    {
        Debug.Log("Exited");

        if (Other.tag == "SensorLeft" || Other.tag == "SensorRight")
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
            leftRender.index = trackedLeftHand.index;
            // changes origin to right hand
            leftFollow.enabled = false;
            yield return new WaitForSeconds(.5f);
            rightModel.SetActive(false);
            leftModel.SetActive(true);
            rightFollow.enabled = true;

            // childing left hand and unchilding right
            ChildToObject(this.gameObject, true);
            ChildToObject(leftHand, false);
            
            // updates vibration to right hand
            leftOrigin = false;
        }
        else
        {
            // switches origin to left hand
            rightFollow.enabled = false;
            yield return new WaitForSeconds(.5f);
            leftModel.SetActive(false);
            rightModel.SetActive(true);
            leftFollow.enabled = true;

            // childing right hand and unchilding left
            ChildToObject(leftHand, true);
            ChildToObject(this.gameObject, false);

            // updates vibration to left hand
            leftOrigin = true;
        }
    }

    /**
     * ChildToObject is a helper method that allows for a game object to 
     * be set as a child to either the camera rig or the main camera.
     * @params: hand- the hand game object is which hand you want to be added
     * as a child
     * @params: toCameraRig- a bool telling whether or not to add the child to
     *  the camera rig.
     **/
    protected virtual void ChildToObject(GameObject hand, bool toCameraRig)
    {
        Vector3 currentPosition = hand.transform.localPosition;
        Quaternion currentRotation = hand.transform.localRotation;
        Vector3 currentScale = hand.transform.localScale;
        Transform newParent = null;

        if (toCameraRig)
        {
            newParent = cameraRig.transform;

            hand.transform.SetParent(newParent);
            hand.transform.localPosition = currentPosition;
            hand.transform.localRotation = currentRotation;

        } else
        {
            newParent = VRTK_DeviceFinder.HeadsetTransform();

            hand.transform.SetParent(newParent);
            hand.transform.localPosition = Vector3.zero;
        }
    }
#endif
}
