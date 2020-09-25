using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBody : MonoBehaviour
{
    public GameObject bodyCanvas;
    
    public void enableCanvas()
    {
        bodyCanvas = GameObject.Find("BodyCanvas");
        bodyCanvas.GetComponent<Canvas>().enabled = true;
    }
}
