using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DrumActivate : MonoBehaviour {

    private AudioSource drumSource;

	// Use this for initialization
	void Start () {

        // getting the audio source
        drumSource = GetComponent<AudioSource>();
        Debug.Log("NEED THE HEAD TO HAVE A SPHERE COLLIDER AND MAKE IT A TRIGGER!");
	}
	
    // for starting the clip
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Entered");
        if (!drumSource.isPlaying)
        {
            drumSource.Play();
        }
    }

    // for ending the clip
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        if (drumSource.isPlaying)
        {
            drumSource.Pause();
        }
    }
}
