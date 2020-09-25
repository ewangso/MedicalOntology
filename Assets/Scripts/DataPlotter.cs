using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlotter : MonoBehaviour
{
    public string inputfile;
    public GameObject PointPrefab; //prefab for the datapoints to be instantiated
    private List<Dictionary<string, object>> pointList;

    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    public string xName;
    public string yName;
    public string zName;

    public float plotscale = 10;
    // Start is called before the first frame update
    void Start()
    {
        pointList = CSVReader.Read(inputfile);
        Debug.Log(pointList);
        List<string> columnList = new List<string>(pointList[1].Keys);
        Debug.Log("There are " + columnList.Count + " columns in CSV");
        foreach (string key in columnList)
        {
            Debug.Log("Column name is" + key);
        }
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];

        // Get maxes of each axis
        float xMax = FindMaxValue(xName);
        float yMax = FindMaxValue(yName);
        float zMax = FindMaxValue(zName);

        // Get minimums of each axis
        float xMin = FindMinValue(xName);
        float yMin = FindMinValue(yName);
        float zMin = FindMinValue(zName);
        for (var i = 1; i < pointList.Count; i++)
        {
            // Get value in poinList at ith "row", in "column" Name, normalize
            float x =
            (System.Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);

            float y =
            (System.Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);

            float z =
            (System.Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

            //Instantiate Prefab at coordinate defined above
            GameObject dataPoint = Instantiate(PointPrefab, new Vector3(x, y, z)*plotscale, Quaternion.identity);

            // Assigns original values to dataPointName
            string dataPointName =
            pointList[i][xName] + " "
            + pointList[i][yName] + " "
            + pointList[i][zName];

            dataPoint.transform.name = dataPointName;
            dataPoint.GetComponent<Renderer>().material.color = new Color(x, y, z, 1.0f);
        }



    }
    private float FindMaxValue(string columnName)
    {
        float maxValue = System.Convert.ToSingle(pointList[0][columnName]);

        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < System.Convert.ToSingle(pointList[i][columnName]))
                maxValue = System.Convert.ToSingle(pointList[i][columnName]);
        }
        return maxValue;
    }

    private float FindMinValue(string columnName)
    {
        float minValue = System.Convert.ToSingle(pointList[0][columnName]);
        for (var i = 0; i < pointList.Count; i++)
        {
            if (minValue > System.Convert.ToSingle(pointList[i][columnName]))
                minValue = System.Convert.ToSingle(pointList[i][columnName]);
        }
        return minValue;
    }
}
