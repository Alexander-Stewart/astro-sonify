//#define EightSevenA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataReader : MonoBehaviour {

    
    public Data data;

#if EightSevenA
    string path = "Assets/Resources/87a_values.txt";
#else
    public string path = "Assets/Resources/";
#endif


    // Use this for initialization
    void Start () {
        StreamReader reader = new StreamReader(path);
        string line;
        string[] values;
        Vector3 curPos;
        float dens;
        line = reader.ReadLine();
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
            values = line.Split(',');

            // getting the curPos as a vector and dens value
            curPos = new Vector3(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
            dens = float.Parse(values[3]);
            //Vector3 gradient = new Vector3(partialX(curPos.x, curPos.y, curPos.z),
            //                                partialY(curPos.x, curPos.y, curPos.z),
            //                                partialZ(curPos.x, curPos.y, curPos.z));
            // Debug.Log("Density value for " + curPos + ": " + dens);


            // adding to hashtable
            //data.densData.Add(curPos, dens);
            data.AddPoint(curPos, dens);
            //data.AddGradient(curPos, gradient);

#endif
        }
        reader.Close();

        //AssetDatabase.CreateAsset(data, "Assets/Resources/Data/ArgonData.asset");
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


        //getting gradients
        //gradientGenerator();

        // debugs
       // Debug.Log("This is the densData: " + densData);
        //Debug.Log("the origin for gradient data: " + gradientData[new Vector3(1f, -45f, 2f)]);
    }

   
}
