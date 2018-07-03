using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour {
#if false
    // Use this for initialization
    void Start () {
        Debug.Log("Script is on");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("Entered");
    }

    void OnTriggerExit(Collider Other)
    {
        Debug.Log("Exited");
    }
#endif
}
