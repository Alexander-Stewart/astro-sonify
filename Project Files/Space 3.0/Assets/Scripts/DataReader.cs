#define EightSevenA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataReader : MonoBehaviour {

    [HideInInspector]
    public Dictionary<Vector3, float> densData = new Dictionary<Vector3, float>();
    public Dictionary<Vector3, Vector3> gradientData = new Dictionary<Vector3, Vector3>();


#if EightSevenA
    string path = "Assets/Resources/87a_values.txt";
#else
    string path = "Assets/Resources/DataFile.txt";
#endif


    // Use this for initialization
    void Start () {
        StreamReader reader = new StreamReader(path);
        string line;
        string[] values;
        Vector3 curPos;
        float dens;
        while (!reader.EndOfStream)
        {

            // deciding how to read data depending on which SN it is.
#if EightSevenA
            for(int i = -256; i < 256; i++)
            {
                for(int j = -256; j < 256; j++)
                {
                    for(int k = -256; k < 256; k++)
                    {
                        line = reader.ReadLine();

                        // getting the curPos as a vector and dens value
                        curPos = new Vector3(i,j,k);
                        dens = float.Parse(line);
                        // Debug.Log("Density value for " + curPos + ": " + dens);


                        // adding to hashtable
                        densData.Add(curPos, dens);
                    }
                }
            }
                     
#else
            line = reader.ReadLine();
            values = line.Split(' ');

            // getting the curPos as a vector and dens value
            curPos = new Vector3(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
            dens = float.Parse(values[3]);
           // Debug.Log("Density value for " + curPos + ": " + dens);

           
            // adding to hashtable
            densData.Add(curPos, dens);
#endif
        }
        reader.Close();

        //getting gradients
        gradientGenerator();

        // debugs
       // Debug.Log("This is the densData: " + densData);
        Debug.Log("the origin for gradient data: " + gradientData[new Vector3(1f, -45f, 2f)]);
    }

    private void gradientGenerator()
    {
        Vector3 gradient = Vector3.zero;
        //float x = 0;
        //float y = 0;
        //float z = 0;

        foreach (KeyValuePair<Vector3, float> curPos in densData)
        {
            gradient = new Vector3(partialX(curPos.Key.x, curPos.Key.y, curPos.Key.z), 
                                            partialY(curPos.Key.x, curPos.Key.y, curPos.Key.z), 
                                            partialZ(curPos.Key.x, curPos.Key.y, curPos.Key.z));
            //x = Mathf.Clamp(x + Random.Range(-1f, 1f), -25, 25);
            //y = Mathf.Clamp(y + Random.Range(-1f, 1f), -25, 25);
            //z = Mathf.Clamp(z + Random.Range(-1f, 1f), -25, 25);
            //gradient = new Vector3(x,y,z);
            //Debug.Log("Pos: " + curPos.Key + ", The Gradient: " + gradient);
            gradientData.Add(curPos.Key, gradient);

            //Debug.Log("Gradient for " + curPos.Key + ": " + gradient);
        }
    }

    private float partialX(float xCor, float yCor, float zCor)
    {

        float returnFloat;
        float range;

        // setting range 
#if EightSevenA
        range = 256f;
#else
        range = 50f;
#endif

        // delta x is 1!
        if(xCor == -range)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor + 1f, yCor, zCor);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        } else if(xCor == range)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor - 1f, yCor, zCor);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        } else
        {
            Vector3 backwardCor = new Vector3(xCor - 1f, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor + 1f, yCor, zCor);
            //Debug.Log("backward: " + densData[backwardCor]);
            //Debug.Log("forward: " + densData[forwardCor]);
            returnFloat = 1f / 2f * (densData[forwardCor] - densData[backwardCor]);
            //Debug.Log("the x component: " + returnFloat);
        }

        return returnFloat;
    }

    private float partialY(float xCor, float yCor, float zCor)
    {

        float returnFloat;
        float range;

        // setting range 
#if EightSevenA
        range = 256f;
#else
        range = 50f;
#endif

        // delta y is 1!
        if (yCor == -range)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor + 1f, zCor);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        }
        else if (yCor == range)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor, yCor - 1f, zCor);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        }
        else
        {
            Vector3 backwardCor = new Vector3(xCor, yCor - 1f, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor + 1f, zCor);
            returnFloat = 1f / 2f * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }

    private float partialZ(float xCor, float yCor, float zCor)
    {

        float returnFloat;
        float range;

        // setting range 
#if EightSevenA
        range = 256f;
#else
        range = 50f;
#endif

        // delta z is 1!
        if (zCor == -range)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor, zCor + 1f);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        }
        else if (zCor == range)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor, yCor, zCor - 1f);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        }
        else
        {
            Vector3 backwardCor = new Vector3(xCor, yCor, zCor - 1f);
            Vector3 forwardCor = new Vector3(xCor, yCor, zCor + 1f);
            returnFloat = 1f / 2f * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }
}
