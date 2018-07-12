#define AS2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using RealSpace3D;

[RequireComponent(typeof(DataReader))]
public class AM2 : MonoBehaviour {

    private RealSpace3D_AudioSource audioSource;
    private GameObject audioSourceGameObject;
    private GameObject audioListener;
    // will find way to make private!
    public AudioMixer audioMixer;

    // not good coding style
    private int currentSoundIndex = 100;
    private int nextSoundIndex;

    public float fadeTime = 1f;
    public float musicVolume = .5f;

    private Dictionary<Vector3, float> densData;


    // Use this for initialization
    void Start () {
        audioSourceGameObject = GameObject.Find("RS3D_AudioSource");

        audioSource = audioSourceGameObject.GetComponent<RealSpace3D_AudioSource>();

        audioListener = GameObject.FindGameObjectWithTag("Ears");

        // getting densData
        densData = GetComponent<DataReader>().densData;
    }
	
	// Update is called once per frame
	void Update () {
        if (audioListener == null)
        {
            audioListener = GameObject.FindGameObjectWithTag("Ears");
        }
        UpdateSound();
        //UpdateMixer();
    }
    
    private void UpdateSound()
    {
        float density = 1;
        Vector3 curPos = audioListener.transform.InverseTransformPoint(audioSourceGameObject.transform.position);
        Vector3 roundedPos = new Vector3(Mathf.RoundToInt(curPos.x),
            Mathf.RoundToInt(curPos.y),
            Mathf.RoundToInt(curPos.z));

        if (densData.ContainsKey(roundedPos))
        {
#if AS2
            density = densData[roundedPos];
#else
            density = densData[roundedPos] / 2f;
#endif
        }
        if (density == 0)
        {
            density = .5f;
        }
#if AS2
        SwitchSound(density);
        //Debug.Log("The rounded Point is: " + roundedPos);
        Debug.Log("The Density is: " + density);
        
        //scaling the density down to the range of the pitch modulation.
        if (density >= -25f && density < -10f)
        {
            density = Scale(density, -25f, -10f, -3f, 3f);
        }

        else if (density >= -10f && density <= 10f)
        {
            density = Scale(density, -10f, 10f, -3f, 3f);
        }

        else if (density > 10f && density <= 25f)
        {
            density = Scale(density, 10f, 25f, -3f, 3f);
        }

        Debug.Log("This is the scaled density: " + density);
        //audioSource.rs3d_AdjustPitch(density * .5f, currentSoundIndex);
#else
        Debug.Log("The rounded Point is: " + roundedPos);
        Debug.Log("The Density is: " + density);
        audioSource.rs3d_AdjustPitch(3f * Mathf.Sin(density / 10f), currentSoundIndex);
#endif
    }

#if AS2
    private void SwitchSound(float density)
    {
      if(density >= -25f && density < -10f)
        {
            if (!audioSource.rs3d_IsPlaying(0))
            {
                nextSoundIndex = 0;

                StartCoroutine(CrossFade(fadeTime));

                Debug.Log("This is the current note Range: c");
            }
        }

        else if (density >= -10f && density <= 10f)
        {
            if (!audioSource.rs3d_IsPlaying(1))
            {
                nextSoundIndex = 1;
        
                StartCoroutine(CrossFade(fadeTime));

                Debug.Log("This is the current note Range: d");
            }
        }

        else if (density > 10f && density <= 25f)
        {
            if (!audioSource.rs3d_IsPlaying(2))
            {
                nextSoundIndex = 2;
                StartCoroutine(CrossFade(fadeTime));

                Debug.Log("This is the current note Range: e");
            }
        }
        //Debug.Log(audioSource.rs3d_GetCurrentClipPlaying());
    }


    private IEnumerator CrossFade(float fadeTime)
    {

        //Debug.Log("Inside Cor");
        float stepInterval = fadeTime / 20.0f;
        float volInterval = musicVolume / 20.0f;

       

        // if there has not been a sound played yet.
        if(currentSoundIndex == 100)
        {
            //Debug.Log("Inside Startup");
            //starting up next sound
            audioSource.rs3d_PlaySound(nextSoundIndex);

            currentSoundIndex = nextSoundIndex;

        } else if(currentSoundIndex != nextSoundIndex)
        {

            //Debug.Log("Inside Switching");
            // stoping previous sound
            Debug.Log("Stopping sound...");
            audioSource.rs3d_StopSound(currentSoundIndex);
            
            //starting up next sound
            audioSource.rs3d_PlaySound(nextSoundIndex);
            //setting new current sound
            currentSoundIndex = nextSoundIndex;
            yield return new WaitForSeconds(stepInterval);
        }
    }

    // returns the density value in the range of the pitch capacity, -3 to 3.
    /**
     * @param: density- the input density value
     * @param: minRange- the minimum density value to satisfy being in the note range
     * @param: maxRange- the max density value that the point can be to be in the note range
     * @param: minScale- the minimum for what the scale will be.
     * @param: maxScale- the maximum for what the scale will be.
     * @return: the value of the density scaled to the minScale and maxScale
     **/
    private float Scale(float density, float minRange, float maxRange, float minScale, float maxScale)
    {
        return (((maxScale - minScale) * (density - minRange)) / (maxRange - minRange)) + minScale;
    }
#endif
}
