using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimbreSelect
{
    brass, voice, clarinet
}

public class ChuckSoundGenerator : MonoBehaviour {

    public TimbreSelect timbreSelect;

    private Dictionary<Vector3, float> densData;

    private float freq;

    private GameObject headFollow;

    private GameObject CasA;

    // for checking MS version
    private MovementSystem MSL;

    // for MS1
    private GameObject CasALeft;
    private GameObject CasARight;
    // for MS1

    private float freqRangeMin, freqRangeMax;

    // Use this for initialization
    void Start () {

        // getting the MS logger to see which version the MS is
        MSL = GameObject.Find("MSLogger").GetComponent<MSLogger>().MS;


        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        densData = playArea.GetComponent<DataReader>().densData;

        // getting the headfollow
        headFollow = GameObject.FindGameObjectWithTag("HeadFollow");

        // getting the CasA
        if (MSL == MovementSystem.MS1)
        {
            CasALeft = GameObject.FindGameObjectWithTag("SuperNovaLeft");
            CasARight = GameObject.FindGameObjectWithTag("SuperNovaRight");
        }
        else
        {
            CasA = GameObject.FindGameObjectWithTag("SuperNova");
        }


        if (timbreSelect == TimbreSelect.brass)
        {
            freqRangeMin = 261.63f;
            freqRangeMax = 1046.50f;

            GetComponent<ChuckSubInstance>().RunCode(@"
            fun void playImpact( float freq )
            {
                // connecting to dac with osc type
                SinOsc foo => Envelope e => dac;

                // lowering the gain
                .5 => foo.gain;
                
                // setting the envelope time
                500::ms => dur t => e.duration;

                // running the envelope with the frequency
                freq => foo.freq;
                e.keyOn();
                1500::ms => now;
                e.keyOff();
                500::ms => now;
            }
   
            global float impactFreq;
            global Event impactHappened;

            while( true )
                {
                    impactHappened => now;
                    spork ~ playImpact( impactFreq );
                }
        ");

        } else if(timbreSelect == TimbreSelect.voice)
        {
            freqRangeMin = 329.63f;
            freqRangeMax = 1318.51f;

            GetComponent<ChuckSubInstance>().RunCode(@"
            fun void playImpact( float freq )
            {
                // connecting to dac with osc type
                Saxofony foo => JCRev r => Envelope e => dac;

                // lowering the gain
                .5 => r.gain;
                .2 => r.mix;


                // setting params
                .8 => foo.stiffness;
                .4 => foo.aperture;
                .5 => foo.noiseGain;
                .7 => foo.blowPosition;
                4 => foo.vibratoFreq;
                .8 => foo.vibratoGain;
                .5 => foo.pressure;


                // setting the envelope time
                500::ms => dur t => e.duration;

                // running the envelope with the frequency
                freq => foo.freq;
                500::ms => now;
                1 => foo.noteOn;
                e.keyOn();
                1000::ms => now;
                e.keyOff();
                1 => foo.noteOff;
                500::ms => now;
            }
   
            global float impactFreq;
            global Event impactHappened;

            while( true )
                {
                    impactHappened => now;
                    spork ~ playImpact( impactFreq );
                }
        ");

        } else
        {
            freqRangeMin = 392.00f;
            freqRangeMax = 783.99f;

            GetComponent<ChuckSubInstance>().RunCode(@"
            fun void playImpact( float freq )
            {
                // connecting to dac with osc type
                PulseOsc foo => Envelope e => dac;

                // lowering the gain
                .5 => foo.gain;
                
                // setting the envelope time
                500::ms => dur t => e.duration;

                // running the envelope with the frequency
                freq => foo.freq;
                1000::ms => now;
                e.keyOn();
                500::ms => now;
                e.keyOff();
                500::ms => now;
            }
   
            global float impactFreq;
            global Event impactHappened;

            while( true )
                {
                    impactHappened => now;
                    spork ~ playImpact( impactFreq );
                }      
        ");

        }

        StartCoroutine("StartBang");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private float GetFreq()
    {
        float returnFloat;
        Vector3 curPos;


        // for finding which hand is active in MS1, or if it is MS1 at all.
        if (MSL == MovementSystem.MS1)
        {
            if (CasALeft.activeSelf)
            {
                curPos = headFollow.transform.InverseTransformPoint(CasALeft.transform.position) / CasALeft.transform.localScale.x;
            }
            else
            {
                curPos = headFollow.transform.InverseTransformPoint(CasARight.transform.position) / CasARight.transform.localScale.x;
            }
        }
        else
        {
            curPos = headFollow.transform.InverseTransformPoint(CasA.transform.position);
        }


        Vector3 roundedPos = new Vector3(Mathf.RoundToInt(curPos.x),
            Mathf.RoundToInt(curPos.y),
            Mathf.RoundToInt(curPos.z));

        if (densData.ContainsKey(roundedPos))
        {
            returnFloat = Scale(densData[roundedPos], -20000f, 20000f, freqRangeMin, freqRangeMax);
        } else
        {
            returnFloat = freqRangeMin;
        }

        return returnFloat;
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




    private IEnumerator StartBang()
    {
        while (true)
        {
            freq = GetFreq();
            GetComponent<ChuckSubInstance>().SetFloat("impactFreq", freq);
            GetComponent<ChuckSubInstance>().BroadcastEvent("impactHappened");
            yield return new WaitForSeconds(3f);
            Debug.Log("Making Bang!!!");
            yield return null;
        }
    }
}
