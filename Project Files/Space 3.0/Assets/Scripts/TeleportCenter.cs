using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportCenter : MonoBehaviour {

    private VRTK_DashTeleport dashTeleport;

	// Use this for initialization
	void Start () {
        GameObject playAreaAlias = GameObject.Find("PlayArea");
        dashTeleport = playAreaAlias.GetComponent<VRTK_DashTeleport>();
        Debug.Log("This is the Teleport Component: " + dashTeleport);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Teleporting....");
        dashTeleport.ForceTeleport(Vector3.zero);
    }
}
