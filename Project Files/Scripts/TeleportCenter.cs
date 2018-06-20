using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportCenter : MonoBehaviour {

    private VRTK_HeightAdjustTeleport HATeleport;

	// Use this for initialization
	void Start () {
        GameObject playAreaAlias = GameObject.Find("PlayArea");
        HATeleport = playAreaAlias.GetComponent<VRTK_HeightAdjustTeleport>();
        Debug.Log("This is the Teleport Component: " + HATeleport);

        HATeleport.ForceTeleport(this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
