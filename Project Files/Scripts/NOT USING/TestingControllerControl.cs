using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TestingControllerControl : MonoBehaviour {
#if false
    // in this test script written inside is:
    // 1. A method ChangeSizeUp that increases the size of the superNova by pressing the trigger
    // 2. A method ChangeSizeDown that decreases the size of the superNova by pressing the analog trigger in.
    // ** The above methods can be used without VRTK.


    //Device.TriggerhapticPulse() can be used to send haptic feedback to user

    protected SteamVR_TrackedObject trackedObj;
    public GameObject superNova;
    public Vector3 wantedSuperNovaPos;
    
    // this variable is called a property.
    // use like a variable, but actually special method called accessor.
    // this is like a special variable where when called or set to new value it will carry out special actions that are requested.
    // for device, instead of storing device, it gets the input that was made to device and stores it.
    public SteamVR_Controller.Device device
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

	// Use this for initialization
	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        ChangeSizeUp();
        ChangeSizeUDown();
	}

    void ChangeSizeUp() {
        if (device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            Vector3 size = superNova.transform.localScale;

            if(!(size.x == 100 || size.x == 100)) {
                size = size * 10;
                superNova.transform.localScale = size;
                superNova.transform.localPosition = wantedSuperNovaPos;
            }
        }
    }

    void ChangeSizeUDown()
    {
        if (device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            Vector3 size = superNova.transform.localScale;

            if (!(size.x == 1 || size.x == 1))
            {
                size = size / 10;
                superNova.transform.localScale = size;
                superNova.transform.localPosition = wantedSuperNovaPos;
            }
        }
    }

#endif
}
