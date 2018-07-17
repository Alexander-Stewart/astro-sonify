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

           
            // adding to hashtable
            densData.Add(curPos, dens);
        }
        reader.Close();

        //getting gradients
        gradientGenerator();

        // debugs
        Debug.Log("This is the densData: " + densData);
    }

    private void gradientGenerator()
    {
        foreach (Vector3 curPos in densData.Keys)
        {
            Vector3 gradient = new Vector3(partialX(curPos.x, curPos.y, curPos.z), 
                                            partialY(curPos.x, curPos.y, curPos.z), 
                                            partialZ(curPos.x, curPos.y, curPos.z));
            gradientData.Add(curPos, gradient);
        }
    }

    private float partialX(float xCor, float yCor, float zCor)
    {
        float returnFloat;

        // delta x is 1!
        if(xCor == 0)
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
        if (yCor == 0)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor + 1, zCor);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        }
        else if (yCor == 50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor, yCor - 1, zCor);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        }
        else
        {
            Vector3 backwardCor = new Vector3(xCor, yCor - 1, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor + 1, zCor);
            returnFloat = 1 / 2 * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }

    private float partialZ(float xCor, float yCor, float zCor)
    {
        float returnFloat;

        // delta z is 1!
        if (yCor == 0)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 forwardCor = new Vector3(xCor, yCor, zCor + 1);
            returnFloat = (densData[forwardCor] - densData[Cor]);
        }
        else if (yCor == 50)
        {
            Vector3 Cor = new Vector3(xCor, yCor, zCor);
            Vector3 backwardCor = new Vector3(xCor, yCor, zCor - 1);
            returnFloat = (densData[Cor] - densData[backwardCor]);
        }
        else
        {
            Vector3 backwardCor = new Vector3(xCor, yCor, zCor - 1);
            Vector3 forwardCor = new Vector3(xCor, yCor, zCor + 1);
            returnFloat = 1 / 2 * (densData[forwardCor] - densData[backwardCor]);
        }

        return returnFloat;
    }
}
