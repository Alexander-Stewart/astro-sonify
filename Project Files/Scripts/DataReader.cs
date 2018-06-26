using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataReader : MonoBehaviour {

    [HideInInspector]
    public Dictionary<Vector3, float> densData = new Dictionary<Vector3, float>();

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
        Debug.Log("This is the densData: " + densData);
    }
}
