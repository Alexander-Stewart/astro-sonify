using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateText : MonoBehaviour {

    private bool startUp = true;
    public List<GameObject> textColliders;
    private List<MenuInteract> MenuInteracts;
    private Text screenText;
    private Text title;

	// Use this for initialization
	void Start () {
        MenuInteracts = new List<MenuInteract>(textColliders.Count);
        if (textColliders.Count == 0)
        {
            Debug.Log("Please add objects onto the ActivateText Script, it is on the canvas object.");
        }
        else
        {
            foreach (GameObject gO in textColliders)
            {
                MenuInteract mI = gO.GetComponent<MenuInteract>();
                MenuInteracts.Add(mI);
            }
        }

        GameObject textObj1 = transform.GetChild(0).gameObject;
        title = textObj1.GetComponent<Text>();

        GameObject textObj2 = transform.GetChild(1).gameObject;
        screenText = textObj2.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (startUp)
        //{
        //    Debug.Log("here");
        //    gameObject.SetActive(false);
        //    startUp = false;
        //}
        foreach (MenuInteract mI in MenuInteracts)
        {
            if (mI.isShowText())
            {
                title.text = mI.title;
                screenText.text = mI.description;
            }
        }
	}
}
