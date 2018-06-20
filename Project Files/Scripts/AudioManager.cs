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
    // will find way to make private!
    public AudioMixer audioMixer;

    // Use this for initialization
    void Start () {
        audioSourceGameObject = GameObject.Find("RS3D_AudioSource");
        Debug.Log("The Audio Source Game Object is: " + audioSourceGameObject);

        audioSource = audioSourceGameObject.GetComponent<RealSpace3D_AudioSource>();

        audioListener = GameObject.FindGameObjectWithTag("Ears");
        Debug.Log("The Audio Listener Game Object is: " + audioListener);

        // getting audioMixer
        Debug.Log("This is the Audio Mixer: " + audioMixer);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSound();
        UpdateMixer();
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

    private void UpdateMixer()
    {
        float heightDif = Mathf.Abs(audioListener.transform.position.y - audioSourceGameObject.transform.position.y)/5f;
        float headRotation = (audioListener.transform.parent.localEulerAngles.y) / 100f / 8f;
        float invHeightDif = 1f - heightDif;
        float invHeadRotation = 0f;
        if(headRotation <= 0)
        {
            invHeadRotation = Mathf.Abs(headRotation);
            headRotation = 1f - invHeadRotation;
        } else
        {
            invHeadRotation = 1f - headRotation;
        }
        Debug.Log("The height Difference between the Audio Source and the Audio Listener is: " + heightDif);
        Debug.Log("The rotation of the audioListener is: " + headRotation);

        //changing audioMixer effects
        audioMixer.SetFloat("EchoMix", headRotation);
        audioMixer.SetFloat("FlangeMix", heightDif);
        audioMixer.SetFloat("EchoDry", invHeadRotation);
        audioMixer.SetFloat("FlangeDry", invHeightDif);
    }
}
