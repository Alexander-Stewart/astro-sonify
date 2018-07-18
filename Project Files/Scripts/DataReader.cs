using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataReader : MonoBehaviour {

    [HideInInspector]
    public Dictionary<Vector3, float> densData = new Dictionary<Vector3, float>();
    public Dictionary<Vector3, Vector3> gradientData = new Dictionary<Vector3, Vector3>();

    string path = "Assets/Resources/DataFile.txt";

    // Use this for initialization
    void Start () {
        StreamReader reader = new StreamReader(path);
        string line;
        string[] values;
        Vector3 curPos;
        float dens;
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            values = line.Split(' ');

            // getting the curPos as a vector and dens value
            curPos = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            dens = float.Parse(values[3]);
           // Debug.Log("Density value for " + curPos + ": " + dens);

           
            // adding to hashtable
            densData.Add(curPos, dens);
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
        float x = 0;
        float y = 0;
        float z = 0;

        foreach (KeyValuePair<Vector3, float> curPos in densData)
        {
            //Vector3 gradient = new Vector3(partialX(curPos.Key.x, curPos.Key.y, curPos.Key.z), 
            //                                partialY(curPos.Key.x, curPos.Key.y, curPos.Key.z), 
            //                                partialZ(curPos.Key.x, curPos.Key.y, curPos.Key.z));
            x = Mathf.Clamp(x + Random.Range(-5f, 6f), -25, 25);
            y = Mathf.Clamp(y + Random.Range(-5f, 6f), -25, 25);
            z = Mathf.Clamp(z + Random.Range(-5f, 6f), -25, 25);
            gradient = new Vector3(x,y,z);

            gradientData.Add(curPos.Key, gradient);

            //Debug.Log("Gradient for " + curPos.Key + ": " + gradient);
        }
    }

    private float partialX(float xCor, float yCor, float zCor)
    {
        float returnFloat;

        // delta x is 1!
        if(xCor == -50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor + 1, yCor, zCor);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        } else if(xCor == 50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor - 1, yCor, zCor);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        } else
        {
            Vector3 backwardCor = new Vector3(xCor - 1, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor + 1, yCor, zCor);
            returnFloat = 1 / 2 * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }

    private float partialY(float xCor, float yCor, float zCor)
    {
        float returnFloat;

        // delta y is 1!
        if (yCor == -50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor + 1f, zCor);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        }
        else if (yCor == 50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor, yCor - 1f, zCor);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        }
        else
        {
            Vector3 backwardCor = new Vector3(xCor, yCor - 1f, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor + 1f, zCor);
            returnFloat = 1 / 2 * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }

    private float partialZ(float xCor, float yCor, float zCor)
    {
        float returnFloat;

        // delta z is 1!
        if (zCor == -50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor, zCor + 1f);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        }
        else if (zCor == 50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor, yCor, zCor - 1f);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        }
        else
        {
            Vector3 backwardCor = new Vector3(xCor, yCor, zCor - 1f);
            Vector3 forwardCor = new Vector3(xCor, yCor, zCor + 1f);
            returnFloat = 1 / 2 * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }
}
