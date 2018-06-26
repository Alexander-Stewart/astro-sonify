﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataGenerator : MonoBehaviour {

    string path = "Assets/Resources/DataFile.txt";

    float randomDensityValue = 0f;

    // setting up the spherical harmonics formula semi-randomly
   


    // Use this for initialization
    void Start () {
        int l = UnityEngine.Random.Range(0, 3);
        Debug.Log("This is l: " + l);
        GenerateFile(l);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private float HarmonicGenerator(int l, int m, float xCor, float yCor, float zCor)
    {
        float returnHarmonic;
        float updateRandomDV;

        //creating random value for the density value at that point
        updateRandomDV = UnityEngine.Random.Range(randomDensityValue - 10f, randomDensityValue + 11f);
        if(updateRandomDV > 50f)
        {
            updateRandomDV = 50f;
        } else if(updateRandomDV < -50f)
        {
            updateRandomDV = -50f;
        }

        randomDensityValue = updateRandomDV;

        switch (l)
        {
            case 0:
                returnHarmonic = Mathf.Sqrt(1f / (4f * Mathf.PI));
                return returnHarmonic * randomDensityValue;
            case 1:
                if(m == -1)
                {
                    returnHarmonic = Mathf.Sqrt(3f / (4f * Mathf.PI)) * xCor;
                    return returnHarmonic * randomDensityValue;
                } else if (m == 0)
                {
                    returnHarmonic = Mathf.Sqrt(3f / (4f * Mathf.PI)) * zCor;
                    return returnHarmonic * randomDensityValue;
                } else
                {
                    returnHarmonic = Mathf.Sqrt(3f / (4f * Mathf.PI)) * yCor;
                    return returnHarmonic * randomDensityValue;
                }
            case 3:
                if (m == -2)
                {
                    returnHarmonic = Mathf.Sqrt(15f / (4f * Mathf.PI)) * xCor * yCor;
                    return returnHarmonic * randomDensityValue;
                } else if(m == -1)
                {
                    returnHarmonic = Mathf.Sqrt(15f / (4f * Mathf.PI)) * yCor * zCor;
                    return returnHarmonic * randomDensityValue;
                } else if(m == 0)
                {
                    returnHarmonic = Mathf.Sqrt(5f / (16f * Mathf.PI)) * (3f * Mathf.Pow(zCor, 2) - 1f);
                    return returnHarmonic * randomDensityValue;
                } else if(m == 1)
                {
                    returnHarmonic = Mathf.Sqrt(15f / (8f * Mathf.PI)) * xCor * zCor;
                    return returnHarmonic * randomDensityValue;
                } else
                {
                    returnHarmonic = Mathf.Sqrt(15f / (32f * Mathf.PI)) * (Mathf.Pow(xCor,2) - Mathf.Pow(yCor,2));
                    return returnHarmonic * randomDensityValue;
                }
            default:
                returnHarmonic = Mathf.Sqrt(15f / (32f * Mathf.PI)) * (Mathf.Pow(xCor, 2) - Mathf.Pow(yCor, 2));
                return returnHarmonic * randomDensityValue;
        }
    }

    private void GenerateFile(int l)
    {
        // creating stream Writer
        StreamWriter writer = new StreamWriter(path);

        int m = UnityEngine.Random.Range(-l, (l + 1));
        Debug.Log("This is m: " + m);
        for(float x = -2; x <= 2; x = x + .1f)
        {
            for(float y = -2; y <= 2; y = y + .1f)
            {
                for(float z = -2; z <= 2; z = z + .1f)
                {
                    float harmonicVal = HarmonicGenerator(l, m, x, y, z);
                    writer.WriteLine((float)Math.Round(x,1) + " " + 
                        (float)Math.Round(y, 1) + " " + (float)Math.Round(z, 1) + " " + harmonicVal);
                }
            }
        }
        writer.Flush();
        writer.Close();
    }
}
