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
        
	}
	
    // for starting the clip
    void OnCollisionEnter(Collision col)
    {
        if (!drumSource.isPlaying)
        {
            drumSource.Play();
        }
    }

    // for ending the clip
    void OnCollisionExit(Collision col)
    {
        if(drumSource.isPlaying)
        {
            drumSource.Stop();
        }
    }
}
