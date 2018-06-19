using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using RealSpace3D;

public class AudioManager : MonoBehaviour {
    /**
     * this is the audio manager, where all audio transformations will take place
     **/
    private RealSpace3D_AudioSource audioSource;
    private GameObject audioSourceGameObject;
    private GameObject audioListener;

    // Use this for initialization
    void Start () {
        audioSourceGameObject = GameObject.Find("RS3D_AudioSource");
        Debug.Log("The Audio Source Game Object is: " + audioSourceGameObject);

        audioSource = audioSourceGameObject.GetComponent<RealSpace3D_AudioSource>();

        audioListener = GameObject.FindGameObjectWithTag("Ears");
        Debug.Log("The Audio Listener Game Object is: " + audioListener);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSound();
	}

    /**
     * used to update the sound a given audios source.
     **/
    private void UpdateSound()
    {
        float distance = Vector3.Distance(audioListener.transform.position, audioSourceGameObject.transform.position);
        Debug.Log("The Distance between the Audio Source and the Audio Listener is: " + distance);

        audioSource.rs3d_AdjustPitch(3f * Mathf.Sin(distance * 2f));
    }
}
