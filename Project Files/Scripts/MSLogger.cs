using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * MSLogger tells which movement system is
 * being used with a message to the console.
 **/
public class MSLogger : MonoBehaviour
{
    public MovementSystem MS;


    // Use this for initialization
    void Awake()
    {
        if (MS == MovementSystem.MS1)
        {
            Debug.Log("MS1");
        }

        else if (MS == MovementSystem.MS2)
        {
            Debug.Log("MS2");
        }

        else if (MS == MovementSystem.MS3)
        {
            Debug.Log("MS3");
        }

        else if (MS == MovementSystem.MS4)
        {
            Debug.Log("MS4");
        }

        else if (MS == MovementSystem.MS5)
        {
            Debug.Log("MS5");
        }
    }


}
