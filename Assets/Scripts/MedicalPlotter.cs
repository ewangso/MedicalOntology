using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// This is testing this.
/// </summary>
public class MedicalPlotter : MonoBehaviour
{
    public Circle circle;
    public string infile;
    public float testRadius;
    public GameObject cubePrefab;
    public GameObject cubeHolder;

    private List<Dictionary<string, object>> dataList;


    public LineRenderer line;

    public string nodeColumn;
    public string parentColumn;
    public string labelColumn;
    public string descriptionColumn;

    public int baselayer = 5;

    public List<GameObject> nodes;
    public List<GameObject> leaves;
    public List<GameObject> visited;
    public List<GameObject> parents;

    public LinkedList<GameObject> nodeObjects;
    
    public SortedDictionary<int, List<GameObject>> nodeMap;

    public bool inCircle;

    int x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        int nodeId = 0;
        int labelnum = 1;
        int descriptionnum = 3;
        int parentId = 7;
        inCircle = false;
        //line.SetWidth(0, 3);
        SortedDictionary<int, List<GameObject>> nodeMap = new SortedDictionary<int, List<GameObject>>();
        //Init of dataList

        dataList = CSVReader.Read(infile);
        //Debug.Log(dataList[1]);

        List<string> columnNames = new List<string>(dataList[1].Keys);

        //nodeMap = map;
        //Gets the columns with node names and parent names
        nodeColumn = columnNames[nodeId];
        parentColumn = columnNames[parentId];
        labelColumn = columnNames[labelnum];
        descriptionColumn = columnNames[descriptionnum];

        System.Random rand = new System.Random();

        // Creates Data object and prints names of objects on front surface
        for (var i = 0; i < dataList.Count; i++)
        {
            //Creates random position for node
            x = rand.Next(0, 100);
            y = rand.Next(0, 100);
            z = rand.Next(0, 100);
            string dataPointName = dataList[i][nodeColumn].ToString();


            //Debug.Log(dataList[i]["Preferred Label"].ToString());

            if (!dataPointName.StartsWith("http://purl.obolibrary.org/obo/"))
            {
                //Debug.Log("DP Name: " + dataPointName);
                continue;
            }
            else if (dataPointName.Contains(","))
            {
                dataPointName = dataPointName.Split(',')[0];
            }

            //Debug.Log("DP Name: " + dataPointName);
            //Creates node as a cube
            GameObject dataPoint = Instantiate(cubePrefab, new Vector3(x, y, z), Quaternion.identity);
            //dataPoint.transform.parent = cubeHolder.transform;

            dataPoint.name = dataPointName.Trim();

            dataPoint.GetComponent<DataContainer>().id = dataPoint.name;
            //Debug.Log(dataPointName);

            //Checks key of dataList Dictionary object
            if (dataList[i].ContainsKey(parentColumn))
            {
                //Initialize all attributes in dataPoint object
                string parentName = dataList[i][parentColumn].ToString();
                dataPoint.GetComponent<DataContainer>().parent = parentName;
                dataPoint.GetComponent<DataContainer>().description = dataList[i][descriptionColumn].ToString();
                dataPoint.GetComponent<DataContainer>().label = dataList[i][labelColumn].ToString();
            }

            TextMeshPro mesh = dataPoint.GetComponentInChildren<TextMeshPro>();

            mesh.text = dataPoint.GetComponent<DataContainer>().label;


            nodes.Add(dataPoint); //Contains nodes
        }

        Debug.Log("Number of nodes in the scene " + nodes.Count);

        //Loop through nodes list
        for (var j = 0; j < nodes.Count; j++)
        {
            GameObject foundparent = findNode(nodes[j].GetComponent<DataContainer>().parent);
            //For nodes without parents
            if (foundparent != null)
            {
                DataContainer parentdata = foundparent.GetComponent<DataContainer>();
                DataContainer childdata = nodes[j].GetComponent<DataContainer>();
                //parentdata.children.Add(nodes[j].name);
                foundparent.GetComponent<DataContainer>().children.Add(nodes[j].name);
                line = foundparent.GetComponent<LineRenderer>();

                var num = rand.Next(0, 5);
                Color[] col = { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta };


                //line.SetPosition(0, foundparent.transform.position);
                //Debug.Log(foundparent.transform.position);
            }
            else
            {
                //orphans.Add(nodes[j]);
            }
        }

        /* For positioning the leaf nodes */
        for (var x = 0; x < nodes.Count; x++)
        {
            if (nodes[x].GetComponent<DataContainer>().children.Count == 0)
            {
                leaves.Add(nodes[x]);
            }
        }

        for(var y = 0; y < leaves.Count; y++)
        {
            leaves[y].GetComponent<DataContainer>().layernum = 0;
            setLayers(leaves[y], 1);
        }

        /*
        for (var y = 0; y < leaves.Count; y++)
        {
            leaves[y].GetComponent<DataContainer>().layernum = 0;
            positionNodes(leaves[y].GetComponent<DataContainer>().parent, 1, baselayer, y);
        }
        */

        for (var z = 0; z < nodes.Count; z++)
        {
            //Debug.Log(nodes[z].GetComponent<DataContainer>().layernum);
            if (nodeMap.ContainsKey(nodes[z].GetComponent<DataContainer>().layernum))
            {
                nodeMap[nodes[z].GetComponent<DataContainer>().layernum].Add(nodes[z]);
            }
            else
            {
                nodeMap[nodes[z].GetComponent<DataContainer>().layernum] = new List<GameObject>();
                nodeMap[nodes[z].GetComponent<DataContainer>().layernum].Add(nodes[z]);
            }
        }

        Debug.Log("Number of layers: " + nodeMap.Count);

        int offset = 0;

        for (var s = 0; s < nodes.Count; s++)
        {
            TextMeshPro mesh = nodes[s].GetComponentInChildren<TextMeshPro>();
            mesh.color = Color.white;

            
            //mesh.text = nodes[s].GetComponent<DataContainer>().label;
            
        }

        for (int s = 0; s  < nodes.Count; s++)
        {
            if (nodes[s].GetComponent<DataContainer>().children.Count > 0 || nodes[s].GetComponent<DataContainer>().parent == null)
            {
                nodes[s].GetComponent<LineRenderer>().enabled = false;
                List<string> childList = nodes[s].GetComponent<DataContainer>().children;

                for (int i = 0; i < childList.Count; i++)
                {
                    findNode(childList[i]).GetComponent<Renderer>().enabled = false;
                    findNode(childList[i]).GetComponentInChildren<TextMeshPro>().enabled = false;
                }
            }
        }

        for (var s = 0; s < nodes.Count - 1; s++)
        {
            GameObject node = nodes[s];
            node.GetComponent<DataContainer>().numDescendants = getDescendants(node);
        }
        List<GameObject> objects = new List<GameObject>();

        //=====================Position nodes by layer==========================
        for (int c = 7; c >= 0; c--)
        {
            List<GameObject> layerNodes = nodeMap[c];
            List<GameObject> orderedNodes = new List<GameObject>();
            int layerSize = layerNodes.Count;

            //Debug.Log("Layer: " + c + " Layer Size: " + layerNodes.Count);

            int numLayer = c;
            int counter = 0;
            int siblingOff = 0;
            float zPos = 0;

            List<float> lastChild = new List<float>();

            SortedDictionary<string, List<GameObject>> parentMap = new SortedDictionary<string, List<GameObject>>();
            SortedDictionary<float, List<GameObject>> order = new SortedDictionary<float, List<GameObject>>();

            //=========Making the parent-sibling map=======================
            foreach (GameObject node in layerNodes)
            {
                string parent = node.GetComponent<DataContainer>().parent;
                if (findNode(parent) == null)
                {
                    parent = "null";
                }

                //Checks if Parent Map has the key for a parent 
                if (parentMap.ContainsKey(parent))
                {
                    //Adds node to existing dictionary key value
                    parentMap[parent].Add(node);
                }
                else
                {
                    //Creates a key for the parent and sets a list object to its value
                    parentMap[parent] = new List<GameObject>();
                    parentMap[parent].Add(node);
                }

            }
            //Debug.Log(parentMap.Count);
            //List<GameObject> lastList = parentMap[parentMap.Count - 1];

            List<GameObject> temp = new List<GameObject>();
            foreach (KeyValuePair<string, List<GameObject>> parentChildrenPair in parentMap)
            {
                temp = parentChildrenPair.Value;

                foreach (GameObject node in parentChildrenPair.Value)
                {
                    orderedNodes.Add(node);
                }
            }
            //Debug.Log(" LAST: " + temp[temp.Count - 1]);

            //================Places the node based on sibling order==================
            counter = 0;
            foreach (KeyValuePair<string, List<GameObject>> parentChildrenPair in parentMap)
            {
                int off = 10;
                

                if (parentChildrenPair.Key != "null")
                {
                    foreach (GameObject childNode in parentChildrenPair.Value)
                    {
                        GameObject parent = findNode(parentChildrenPair.Key);
                        string parentString = parentChildrenPair.Key;
                        int childCount = childNode.GetComponent<DataContainer>().children.Count;

                        childNode.transform.position = new Vector3(parent.transform.position.x + counter, numLayer * 5, 0);
                        childNode.GetComponent<DataContainer>().siblings = parentChildrenPair.Value;
                        counter += ((getDescendants(childNode) / 2) + off);
                        off = 0;
                        
                        lastChild.Add(parent.transform.position.x);
                    }
                }
            }

            counter = 0;
            foreach (KeyValuePair<string, List<GameObject>> parentChildrenPair in parentMap)
            {
                int off = 10;

                if (parentChildrenPair.Key == "null")
                {
                    List<GameObject> parentless = parentChildrenPair.Value;
                    for (int i = 0; i < parentless.Count; i++)
                    {
                        counter += ((getDescendants(parentless[i]) / 2) + off + 5);
                        if (c <= 4)
                        {
                            parentless[i].transform.position = new Vector3(
                                lastChild[lastChild.Count - 1] + counter,
                                numLayer * 20,
                                0
                            );
                        }
                        off = 0;

                    }
                }
            }


            //==============Adjusting the z position of the nodes==============

            /*Setting up the dictionary for layer order*/

            for (int i = 0; i < layerSize; i++)
            {
                if (order.ContainsKey(layerNodes[i].transform.position.x))
                {
                    order[layerNodes[i].transform.position.x].Add(layerNodes[i]);
                }
                else
                {
                    order[layerNodes[i].transform.position.x] = new List<GameObject>();
                    order[layerNodes[i].transform.position.x].Add(layerNodes[i]);
                }
            }

            int dir = 0;
            int count = 0;
            int offVar = 0;
            //StartCoroutine(SortZ(layerNodes, 0));

            
            if (c == 0)
            {
                //StartCoroutine(Circulate(objects, order));
                foreach (KeyValuePair<float, List<GameObject>> pair in order)
                {

                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        objects.Add(pair.Value[i]);

                    }
                    
                }

                StartCoroutine(CirculateNodes(objects));
            }


            
        } //End layer for loop
        Debug.Log("The number of leaves is " + leaves.Count);
        Debug.Log("Number of nodes in the scene " + nodes.Count);
        
    } //End Start

    private void FixedUpdate()
    {
        if (inCircle)
        {
            DrawLines();
        }

    }

    IEnumerator CirculateNodes(List<GameObject> objects)
    {
        yield return new WaitForSeconds(80);
        float angleStep = 360.0f / (objects.Count + 20);
        for (int i = 0; i < objects.Count; i++)
        {
            float angle = angleStep * i;
            Vector3 pos = RandomCircle(new Vector3(0, 0, 0), 100f, angle);
            objects[i].transform.position = pos;
        }
        inCircle = true;
    }

    Vector3 RandomCircle(Vector3 center, float radius, float ang)
    {
        System.Random rnd = new System.Random();
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }

    public GameObject findNode(string nodeName)
    {
        GameObject obj = GameObject.Find(nodeName);
        if (obj != null)
            return obj;
        else
            return null;
    }

    //function for ordering nodes in a scene
    //positionNodes(leaves[y].GetComponent<DataContainer>().parent, 1, baselayer, y);

    public void setLayers(GameObject leaf, int layer)
    {
        GameObject parent = findNode(leaf.GetComponent<DataContainer>().parent);
        if (parent == null)
        {
            return;
        }
        else
        {
            parent.GetComponent<DataContainer>().layernum = layer;
            setLayers(parent,layer + 1);
        }
    }

    public int getDescendants(GameObject node)
    {
        int numDescendants = 0;
        List<string> childList = node.GetComponent<DataContainer>().children;
        numDescendants += childList.Count;

        for (int i = 0; i < childList.Count; i++)
        {
            numDescendants += getDescendants(findNode(childList[i]));
        }

        return numDescendants;
    }

    public void DrawLines()
    {
        //yield return new WaitForSeconds(0);
        for (var j = 0; j < nodes.Count; j++)
        {
            Color[] col = { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta };
            //For nodes that have children
            GameObject parentNode = nodes[j];
            if (parentNode.GetComponent<DataContainer>().children.Count > 0)
            {
                List<string> childList = parentNode.GetComponent<DataContainer>().children;
                List<GameObject> siblingList = parentNode.GetComponent<DataContainer>().siblings;
                int poscount = 0;
                line = parentNode.GetComponent<LineRenderer>();
                int layer = parentNode.GetComponent<DataContainer>().layernum;
                switch (layer)
                {
                    case 7:
                        line.material.color = col[4];
                        break;
                    case 6:
                        line.material.color = col[3];
                        break;
                    case 5:
                        line.material.color = col[2];
                        break;
                    case 4:
                        line.material.color = col[1];
                        break;
                    case 3:
                        line.material.color = col[0];
                        break;
                    case 2:
                        line.material.color = col[4];
                        break;
                    case 1:
                        line.material.color = col[3];
                        break;
                    case 0:
                        line.material.color = col[2];
                        break;
                }
                parentNode.GetComponent<DataContainer>().linknum = 0;
                line.positionCount = childList.Count * 2;
                int off = 10;
                for (var k = 0; k < childList.Count; k++)
                {
                    GameObject child = findNode(childList[k]);
                    bool test = Math.Abs(child.GetComponent<DataContainer>().layernum - parentNode.GetComponent<DataContainer>().layernum) == 1;

                    if (child != null)
                    {
                        line.SetPosition(poscount, child.transform.position);
                        poscount++;
                        line.SetPosition(poscount, parentNode.transform.position);
                        child.GetComponent<DataContainer>().linknum = poscount;
                        poscount++;
                    }
                    //line.SetWidth(0.1f, 0.1f);
                    
                }
            }
        }
    }

    public void positionNodes(string parent, int layernum, int baselayer, int pos)
    {
        GameObject parentObject = findNode(parent);
        if (parentObject == null)
        {
            return;
        }
        else
        {
            //visited.Add(parentObject);
            Vector3 newpos = new Vector3(pos, baselayer + 10, 30);
            //parentObject.transform.position = newpos;
            parentObject.GetComponent<DataContainer>().layernum = layernum;
            positionNodes(
                parentObject.GetComponent<DataContainer>().parent,
                layernum + 1,
                baselayer + 10,
                pos + 5
            );
        }
    }
}
