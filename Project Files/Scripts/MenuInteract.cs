//#define SOUND
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using RealSpace3D;

public class MenuInteract : VRTK_InteractableObject {

    // move turning off on startup to the canvas script!!

    private bool showText = false;
    private GameObject canvas;
#if SOUND
    private RealSpace3D_AudioSource audioSource;
#endif

    public string description;
    public string title;
    
    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        showText = true;
        canvas.SetActive(true);
        Debug.Log("Shit is on");
#if SOUND
        audioSource.rs3d_PlaySound();
#endif
    }

    public override void StopUsing(VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        showText = false;
        canvas.SetActive(false);
        Debug.Log("Shit has stopped");
#if SOUND
        audioSource.rs3d_StopSound();
#endif
    }


    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
#if SOUND
        audioSource = gameObject.transform.GetChild(1).gameObject.GetComponent<RealSpace3D_AudioSource>();
#endif
    }
	
	// Update is called once per frame
	protected override void Update () {
        
	}

    public bool isShowText()
    {
        return showText;
    }
}
