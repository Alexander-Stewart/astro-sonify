﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuInteract : VRTK_InteractableObject {

    // move turning off on startup to the canvas script!!

    private bool startUp = true;
    private bool showText = false;
    private GameObject canvas;

    public string description;
    public string title;
    
    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        showText = true;
        Debug.Log("Shit is on");
    }

    public override void StopUsing(VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        showText = false;
        Debug.Log("Shit has stopped");
    }


    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (startUp)
        {
            Debug.Log("here");
            canvas.SetActive(false);
            startUp = false;
        }

        if (showText)
        {
            canvas.SetActive(true);
        } else
        {
            canvas.SetActive(false);
        }
	}

    public bool isShowText()
    {
        return showText;
    }
}
