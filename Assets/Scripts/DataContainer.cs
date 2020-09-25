using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public string id;
    public string label;
    public string parent;
    public List<string> children;
    public List<GameObject> siblings;
    public int numDescendants;
    public int linknum;
    public string description;
    public int layernum;
    public int layerOrder;
    
}
