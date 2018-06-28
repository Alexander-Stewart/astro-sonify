using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateText : MonoBehaviour {

    public List<GameObject> textColliders;
    private List<MenuInteract> MenuInteracts;
    private Text screenText;

	// Use this for initialization
	void Start () {
        MenuInteracts = new List<MenuInteract>(textColliders.Count);
        foreach(GameObject gO in textColliders){
            MenuInteract mI = gO.GetComponent<MenuInteract>();
            MenuInteracts.Add(mI);
        }

        GameObject textObj = transform.GetChild(0).gameObject;
        screenText = textObj.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(MenuInteract mI in MenuInteracts)
        {
            if (mI.isShowText())
            {
                screenText.text = mI.description;
            }
        }
	}
}
