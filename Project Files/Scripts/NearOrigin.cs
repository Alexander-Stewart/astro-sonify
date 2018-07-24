//#define ALT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;


/**
     * NearOrigin is a script that handles haptic feedback
     * for when the controllers are near each other,
     * and also handles switching which controller is mainly
     * used for movement/control in the Movement System.
     * **I want to change many of the public game objects to private,
     * but was having trouble getting them through script, will update later**
     **/
public class NearOrigin : MonoBehaviour
{

    private MovementSystem MS;

    public float delayTime;
    public ushort hapticStrength;
    private bool inOrigin = false;
    [HideInInspector]
    public bool leftOrigin = true;

    private SteamVR_TrackedObject trackedRightHand;
    private SteamVR_TrackedObject trackedLeftHand;

    private SteamVR_Controller.Device device1;
    private SteamVR_Controller.Device device2;

    public GameObject leftHand;

    // FOR MS1

    private GameObject superNovaLeftObject;
    private GameObject superNovaRightObject;

    // FOR MS1

    // F0R MS2
    private VRTK_TransformFollow leftFollow;
    private VRTK_TransformFollow rightFollow;

    private SteamVR_RenderModel leftRender;

    private GameObject leftModel;
    private GameObject rightModel;

    private GameObject cameraRig;

    // FOR MS2
#if ALT
    private VRTK_TransformFollow leftHandFollow;
    private VRTK_TransformFollow rightHandFollow;
#endif

    // Use this for initialization
    void Start()
    {

        //getting MS!!
        MS = GameObject.Find("MSLogger").GetComponent<MSLogger>().MS;


        // if the MS is MS1
        if (MS == MovementSystem.MS1)
        {
            // getting the left hand and supernova game objects(supernova directly gets the transform follow component)
            superNovaLeftObject = GameObject.FindGameObjectWithTag("SuperNovaLeft");
            superNovaRightObject = GameObject.FindGameObjectWithTag("SuperNovaRight");

            superNovaRightObject.SetActive(false);
            Debug.Log("This is the left hand: " + leftHand);

            // getting tracked object components
            trackedLeftHand = leftHand.GetComponent<SteamVR_TrackedObject>();
            trackedRightHand = GetComponent<SteamVR_TrackedObject>();

#if ALT
        leftHandFollow = leftHand.GetComponent<VRTK_TransformFollow>();
        rightHandFollow = GetComponent<VRTK_TransformFollow>();
        leftHandFollow.enabled = false;
#endif
        }
        // If the MS is MS2
        else if (MS == MovementSystem.MS2)
        {
            // getting tracked object components
            trackedLeftHand = leftHand.GetComponent<SteamVR_TrackedObject>();
            trackedRightHand = GetComponent<SteamVR_TrackedObject>();

            // getting the models for the  controllers.
            leftModel = leftHand.transform.GetChild(0).gameObject;
            rightModel = transform.GetChild(0).gameObject;

            //setting up left model
            leftRender = leftModel.GetComponent<SteamVR_RenderModel>();

            // getting the VRTK_TransformFollow components
            leftFollow = leftHand.GetComponent<VRTK_TransformFollow>();
            rightFollow = GetComponent<VRTK_TransformFollow>();

            // getting cameraRig and childing the right hand.
            cameraRig = transform.parent.gameObject;
            ChildToObject(this.gameObject, false, leftHand);
        }

    }

    // Update is called once per frame
    void Update()
    {
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
        // MS1
        if (MS == MovementSystem.MS1)
        {
            Debug.Log("Entered");

            if (Other.tag == "LeftController")
            {
                StartCoroutine("MyCoroutine");
                Debug.Log("Tagged");
                inOrigin = true;
            }
        }

        // MS2
        if (MS == MovementSystem.MS2)
        {
            Debug.Log("Entered");

            if (Other.tag == "SensorLeft" || Other.tag == "SensorRight")
            {
                StartCoroutine("MyCoroutine");
                Debug.Log("Tagged");
                inOrigin = true;
            }
        }
    }

    void OnTriggerExit(Collider Other)
    {
        // MS1
        if (MS == MovementSystem.MS1)
        {
            Debug.Log("Exited");

            if (Other.tag == "LeftController")
            {
                StopCoroutine("MyCoroutine");

                inOrigin = false;
            }
        }

        // MS2
        if (MS == MovementSystem.MS2)
        {
            Debug.Log("Exited");

            if (Other.tag == "SensorLeft" || Other.tag == "SensorRight")
            {
                StopCoroutine("MyCoroutine");

                inOrigin = false;
            }
        }
    }


    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(delayTime);

        // MS1
        if (MS == MovementSystem.MS1)
        {
            if (leftOrigin)
            {
#if ALT
            rightHandFollow.enabled = false;
            leftHandFollow.enabled = true;
#endif
                // changes origin to right hand
                superNovaRightObject.SetActive(true);
                superNovaRightObject.transform.localScale = superNovaLeftObject.transform.localScale;
                superNovaLeftObject.SetActive(false);


                // updates vibration to right hand
                leftOrigin = false;
            }
            else
            {
#if ALT
            leftHandFollow.enabled = false;
            rightHandFollow.enabled = true;
#endif
                // switches origin to left hand
                superNovaLeftObject.SetActive(true);
                superNovaLeftObject.transform.localScale = superNovaRightObject.transform.localScale;
                superNovaRightObject.SetActive(false);


                // updates vibration to left hand
                leftOrigin = true;
            }
        }

        // MS2
        if (MS == MovementSystem.MS2)
        {
            if (leftOrigin)
            {
                leftRender.index = trackedLeftHand.index;
                // changes origin to right hand
                leftFollow.enabled = false;
                yield return new WaitForSeconds(.5f);
                rightFollow.enabled = true;

                // childing left hand and unchilding right
                ChildToObject(this.gameObject, true, leftHand);
                ChildToObject(leftHand, false, this.gameObject);

                // updates vibration to right hand
                leftOrigin = false;
            }
            else
            {
                // switches origin to left hand
                rightFollow.enabled = false;
                yield return new WaitForSeconds(.5f);
                leftFollow.enabled = true;

                // childing right hand and unchilding left
                ChildToObject(leftHand, true, this.gameObject);
                ChildToObject(this.gameObject, false, leftHand);

                // updates vibration to left hand
                leftOrigin = true;
            }
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
    protected virtual void ChildToObject(GameObject hand, bool toCameraRig, GameObject otherHand)
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

        }
        else
        {
            newParent = otherHand.transform;

            hand.transform.SetParent(newParent);
            hand.transform.localPosition = Vector3.zero;
        }
    }


#if ALT
    // for the audioManager to get the bool leftOrigin!
    public bool getLeftOrigin()
    {
        return leftOrigin;
    }
#endif
}
