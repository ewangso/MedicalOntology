using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ChangeVisibility : MonoBehaviour
{
    string nodeID;
    GameObject node;

    void Start()
    {
        nodeID = GameObject.Find("ClassID").GetComponent<Text>().text;
        node = findNode(nodeID);
    }
    
    public void collapseChildren()
    {
        if(node != null)
        {
            List<string> childList = GetDescendants(nodeID, new List<string>());
            node.GetComponent<LineRenderer>().enabled = false;
            if (childList.Count > 0)
            {
                for (int i = 0; i < childList.Count; i++)
                {
                    findNode(childList[i]).GetComponent<Renderer>().enabled = false;
                    findNode(childList[i]).GetComponent<LineRenderer>().enabled = false;
                    findNode(childList[i]).GetComponentInChildren<TextMeshPro>().enabled = false;
                }
            }
        }

    }
    
    public void showChildren()
    {
        if (node != null)
        {

            List<string> childList = node.GetComponent<DataContainer>().children;
            node.GetComponent<LineRenderer>().enabled = true;
            if (childList.Count > 0)
            {
                for (int i = 0; i < childList.Count; i++)
                {
                    findNode(childList[i]).GetComponent<Renderer>().enabled = true;
                    findNode(childList[i]).GetComponentInChildren<TextMeshPro>().enabled = true;
                }
            }
        }
    }
    
    public GameObject findNode(string nodeName)
    {
        GameObject obj = GameObject.Find(nodeName);
        if (obj != null)
            return obj;
        else
            return null;
    }

    public List<string> GetDescendants(string node, List<string> res)
    {
        List<string> children = findNode(node).GetComponent<DataContainer>().children;

        for (int i = 0; i < children.Count; i++)
        {
            res.Add(children[i]);
            GetDescendants(children[i], res);
        }

        return res;
    }
}
