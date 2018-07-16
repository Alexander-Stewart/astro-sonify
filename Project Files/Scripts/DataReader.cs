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
        Vector3 gradient;
        float dens;
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            values = line.Split(' ');

            // getting the curPos as a vector and dens value
            curPos = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            dens = float.Parse(values[3]);

            //generating gradient: 
            gradient = gradientGenerator(curPos.x, curPos.y, curPos.z, dens);

            // adding to hashtable
            densData.Add(curPos, dens);
            gradientData.Add(curPos, gradient);
        }
        reader.Close();
        Debug.Log("This is the densData: " + densData);
    }

    private Vector3 gradientGenerator(float xCor, float yCor, float zCor, float density)
    {
        Vector3 returnVector = new Vector3(partialX(xCor,yCor,zCor), partialY(xCor,yCor,zCor), partialZ(xCor,yCor,zCor)) * density;
        return returnVector;
    }

    private float partialX(float xCor, float yCor, float zCor)
    {
        return Mathf.Sqrt(15f / (4f * Mathf.PI)) * yCor;
    }

    private float partialY(float xCor, float yCor, float zCor)
    {
        return Mathf.Sqrt(15f / (4f * Mathf.PI)) * xCor;
    }

    // may want to add value just for movement sake
    private float partialZ(float xCor, float yCor, float zCor)
    {
        return 0f;
    }
}
