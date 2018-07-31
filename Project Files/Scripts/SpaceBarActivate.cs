using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBarActivate : MonoBehaviour {

    public GameObject menu;


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            menu.SetActive(!menu.activeSelf);
        }
	}


}
