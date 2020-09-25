using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*!
 * \Collapse.cs
 * \brief a brief description of a file
 */

//! My actual function doesn't really look like this
/*! 
 *  Some sample detail which isn't exactly the same as the main
 *  function but the structure is the same
 */
public class Collapse : MonoBehaviour
{
    Renderer renderer;
    List<string> childList;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        childList = gameObject.GetComponent<DataContainer>().children;
    }
    
    void FixedUpdate()
    {
        if(gameObject.tag == "Visible")
        {
            for (int i = 0; i < childList.Count; i++)
            {
                findNode(childList[i]).tag = "Visible";
                findNode(childList[i]).GetComponent<LineRenderer>().enabled = true;
                findNode(childList[i]).GetComponentInChildren<TextMeshPro>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < childList.Count; i++)
            {
                findNode(childList[i]).tag = "Invisible";
                findNode(childList[i]).GetComponent<LineRenderer>().enabled = false;
                findNode(childList[i]).GetComponentInChildren<TextMeshPro>().enabled = false;
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

}
