using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSLogger : MonoBehaviour {

	// Use this for initialization
	void Awake () {
#if MS1
        Debug.Log("MS1 Active");
#elif MS2
        Debug.Log("MS2 Active");
#elif MS3
        Debug.Log("MS3 Active");
#else 
        Debug.Log("Please Activate a MS inside the Player Options in Build Settings.");
#endif
    }
	
	
}
