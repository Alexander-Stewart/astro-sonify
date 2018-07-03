using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Audio;

public class MenuInteract : VRTK_InteractableObject {

    // move turning off on startup to the canvas script!!

    private bool showText = false;
    private GameObject canvas;
    private AudioSource audioSource;

    public string description;
    public string title;
    
    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        showText = true;
        canvas.SetActive(true);
        Debug.Log("Shit is on");
        audioSource.PlayDelayed(.5f);
    }

    public override void StopUsing(VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        showText = false;
        canvas.SetActive(false);
        Debug.Log("Shit has stopped");
        audioSource.Stop();
    }


    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        audioSource = gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }

    public bool isShowText()
    {
        return showText;
    }
}
